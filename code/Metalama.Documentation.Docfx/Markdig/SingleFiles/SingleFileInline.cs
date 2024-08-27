using Metalama.Documentation.Docfx.Markdig.Tabs;

namespace Metalama.Documentation.Docfx.Markdig.SingleFiles;

public class SingleFileInline : TabGroupBaseInline
{
    public string Src { get; set; } = null!;
    
    public bool ShowTransformed { get; set; }

    public string? Marker { get; set; }

    public string? Member { get; set; }
}