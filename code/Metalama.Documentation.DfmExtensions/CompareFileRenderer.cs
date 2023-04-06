using Microsoft.DocAsCode.MarkdownLite;
using System.IO;
using System.Text;

namespace Metalama.Documentation.DfmExtensions;

internal class CompareFileRenderer : BaseRenderer<CompareFileToken>
{
    protected override StringBuffer RenderCore( CompareFileToken token, MarkdownBlockContext context )
    {
        var name = Path.GetFileNameWithoutExtension( token.Src ).ToLowerInvariant();
        
        var sourceTab =  new CodeTab( "source", token.Src, name, SandboxFileKind.TargetCode);
        var transformedTab = new TransformedSingleFileCodeTab( token.Src );

        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine( $@"<div id=""compare-{name}"" class=""compare"">" );
        stringBuilder.AppendLine( $@"  <div class=""left"">" );
        stringBuilder.AppendLine( $@"  <div class=""compare-header"">Source Code</div>" );
        stringBuilder.AppendLine( sourceTab.GetTabContent( false ) );
        stringBuilder.AppendLine( $@"  </div>" ); // <div class="left">
        stringBuilder.AppendLine( $@"  <div class=""right"">" );
        stringBuilder.AppendLine( $@"  <div class=""compare-header"">Transformed Code</div>" );
        stringBuilder.AppendLine( transformedTab.GetTabContent( false ) );
        stringBuilder.AppendLine( $@"  </div>" ); // <div class="right">
        stringBuilder.AppendLine( $@"</div>" ); // top div

        return stringBuilder.ToString();
    }
}