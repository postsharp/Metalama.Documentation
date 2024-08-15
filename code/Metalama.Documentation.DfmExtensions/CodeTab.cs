﻿// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Metalama.Documentation.DfmExtensions;

internal class CodeTab : BaseTab
{
    private static readonly Regex _htmlStartMarkerRegex = new( """<span class="[^\"]*">\/\*&lt;([\w+]+)&gt;\*\/<\/span>""" );
    private static readonly Regex _htmlEndMarkerRegex = new( """<span class="[^\"]*">\/\*&lt;\/([\w+]+)&gt;\*\/<\/span>""" );
    private static readonly Regex _anyMarkerRegex = new( """\/\*\<\/?([\w+]+)\>\*\/""" );
    private static readonly Regex _memberRegex = new( """<span class='line-number' data-member='([^']*)'>""" );
    private static readonly Regex _emptyLineRegex = new( """<span class='line-number'[^>]*>\d+<\/span>\s*$""" );

    public string Name { get; }

    public SandboxFileKind SandboxFileKind { get; }

    public string? Member { get; }

    public string? Marker { get; }

    public CodeTab(
        string tabId,
        string fullPath,
        string name,
        SandboxFileKind sandboxFileKind,
        string? marker = null,
        string? member = null ) : base(
        tabId,
        fullPath )
    {
        this.Name = name;
        this.SandboxFileKind = sandboxFileKind;
        this.Member = member;
        this.TabHeader = name + " Code";
        this.Marker = marker;
    }

    protected override bool IsContentEmpty( string[] lines )
        => base.IsContentEmpty( lines ) || lines.All( l => l.TrimStart().StartsWith( "//", StringComparison.Ordinal ) );

    private string? GetHtmlPath() => PathHelper.GetObjPath( this.GetProjectDirectory(), this.FullPath, this.HtmlExtension );

    protected virtual string HtmlExtension => ".cs.html";

    public bool Exists() => this.GetHtmlPath() != null;

    public override string GetTabContent( bool fallbackToSource = true )
    {
        var htmlPath = this.GetHtmlPath();

        if ( htmlPath != null )
        {
            if ( !string.IsNullOrEmpty( this.Marker ) || !string.IsNullOrEmpty( this.Member ) )
            {
                var outputLines = new List<string>();

                var isWithinMarker = false;
                var foundStartMarker = false;
                var foundEndMarker = false;
                var foundMember = false;

                // Read and filter lines.
                foreach ( var htmlLine in File.ReadAllLines( htmlPath ) )
                {
                    var matchStartMarker = _htmlStartMarkerRegex.Match( htmlLine );

                    if ( matchStartMarker.Success && matchStartMarker.Groups[1].Value == this.Marker )
                    {
                        isWithinMarker = true;
                        foundStartMarker = true;
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
                        var cleanedLine = _htmlStartMarkerRegex.Replace( _htmlEndMarkerRegex.Replace( htmlLine, "" ), "" );
                        outputLines.Add( cleanedLine );

                        var matchEndMarker = _htmlEndMarkerRegex.Match( htmlLine );

                        if ( matchEndMarker.Success && matchEndMarker.Groups[1].Value == this.Marker )
                        {
                            isWithinMarker = false;
                            foundEndMarker = true;
                        }
                    }
                }

                // Check that we found the markers.
                if ( this.Marker != null && !foundStartMarker )
                {
                    throw new InvalidOperationException( $"The '/*<{this.Marker}>*/' marker was not found in '{htmlPath}'." );
                }

                if ( this.Marker != null && !foundEndMarker )
                {
                    throw new InvalidOperationException( $"The '/*</{this.Marker}>*/' marker was not found in '{htmlPath}'." );
                }

                if ( this.Member != null && !foundMember )
                {
                    throw new InvalidOperationException( $"The member '{this.Member}' was not found in '{htmlPath}'." );
                }

                // Trim.
                while ( outputLines.Count > 0 && _emptyLineRegex.IsMatch( outputLines[0] ) )
                {
                    outputLines.RemoveAt( 0 );
                }

                while ( outputLines.Count > 0 && _emptyLineRegex.IsMatch( outputLines[outputLines.Count - 1] ) )
                {
                    outputLines.RemoveAt( outputLines.Count - 1 );
                }

                // Return the final html.

                return "<pre><code class=\"nohighlight\">" + string.Join( "\n", outputLines ) + "</code></pre>";
            }
            else
            {
                var html = File.ReadAllText( htmlPath );
                var cleanHtml = _htmlStartMarkerRegex.Replace( _htmlEndMarkerRegex.Replace( html, "" ), "" );

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
            throw new FileNotFoundException( $"No HTML file for '{this.FullPath}' could be found.", htmlPath );
        }
    }

    protected override string TabHeader { get; }

    public string GetCodeForComparison()
    {
        var document = new HtmlDocument();
        document.Load( this.GetHtmlPath() ?? throw new FileNotFoundException( $"Cannot find the HTML file for {this.FullPath}." ) );

        var diagLines = document.DocumentNode.SelectNodes( "//span[@class='diagLines']" )?.ToList() ?? new List<HtmlNode>();

        foreach ( var diag in diagLines )
        {
            diag.Remove();
        }

        return document.DocumentNode.InnerText;
    }

    public string GetSandboxCode()
    {
        var lines = File.ReadAllLines( this.FullPath )
            .SkipWhile( l => l.TrimStart().StartsWith( "//", StringComparison.Ordinal ) )
            .Select( x => _anyMarkerRegex.Replace( x, "" ) )
            .ToList();

        // Trim.
        while ( lines.Count > 0 && string.IsNullOrWhiteSpace( lines[0] ) )
        {
            lines.RemoveAt( 0 );
        }

        while ( lines.Count > 0 && string.IsNullOrWhiteSpace( lines[lines.Count - 1] ) )
        {
            lines.RemoveAt( lines.Count - 1 );
        }

        return string.Join( Environment.NewLine, lines );
    }
}