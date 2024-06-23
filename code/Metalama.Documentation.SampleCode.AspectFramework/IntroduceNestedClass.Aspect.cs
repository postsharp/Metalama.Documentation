// This is public domain Metalama sample code.

#pragma warning disable CA1725

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.IntroduceNestedClass;

public class BuilderAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        base.BuildAspect( builder );

        // Introduce a nested type.
        builder.IntroduceClass( "Builder" );
    }
}