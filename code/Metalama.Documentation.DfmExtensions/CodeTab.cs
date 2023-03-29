// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System;
using System.IO;
using System.Linq;

namespace Metalama.Documentation.DfmExtensions;

internal class CodeTab : BaseTab
{
    public string Name { get; }

    public SandboxFileKind SandboxFileKind { get; }

    public CodeTab( string tabId, string fullPath, string name, SandboxFileKind sandboxFileKind ) : base( tabId, fullPath )
    {
        this.Name = name;
        this.SandboxFileKind = sandboxFileKind;
        this.TabHeader = name + " Code";
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

    public override string GetTabContent( bool fallbackToSource = true )
    {
        var htmlPath = this.GetHtmlPath();

        if ( File.Exists( htmlPath ) )
        {
            return File.ReadAllText( htmlPath );
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