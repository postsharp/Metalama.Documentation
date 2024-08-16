using Metalama.Documentation.Markdig.Extensions.Tabs;

namespace Metalama.Documentation.Markdig.Extensions.MultipleFiles;

public class MultipleFilesInline : TabGroupBaseInline
{
    public string[] Files { get; set; } = [];

    public TabMode Mode { get; set; } = TabMode.Default;
}