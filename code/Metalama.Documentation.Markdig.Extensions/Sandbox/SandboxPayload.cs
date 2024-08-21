// This is public domain Metalama sample code.

using Newtonsoft.Json;
using PKT.LZStringCSharp;

namespace Metalama.Documentation.Markdig.Extensions.Sandbox;

public class SandboxPayload
{
    [JsonProperty( PropertyName = "f" )]
    public IReadOnlyList<SandboxFile> Files { get; }

    public SandboxPayload( IReadOnlyList<SandboxFile> files )
    {
        this.Files = files;
    }

    public string ToCompressedString()
    {
        var tryPayloadJson = JsonConvert.SerializeObject( this );

        return LZString.CompressToEncodedURIComponent( tryPayloadJson );
    }
}