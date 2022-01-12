using System;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Metalama.Documentation.SampleCode.AspectFramework.Tags
{
    internal class TagsAspect : MethodAspect
    {
        public override void BuildAspect( IAspectBuilder<IMethod> builder )
        {
            builder.Advices.OverrideMethod(
                builder.Target, 
                nameof(OverrideMethod), 
                tags: new() { ["ParameterCount"] = builder.Target.Parameters.Count });
        }

        [Template]
        private dynamic? OverrideMethod()
        {
            Console.WriteLine($"This method has {meta.Tags["ParameterCount"]} parameters.");
            return meta.Proceed();
        }
    }
}
