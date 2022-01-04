using Metalama.Framework.Aspects;

namespace Metalama.Documentation.SampleCode.AspectFramework
{
    public class EmptyOverrideMethodAttribute : OverrideMethodAspect
    {
        public override dynamic? OverrideMethod()
        {
            return meta.Proceed();
        }
    }
}
