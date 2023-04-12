// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System.IO;
using System.Text;

namespace Metalama.Documentation.DfmExtensions;

internal class CompareTab : BaseTab
{
    public CompareTab( string tabId, string fullPath ) : base( tabId, fullPath )
    {
        this.TabHeader = Path.GetFileNameWithoutExtension( fullPath );
    }

    public override string GetTabContent( bool fallbackToSource = true )
    {
        var sourceTab = new CodeTab( "source", this.FullPath, this.TabId, SandboxFileKind.TargetCode );
        var transformedTab = new TransformedSingleFileCodeTab( this.FullPath );

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

    protected override string TabHeader { get; }
}