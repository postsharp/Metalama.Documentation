using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Eligibility;
using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.OverrideMethodAspect_
{
#pragma warning disable CS0067
    // <aspect>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class OverrideMethodAspect : Attribute, IAspect<IMethod>
    {
        public virtual void BuildAspect(IAspectBuilder<IMethod> builder)
        {
            builder.Advices.OverrideMethod(builder.Target, nameof(this.OverrideMethod));
        }

        public virtual void BuildEligibility(IEligibilityBuilder<IMethod> builder) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");


        [Template]
        public abstract dynamic? OverrideMethod();
    }
#pragma warning restore CS0067

    // </aspect>
}