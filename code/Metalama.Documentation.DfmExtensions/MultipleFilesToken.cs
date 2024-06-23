// This is public domain Metalama sample code.

using Microsoft.DocAsCode.MarkdownLite;
using System.Linq;

namespace Metalama.Documentation.DfmExtensions;

public sealed class MultipleFilesToken : TabGroupBaseToken
{
    public string[] Files { get; }

    public TabMode Mode { get; }

    public MultipleFilesToken(
        IMarkdownRule rule,
        IMarkdownContext context,
        SourceInfo sourceInfo,
        string name,
        string[] files,
        TabMode mode,
        bool addLinks )
        : base( rule, context, sourceInfo, name, "", "", addLinks )
    {
        this.Files = files.Select( f => PathHelper.ResolveTokenPath( f, context, sourceInfo ) ).ToArray();
        this.Mode = mode;
    }

    public override string ToString() => $"[!metalama-files {string.Join( " ", this.Files )}]";
}