// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

namespace Metalama.Documentation.Markdig.Extensions.Tabs;

internal class ProgramOutputTab : BaseTab
{
    public ProgramOutputTab( string fullPath ) : base( "output", fullPath ) { }

    // ReSharper disable once OptionalParameterHierarchyMismatch
    public override string GetTabContent( bool fallbackToSource = false ) => "<pre class=\"program-output\">" + File.ReadAllText( this.FullPath ) + "</pre>";

    protected override string TabHeader => "Program Output";
}