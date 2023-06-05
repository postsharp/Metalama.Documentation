// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System.Text;

namespace Metalama.Documentation.DfmExtensions;

internal class CompareTab : BaseTab
{
    public CompareTab( string tabId, string tabHeader, string fullPath ) : base( tabId, fullPath )
    {
        this.TabHeader = tabHeader;
    }

    private CodeTab GetSourceTab() => new( "source", this.FullPath, this.TabId, SandboxFileKind.TargetCode );

    public override string GetTabContent( bool fallbackToSource = true )
    {
        var sourceTab = this.GetSourceTab();
        var transformedTab = new TransformedSingleFileCodeTab( this.FullPath );

        string NormalizeCode( string s ) => s.Replace( " ", "" );

        if ( !transformedTab.Exists()  || NormalizeCode( sourceTab.GetSandboxCode() ) == NormalizeCode( transformedTab.GetSandboxCode() ) )
        {
            return sourceTab.GetTabContent( fallbackToSource );
        }

        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine( $@"<div id=""compare-{this.TabId}"" class=""compare"">" );
        stringBuilder.AppendLine( $@"  <div class=""left"">" );
        stringBuilder.AppendLine( $@"  <div class=""compare-header"">Source Code</div>" );
        stringBuilder.AppendLine( sourceTab.GetTabContent( false ) );
        stringBuilder.AppendLine( $@"  </div>" ); // <div class="left">
        stringBuilder.AppendLine( $@"  <div class=""right"">" );
        stringBuilder.AppendLine( $@"  <div class=""compare-header"">Transformed Code</div>" );
        stringBuilder.AppendLine( transformedTab.GetTabContent( false ) );
        stringBuilder.AppendLine( $@"  </div>" ); // <div class="right">
        stringBuilder.AppendLine( $@"</div>" );   // top div

        return stringBuilder.ToString();
    }

    public string GetSandboxCode() => this.GetSourceTab().GetSandboxCode();

    protected override string TabHeader { get; }
}