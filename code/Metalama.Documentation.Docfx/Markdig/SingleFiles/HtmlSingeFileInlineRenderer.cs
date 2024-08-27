using Markdig.Renderers;
using Markdig.Renderers.Html;
using Metalama.Documentation.Docfx.Markdig.Sandbox;
using Metalama.Documentation.Docfx.Markdig.Tabs;

namespace Metalama.Documentation.Docfx.Markdig.SingleFiles;

public class HtmlSingeFileInlineRenderer : HtmlObjectRenderer<SingleFileInline>
{
    protected override void Write( HtmlRenderer renderer, SingleFileInline obj )
    {
        var name = Path.GetFileNameWithoutExtension( obj.Src );

        var tab = obj.ShowTransformed
            ? new TransformedSingleFileCodeTab(
                Path.GetFileNameWithoutExtension( obj.Src ),
                obj.Src,
                "" )
            : new CodeTab( name, obj.Src, SandboxFileKind.ExtraCode, obj.Marker, obj.Member );

        renderer.WriteLine( "<div class='single-file'>" );
        renderer.WriteLine( tab.GetTabContent( false ) );
        renderer.WriteLine( "</div>" );
    }
}