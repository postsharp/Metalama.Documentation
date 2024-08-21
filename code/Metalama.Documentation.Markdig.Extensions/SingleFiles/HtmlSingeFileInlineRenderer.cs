using Markdig.Renderers;
using Markdig.Renderers.Html;
using Metalama.Documentation.Markdig.Extensions.Sandbox;
using Metalama.Documentation.Markdig.Extensions.Tabs;

namespace Metalama.Documentation.Markdig.Extensions.SingleFiles;

public class HtmlSingeFileInlineRenderer : HtmlObjectRenderer<SingleFileInline>
{
    protected override void Write( HtmlRenderer renderer, SingleFileInline obj )
    {
        var name = Path.GetFileNameWithoutExtension( obj.Src );

        var tab = token.ShowTransformed
            ? new TransformedSingleFileCodeTab( Path.GetFileNameWithoutExtension( token.Src ), token.Src, "" )
            : new CodeTab( name, token.Src, SandboxFileKind.ExtraCode, token.Marker, token.Member );

        renderer.WriteLine( "<div class='single-file'>" );
        renderer.WriteLine( tab.GetTabContent( false ) );
        renderer.WriteLine( "</div>" );
    }
}