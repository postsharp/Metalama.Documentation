// This is public domain Metalama sample code.

using Newtonsoft.Json;
using PKT.LZStringCSharp;
using System.Collections.Generic;

namespace Metalama.Documentation.DfmExtensions;

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