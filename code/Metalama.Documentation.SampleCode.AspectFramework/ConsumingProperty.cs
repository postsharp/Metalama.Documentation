using Metalama.Framework.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metalama.Documentation.SampleCode.AspectFramework.ConsumingProperty
{
    public class Log : OverrideMethodAspect
    {
        public string? Category { get; set; }

        public override dynamic? OverrideMethod()
        {
            if (!meta.Target.Project.TryGetProperty("DefaultLogCategory", out var defaultCategory))
            {
                defaultCategory = "Default";
            }

            Console.WriteLine($"{ this.Category ?? defaultCategory }: Executing {meta.Target.Method}.");

            return meta.Proceed();
        }
    }
}
