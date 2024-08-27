using Metalama.Documentation.Docfx.Markdig.Tabs;

namespace Metalama.Documentation.Docfx.Markdig.MultipleFiles;

public class MultipleFilesInline : TabGroupBaseInline
{
    public string[] Files { get; set; } = [];

    public TabMode Mode { get; set; } = TabMode.Default;
}