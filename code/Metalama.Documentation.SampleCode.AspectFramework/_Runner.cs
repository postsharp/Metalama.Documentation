// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

using Metalama.TestFramework;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

#pragma warning disable IDE1006 // Naming Styles

namespace Metalama.Documentation.SampleCode.CompileTimeTesting
{
    public class _Runner : TestSuite
    {
        public _Runner( ITestOutputHelper logger ) : base( logger ) { }

        [Theory]
        [CurrentDirectory]
        public Task Test( string f ) => this.RunTestAsync( f );
    }
}