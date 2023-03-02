
using System.Linq;
using Microsoft.DocAsCode.MarkdownLite;

namespace Metalama.Documentation.DfmExtensions;

public sealed class SampleToken : IMarkdownToken
{
    public SampleToken( SampleTokenRule rule, IMarkdownContext context, SourceInfo sourceInfo, string src, string name, string title, string tabs )
    {
        this.Rule = rule;
        this.Context = context;
        this.SourceInfo = sourceInfo;
        this.Src = src;
        this.Name = name;
        this.Title = title;
        this.Tabs = tabs.Split( ',' ).Select( x => x.Trim() ).Where( x => !string.IsNullOrEmpty( x ) ).ToArray();
    }

    public IMarkdownRule Rule { get; }

    public IMarkdownContext Context { get; }

    public SourceInfo SourceInfo { get; }

    public string Src { get; }

    public string Name { get; }

    public string Title { get; }

    public string[] Tabs { get; }
}