using Microsoft.DocAsCode.MarkdownLite;

namespace Metalama.Documentation.DfmExtensions;

public sealed class SingleFileToken : IMarkdownToken
{
    public IMarkdownRule Rule { get; }

    public IMarkdownContext Context { get; }

    public SourceInfo SourceInfo { get; }

    public string Src { get; }

    public bool ShowTransformed { get; }

    public SingleFileToken( IMarkdownRule rule, IMarkdownContext context, SourceInfo sourceInfo, string src, bool showTransformed )
    {
        this.Rule = rule;
        this.Context = context;
        this.SourceInfo = sourceInfo;
        this.Src = PathHelper.ResolveTokenPath( src, context, sourceInfo );
        this.ShowTransformed = showTransformed;
    }
}