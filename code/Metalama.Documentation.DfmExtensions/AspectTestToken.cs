// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

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
        string tabs ) : base( rule, context, sourceInfo, name, title, tabs )
    {
        this.Src = PathHelper.ResolveTokenPath( src, context, sourceInfo );
    }

    public string Src { get; }

    public override string ToString() => $"[!metalama-test {this.Src}]";
}