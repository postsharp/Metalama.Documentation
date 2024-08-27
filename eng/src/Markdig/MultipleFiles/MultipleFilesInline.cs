using BuildMetalamaDocumentation.Markdig.Tabs;

namespace BuildMetalamaDocumentation.Markdig.MultipleFiles;

public class MultipleFilesInline : TabGroupBaseInline
{
    public string[] Files { get; set; } = [];

    public TabMode Mode { get; set; } = TabMode.Default;
}