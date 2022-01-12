using Metalama.Framework.Aspects;

namespace Metalama.Documentation.SampleCode.AspectFramework
{
    public class EmptyOverrideFieldOrPropertyAttribute : OverrideFieldOrPropertyAspect
    {
        public override dynamic? OverrideProperty
        {
            get => meta.Proceed();
            set => meta.Proceed();
        }
    }
}
