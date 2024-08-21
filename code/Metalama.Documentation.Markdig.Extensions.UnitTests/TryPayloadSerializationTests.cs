// This is public domain Metalama sample code.

using Metalama.Documentation.Markdig.Extensions.Sandbox;
using Newtonsoft.Json;

namespace Metalama.Documentation.Markdig.Extensions.UnitTests;

public class TryPayloadSerializationTests
{
    [Fact]
    public void PayloadIsSerializable()
    {
        var expected =
            """{"f":[{"n":"Program.cs","c":"class Program {}","k":"t"},{"n":"Aspect.cs","c":"class Aspect {}","k":"a"},{"n":"Additional.cs","c":"class Helper {}","k":"e"}]}""";

        var actual = JsonConvert.SerializeObject(
            new SandboxPayload(
                new[]
                {
                    new SandboxFile( "Program.cs", "class Program {}", SandboxFileKind.TargetCode ),
                    new SandboxFile( "Aspect.cs", "class Aspect {}", SandboxFileKind.AspectCode ),
                    new SandboxFile( "Additional.cs", "class Helper {}", SandboxFileKind.ExtraCode )
                } ) );

        Assert.Equal( expected, actual );
    }
}