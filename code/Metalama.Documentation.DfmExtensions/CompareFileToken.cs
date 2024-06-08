// This is public domain Metalama sample code.

using Microsoft.DocAsCode.MarkdownLite;

namespace Metalama.Documentation.DfmExtensions;

public sealed class CompareFileToken : IMarkdownToken
{
    public IMarkdownRule Rule { get; }

    public IMarkdownContext Context { get; }

    public SourceInfo SourceInfo { get; }

    public string Src { get; }

    public CompareFileToken( IMarkdownRule rule, IMarkdownContext context, SourceInfo sourceInfo, string src )
    {
        this.Rule = rule;
        this.Context = context;
        this.SourceInfo = sourceInfo;
        this.Src = PathHelper.ResolveTokenPath( src, context, sourceInfo );
    }
}