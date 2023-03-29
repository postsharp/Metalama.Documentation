// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Microsoft.DocAsCode.MarkdownLite;
using System.Linq;

namespace Metalama.Documentation.DfmExtensions;

public abstract class TabGroupBaseToken : IMarkdownToken
{
    protected TabGroupBaseToken( IMarkdownRule rule, IMarkdownContext context, SourceInfo sourceInfo, string src, string name, string title, string tabs )
    {
        this.Rule = rule;
        this.Context = context;
        this.SourceInfo = sourceInfo;
        this.Src = PathHelper.ResolveTokenPath( src, context, sourceInfo );
        this.Name = name;
        this.Title = title;
        this.Tabs = tabs.Split( ',' ).Select( x => x.Trim() ).Where( x => !string.IsNullOrEmpty( x ) ).ToArray();
    }

    public string[] Tabs { get; }

    public IMarkdownRule Rule { get; }

    public IMarkdownContext Context { get; }

    public SourceInfo SourceInfo { get; }

    public string Src { get; }

    public string Name { get; }

    public string Title { get; }
}