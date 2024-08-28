using Markdig.Renderers;
using Markdig.Renderers.Html;
using Metalama.Documentation.Markdig.Extensions.Sandbox;
using Metalama.Documentation.Markdig.Extensions.Tabs;

namespace Metalama.Documentation.Markdig.Extensions.MultipleFiles;

public class HtmlMultipleFilesInlineRenderer : HtmlObjectRenderer<MultipleFilesInline>
{
    protected override void Write( HtmlRenderer renderer, MultipleFilesInline obj )
    {
        var directory = Path.GetDirectoryName( obj.Files[0] )!;
        var tabGroup = new DirectoryTabGroup( obj.Name.Replace( '.', '_' ), directory );

        foreach ( var file in obj.Files )
        {
            var tabId = Path.GetFileNameWithoutExtension( file );
            var tabName = Path.GetFileName( file );

            switch ( obj.Mode )
            {
                case TabMode.Source:
                    tabGroup.Tabs.Add( new CodeTab( tabId, file, tabName, SandboxFileKind.Incompatible ) );

                    break;

                case TabMode.Default:
                case TabMode.Diff:
                    tabGroup.Tabs.Add( new CompareTab( tabId, tabName, file ) );

                    break;

                case TabMode.Transformed:
                    tabGroup.Tabs.Add( new TransformedSingleFileCodeTab( file ) );

                    break;
            }
        }

        tabGroup.Render( renderer, obj );
    }
}