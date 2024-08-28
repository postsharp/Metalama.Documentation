using Markdig.Renderers;
using Markdig.Renderers.Html;
using Metalama.Documentation.Markdig.Extensions.Tabs;

namespace Metalama.Documentation.Markdig.Extensions.CompareFile;

public class HtmlCompareFileInlineRenderer : HtmlObjectRenderer<CompareFileInline>
{
    protected override void Write( HtmlRenderer renderer, CompareFileInline obj )
    {
        if ( !File.Exists( obj.Src ) )
        {
            throw new FileNotFoundException( $"The file '{obj.Src}' does not exist." );
        }

        var name = Path.GetFileNameWithoutExtension( obj.Src ).ToLowerInvariant();
        var compareTab = new CompareTab( name, name, obj.Src );
        var content = compareTab.GetTabContent();

        renderer.WriteLine( content );
    }
}