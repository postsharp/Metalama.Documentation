// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;

namespace Metalama.Documentation.SampleCode.AspectFramework.AdvisingTypeFabric
{
    internal class MyClass
    {
        private class Fabric : TypeFabric
        {
            [Template]
            public int MethodTemplate() => (int) meta.Tags["index"]!;

            public override void AmendType( ITypeAmender amender )
            {
                for ( var i = 0; i < 10; i++ )
                {
                    var methodBuilder = amender.Advices.IntroduceMethod(
                        amender.Type,
                        nameof(this.MethodTemplate),
                        tags: new Framework.Aspects.Tags { ["index"] = i } );

                    methodBuilder.Name = "Method" + i;
                }
            }
        }
    }
}