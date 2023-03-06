using Newtonsoft.Json;
using Xunit;

namespace Metalama.Documentation.DfmExtensions.UnitTests;

public class TryPayloadSerializationTests
{
    [Fact]
    public void Serialize()
    {
        var expected = """{"f":[{"n":"Program.cs","c":"class Program {}","k":"t"},{"n":"Aspect.cs","c":"class Aspect {}","k":"a"},{"n":"Additional.cs","c":"class Helper {}","k":"e"}]}""";

        var actual = JsonConvert.SerializeObject( new TryPayload( new[] {
            new TryPayloadFile( "Program.cs", "class Program {}", TryFileKind.TargetCode ),
            new TryPayloadFile( "Aspect.cs", "class Aspect {}", TryFileKind.AspectCode ),
            new TryPayloadFile( "Additional.cs", "class Helper {}", TryFileKind.ExtraCode )
        } ) );

        Assert.Equal( expected, actual );
    }
}