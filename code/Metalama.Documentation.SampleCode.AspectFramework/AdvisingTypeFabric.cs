using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;

namespace Doc.AdvisingTypeFabric
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
                        tags: new { index = i } );

                    methodBuilder.Name = "Method" + i;
                }
            }
        }
    }
}