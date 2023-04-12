// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Microsoft.DocAsCode.MarkdownLite;

namespace Metalama.Documentation.DfmExtensions;

public sealed class SingleFileToken : IMarkdownToken
{
    public IMarkdownRule Rule { get; }

    public IMarkdownContext Context { get; }

    public SourceInfo SourceInfo { get; }

    public string Src { get; }

    public bool ShowTransformed { get; }

    public string? From { get; }

    public string? To { get; }

    public SingleFileToken( IMarkdownRule rule, IMarkdownContext context, SourceInfo sourceInfo, string src, bool showTransformed, string? from, string? to )
    {
        this.Rule = rule;
        this.Context = context;
        this.SourceInfo = sourceInfo;
        this.Src = PathHelper.ResolveTokenPath( src, context, sourceInfo );
        this.ShowTransformed = showTransformed;
        this.From = from;
        this.To = to;
    }
}