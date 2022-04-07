using Metalama.Framework.Aspects;
using System;

namespace Doc.SimpleLogging
{
    public class SimpleLogAttribute : OverrideMethodAspect
    {
        public override dynamic? OverrideMethod()
        {
            Console.WriteLine( $"Entering {meta.Target.Method}" );

            try
            {
                return meta.Proceed();
            }
            finally
            {
                Console.WriteLine( $"Leaving {meta.Target.Method}" );
            }
        }
    }
}