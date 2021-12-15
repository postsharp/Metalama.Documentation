using System.Threading.Tasks;
using Caravela.TestFramework;
using Xunit;
using Xunit.Abstractions;

#pragma warning disable IDE1006 // Naming Styles


namespace Caravela.Documentation.SampleCode.CompileTimeTesting
{
    public class _Runner : TestSuite
    {
        public _Runner(ITestOutputHelper logger) : base(logger) { }

        
        [Theory, CurrentDirectory]
        public Task Test(string f) => this.RunTestAsync(f);
    }
}