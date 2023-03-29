using Microsoft.DocAsCode.MarkdownLite;

namespace Metalama.Documentation.DfmExtensions;

public sealed class ProjectButtonsToken : TabGroupBaseToken
{
    public ProjectButtonsToken( IMarkdownRule rule, IMarkdownContext context, SourceInfo sourceInfo, string src, string name, string title, string tabs ) : base( rule, context, sourceInfo, src, name, title, tabs ) { }
}