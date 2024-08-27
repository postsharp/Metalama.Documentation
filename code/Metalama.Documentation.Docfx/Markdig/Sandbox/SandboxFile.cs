// This is public domain Metalama sample code.

using Newtonsoft.Json;

namespace Metalama.Documentation.Docfx.Markdig.Sandbox;

public class SandboxFile
{
    [JsonProperty( PropertyName = "n" )]
    public string Name { get; }

    [JsonProperty( PropertyName = "c" )]
    public string Content { get; }

    [JsonProperty( PropertyName = "k" )]
    public SandboxFileKind Kind { get; }

    public SandboxFile( string name, string content, SandboxFileKind kind )
    {
        this.Name = name;
        this.Content = content;
        this.Kind = kind;
    }
}