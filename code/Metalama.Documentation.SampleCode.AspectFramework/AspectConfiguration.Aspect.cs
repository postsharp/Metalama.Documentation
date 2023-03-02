
using Metalama.Framework.Aspects;
using System;

namespace Doc.AspectConfiguration
{
    // The aspect itself, consuming the configuration.
    public class LogAttribute : OverrideMethodAspect
    {
        public string? Category { get; set; }

        public override dynamic? OverrideMethod()
        {
            var defaultCategory = meta.Target.Project.LoggingOptions().DefaultCategory;

            Console.WriteLine( $"{this.Category ?? defaultCategory}: Executing {meta.Target.Method}." );

            return meta.Proceed();
        }
    }
}