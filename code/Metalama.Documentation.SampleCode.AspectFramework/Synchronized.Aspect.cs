using System.Linq;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Metalama.Documentation.SampleCode.AspectFramework.Synchronized
{
    internal class SynchronizedAttribute : TypeAspect
    {
        public override void BuildAspect( IAspectBuilder<INamedType> builder )
        {
            foreach ( var method in builder.Target.Methods.Where( m => !m.IsStatic))
            {
                builder.Advices.OverrideMethod(method, nameof(OverrideMethod));
            }
        }

        [Template]
        private dynamic? OverrideMethod()
        {
            lock ( meta.This )
            {
                return meta.Proceed();
            }
        }
    }
}
