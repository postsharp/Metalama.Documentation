// This is public domain Metalama sample code.

using Microsoft.DocAsCode.MarkdownLite;

namespace Metalama.Documentation.DfmExtensions;

public sealed class AspectTestToken : TabGroupBaseToken
{
    public AspectTestToken(
        AspectTestTokenRule rule,
        IMarkdownContext context,
        SourceInfo sourceInfo,
        string src,
        string name,
        string title,
        string tabs,
        DiffSide diffSide ) : base( rule, context, sourceInfo, name, title, tabs, diffSide: diffSide )
    {
        this.Src = PathHelper.ResolveTokenPath( src, context, sourceInfo );
    }

    public string Src { get; }

    public override string ToString() => $"[!metalama-test {this.Src}]";
}