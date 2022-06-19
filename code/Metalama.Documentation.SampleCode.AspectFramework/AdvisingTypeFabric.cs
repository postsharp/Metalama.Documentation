// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;

namespace Doc.AdvisingTypeFabric
{
    internal class MyClass
    {
        private class Fabric : TypeFabric
        {
            [Template]
            public int MethodTemplate( [CompileTime] int index )
            {
                return index;
            }

            public override void AmendType( ITypeAmender amender )
            {
                for ( var i = 0; i < 10; i++ )
                {
                    amender.Advices.IntroduceMethod(
                        amender.Type,
                        nameof(this.MethodTemplate),
                        args: new { index = i },
                        buildMethod: m => m.Name = "Method" + i );
                }
            }
        }
    }
}