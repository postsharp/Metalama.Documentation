﻿// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Metalama.Documentation.DfmExtensions;

internal class CodeTab : BaseTab
{
    private static readonly Regex _htmlMarkerRegex = new( """<span class="[^\"]*">\/\*\[([^\*]+)\]\*\/<\/span>""" );
    private static readonly Regex _memberRegex = new( """<span class='line-number' data-member='([^']*)'>""" );

    public string Name { get; }

    public SandboxFileKind SandboxFileKind { get; }

    public string? Member { get; }

    public string? FromMarker { get; }

    public string? ToMarker { get; }

    public CodeTab(
        string tabId,
        string fullPath,
        string name,
        SandboxFileKind sandboxFileKind,
        string? fromMarker = null,
        string? toMarker = null,
        string? member = null ) : base(
        tabId,
        fullPath )
    {
        this.Name = name;
        this.SandboxFileKind = sandboxFileKind;
        this.Member = member;
        this.TabHeader = name + " Code";
        this.FromMarker = fromMarker ?? toMarker;
        this.ToMarker = toMarker ?? fromMarker;
    }

    protected override bool IsContentEmpty( string[] lines ) => base.IsContentEmpty( lines ) || lines.All( l => l.TrimStart().StartsWith( "//" ) );

    private string GetHtmlPath()
    {
        var projectDirectory = this.GetProjectDirectory();
        var relativePath = PathHelper.GetRelativePath( projectDirectory, this.FullPath );

        return Path.GetFullPath(
            Path.Combine(
                projectDirectory,
                "obj",
                "html",
                "net6.0",
                Path.ChangeExtension( relativePath, this.HtmlExtension ) ) );
    }

    protected virtual string HtmlExtension => ".cs.html";

    public bool Exists() => File.Exists( this.GetHtmlPath() );

    public override string GetTabContent( bool fallbackToSource = true )
    {
        var htmlPath = this.GetHtmlPath();

        if ( File.Exists( htmlPath ) )
        {
            if ( !string.IsNullOrEmpty( this.FromMarker ) || !string.IsNullOrEmpty( this.Member ) )
            {
                var output = new StringBuilder();
                output.Append( @"<pre><code class=""nohighlight"">" );

                var isWithinMarker = false;
                var foundStartMarker = false;
                var foundEndMarker = false;
                var foundMember = false;

                foreach ( var htmlLine in File.ReadAllLines( htmlPath ) )
                {
                    var matchLine = _htmlMarkerRegex.Match( htmlLine );

                    string? marker = null;

                    if ( matchLine.Success )
                    {
                        marker = matchLine.Groups[1].Value;

                        if ( marker == this.FromMarker )
                        {
                            isWithinMarker = true;
                            foundStartMarker = true;
                        }
                    }
                    else if ( !string.IsNullOrEmpty( this.Member ) )
                    {
                        var matchMember = _memberRegex.Match( htmlLine );
                        isWithinMarker = matchMember.Success && this.Member == matchMember.Groups[1].Value;

                        if ( isWithinMarker )
                        {
                            foundMember = true;
                        }
                    }

                    if ( isWithinMarker )
                    {
                        var cleanedLine = _htmlMarkerRegex.Replace( htmlLine, "" );
                        output.AppendLine( cleanedLine );

                        if ( marker == this.ToMarker )
                        {
                            isWithinMarker = false;
                            foundEndMarker = true;
                        }
                    }
                }

                output.AppendLine( "</code></pre>" );

                if ( this.FromMarker != null && !foundStartMarker )
                {
                    throw new InvalidOperationException( $"The 'from' marker '{this.FromMarker}' was not found." );
                }

                if ( this.ToMarker != null && !foundEndMarker )
                {
                    throw new InvalidOperationException( $"The 'to' marker '{this.ToMarker}' was not found." );
                }

                if ( this.Member != null && !foundMember )
                {
                    throw new InvalidOperationException( $"The member '{this.Member}' was not found." );
                }

                return output.ToString();
            }
            else
            {
                var html = File.ReadAllText( htmlPath );
                  var cleanHtml = _htmlMarkerRegex.Replace( html, "" );

                  return cleanHtml;
            }
        }
        else if ( fallbackToSource )
        {
            // When the HTML file does not exist, we will rely on run-time formatting.
            return "<pre><code class=\"lang-csharp\">" + File.ReadAllText( this.FullPath ) + "<code></pre>";
        }
        else
        {
            throw new FileNotFoundException( $"The file '{htmlPath}' could not be found.", htmlPath );
        }
    }

    protected override string TabHeader { get; }

    public string GetSandboxCode()
    {
        var lines = File.ReadAllLines( this.FullPath );

        return string.Join( Environment.NewLine, lines.SkipWhile( l => l.TrimStart().StartsWith( "//" ) ) );
    }
}