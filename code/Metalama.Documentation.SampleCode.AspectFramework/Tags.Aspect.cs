using System;
using Caravela.Framework.Aspects;
using Caravela.Framework.Code;

namespace Caravela.Documentation.SampleCode.AspectFramework.Tags
{
    class TagsAspect : MethodAspect
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
