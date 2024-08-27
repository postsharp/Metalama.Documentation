// This is public domain Metalama sample code.

using System.IO;

namespace BuildMetalamaDocumentation.Markdig.Tabs;

internal class ProgramOutputTab : BaseTab
{
    public ProgramOutputTab( string fullPath ) : base( "output", fullPath ) { }

    public override string GetTabContent( bool fallbackToSource = false )
        => "<pre class=\"program-output\">" + File.ReadAllText( this.FullPath ) + "</pre>";

    protected override string TabHeader => "Program Output";
}