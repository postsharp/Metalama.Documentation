using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Metalama.Documentation.DfmExtensions;

[JsonConverter( typeof( StringEnumConverter ) )]
public enum TryFileKind
{
    [EnumMember(Value = "t")]
    TargetCode,
    [EnumMember(Value = "a")]
    AspectCode,
    [EnumMember(Value = "e")]
    ExtraCode
}

public class TryPayload
{
    [JsonProperty(PropertyName = "f")]
    public IReadOnlyList<TryPayloadFile> Files { get; }

    public TryPayload( IReadOnlyList<TryPayloadFile> files )
    {
        this.Files = files;
    }
}

public class TryPayloadFile
{
    [JsonProperty( PropertyName = "n" )]
    public string Name { get; }

    [JsonProperty( PropertyName = "c" )]
    public string Content { get; }
    
    [JsonProperty(PropertyName = "k")]
    public TryFileKind Kind { get; }

    public TryPayloadFile( string name, string content, TryFileKind kind )
    {
        this.Name = name;
        this.Content = content;
        this.Kind = kind;
    }
}
