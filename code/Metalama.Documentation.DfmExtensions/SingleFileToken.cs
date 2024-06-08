// This is public domain Metalama sample code.

using Microsoft.DocAsCode.MarkdownLite;

namespace Metalama.Documentation.DfmExtensions;

public sealed class SingleFileToken : IMarkdownToken
{
    public IMarkdownRule Rule { get; }

    public IMarkdownContext Context { get; }

    public SourceInfo SourceInfo { get; }

    public string Src { get; }

    public bool ShowTransformed { get; }

    public string? Marker { get; }

    public string? Member { get; }

    public SingleFileToken(
        IMarkdownRule rule,
        IMarkdownContext context,
        SourceInfo sourceInfo,
        string src,
        bool showTransformed,
        string? marker,
        string? member )
    {
        this.Rule = rule;
        this.Context = context;
        this.SourceInfo = sourceInfo;
        this.Src = PathHelper.ResolveTokenPath( src, context, sourceInfo );
        this.ShowTransformed = showTransformed;
        this.Marker = marker;
        this.Member = member;
    }
}