using Metalama.Documentation.Markdig.Extensions.Tabs;

namespace Metalama.Documentation.Markdig.Extensions.CompareFile;

public class CompareFileInline : TabGroupBaseInline
{
    public string Src { get; set; } = null!;
}