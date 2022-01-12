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

            public override void AmendType(ITypeAmender amender)
            {
                for ( int i = 0; i < 10; i++)
                {
                    var methodBuilder = amender.Advices.IntroduceMethod(
                        amender.Type, 
                        nameof(MethodTemplate), 
                        tags: new () {  ["index"] = i} );

                    methodBuilder.Name = "Method" + i;

                }
            }
        }
    }
}
