// This is public domain Metalama sample code.

using Microsoft.DocAsCode.MarkdownLite;
using System.IO;
using System.Text;

namespace Metalama.Documentation.DfmExtensions;

internal class MultipleFilesRenderer : BaseRenderer<MultipleFilesToken>
{
    protected override StringBuffer RenderCore( MultipleFilesToken token, MarkdownBlockContext context )
    {
        var directory = Path.GetDirectoryName( token.Files[0] )!;
        var tabGroup = new DirectoryTabGroup( token.Name.Replace( '.', '_' ), directory );

        foreach ( var file in token.Files )
        {
            var tabId = Path.GetFileNameWithoutExtension( file );
            var tabName = Path.GetFileName( file );

            switch ( token.Mode )
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

        var stringBuilder = new StringBuilder();

        tabGroup.Render( stringBuilder, token );

        return stringBuilder.ToString();
    }
}