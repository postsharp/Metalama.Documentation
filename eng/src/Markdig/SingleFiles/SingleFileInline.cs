using BuildMetalamaDocumentation.Markdig.Tabs;

namespace BuildMetalamaDocumentation.Markdig.SingleFiles;

public class SingleFileInline : TabGroupBaseInline
{
    public string Src { get; set; } = null!;
    
    public bool ShowTransformed { get; set; }

    public string? Marker { get; set; }

    public string? Member { get; set; }
}