// This is public domain Metalama sample code.

using Microsoft.DocAsCode.MarkdownLite;

namespace Metalama.Documentation.DfmExtensions;

public class VimeoToken : IMarkdownToken
{
    public IMarkdownRule Rule { get; }

    public IMarkdownContext Context { get; }

    public SourceInfo SourceInfo { get; }

    public string Id { get; }

    public VimeoToken( IMarkdownRule rule, IMarkdownContext context, SourceInfo sourceInfo, string id )
    {
        this.Rule = rule;
        this.Context = context;
        this.SourceInfo = sourceInfo;
        this.Id = id;
    }
}