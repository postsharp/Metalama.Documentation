// This is public domain Metalama sample code.

using Metalama.Testing.AspectTesting;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

#pragma warning disable IDE1006 // Naming Styles

namespace Metalama.Documentation.SampleCode.CompileTimeTesting;

public class _Runner : AspectTestClass
{
    public _Runner( ITestOutputHelper logger ) : base( logger ) { }

    [Theory]
    [CurrentDirectory]
    public Task Test( string f ) => this.RunTestAsync( f );
}