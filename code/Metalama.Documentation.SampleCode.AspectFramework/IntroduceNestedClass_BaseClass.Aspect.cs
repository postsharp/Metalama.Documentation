// This is public domain Metalama sample code.

#if TEST_OPTIONS
// @OutputAllSyntaxTrees
#endif

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.IntroduceNestedClass_BaseClass;

public class BuilderAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        base.BuildAspect( builder );

        builder.IntroduceClass(
            "Builder",
            typeBuilder =>
            {
                typeBuilder.IsSealed = false;
                typeBuilder.BaseType = (INamedType) TypeFactory.GetType( typeof(BaseFactory) );
            } );
    }
}