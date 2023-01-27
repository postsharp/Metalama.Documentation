using Microsoft.DocAsCode.MarkdownLite;

namespace Metalama.Documentation.DfmExtensions;

public sealed class SampleToken : IMarkdownToken
{
    public SampleToken( SampleTokenRule rule, IMarkdownContext context, string src, string name, string title, SourceInfo sourceInfo )
    {
        Rule = rule;
        Context = context;
        SourceInfo = sourceInfo;
        Src = src;
        Name = name;
        Title = title;
    }

    public IMarkdownRule Rule { get; }

    public IMarkdownContext Context { get; }

    public SourceInfo SourceInfo { get; }

    public string Src { get; }

    public string Name { get; }

    public string Title { get; }
}