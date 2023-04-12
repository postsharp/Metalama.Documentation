// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Microsoft.DocAsCode.MarkdownLite;

namespace Metalama.Documentation.DfmExtensions;

public sealed class ProjectButtonsToken : TabGroupBaseToken
{
    public ProjectButtonsToken(
        IMarkdownRule rule,
        IMarkdownContext context,
        SourceInfo sourceInfo,
        string directory,
        string name,
        string title,
        string tabs ) :
        base( rule, context, sourceInfo, name, title, tabs )
    {
        this.Directory = PathHelper.ResolveTokenPath( directory, context, sourceInfo );
    }

    public string Directory { get; }
}