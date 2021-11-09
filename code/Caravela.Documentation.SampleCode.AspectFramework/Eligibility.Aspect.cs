using Caravela.Framework.Aspects;
using Caravela.Framework.Code;
using Caravela.Framework.Eligibility;

namespace Caravela.Documentation.SampleCode.AspectFramework.Eligibility
{
    internal class LogAttribute : OverrideMethodAspect
    {
        public override void BuildEligibility(IEligibilityBuilder<IMethod> builder)
        {
            base.BuildEligibility(builder);

            // The aspect must not be offered to non-static methods because it uses a static field 'logger'.
            builder.MustBeNonStatic();
        }

        public override dynamic? OverrideMethod()
        {
            meta.This.logger.WriteLine($"Executing {meta.Target.Method}");

            return meta.Proceed();
        }
    }
}
