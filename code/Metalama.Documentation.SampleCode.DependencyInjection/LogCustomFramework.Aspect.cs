// This is public domain Metalama sample code.

using Doc.LogCustomFramework;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Extensions.DependencyInjection;
using Metalama.Extensions.DependencyInjection.Implementation;
using Microsoft.Extensions.Logging;
using Metalama.Framework.Diagnostics;

#pragma warning disable CS0649, CS8618

[assembly: AspectOrder( typeof(LogAttribute), typeof(DependencyAttribute) )]

namespace Doc.LogCustomFramework
{
    // Our logging aspect.
    public class LogAttribute : OverrideMethodAspect
    {
        // Defines the dependency consumed by the aspect. It will be handled by LoggerDependencyInjectionFramework.
        // Note that the aspect does not need to know the implementation details of the dependency injection framework.
        [IntroduceDependency]
        private readonly ILogger _logger;

        public override dynamic? OverrideMethod()
        {
            try
            {
                this._logger.LogWarning( $"{meta.Target.Method} started." );

                return meta.Proceed();
            }
            finally
            {
                this._logger.LogWarning( $"{meta.Target.Method} completed." );
            }
        }
    }
}