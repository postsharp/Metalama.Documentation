using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Extensions.DependencyInjection;
using Metalama.Extensions.DependencyInjection.Implementation;
using Metalama.Framework.Fabrics;
using Microsoft.Extensions.Logging;

#pragma warning disable CS0649, CS8618

[assembly: AspectOrder( typeof(Doc.LogCustomFramework.LogAttribute), typeof(DependencyAttribute))] 

namespace Doc.LogCustomFramework
{
    public class LoggerDependencyInjectionFramework : DefaultDependencyInjectionFramework
    {
        // Returns true if we want to handle this dependency, i.e. if is a dependency of type ILogger.
        public override bool CanHandleDependency( DependencyContext context )
        {
            return context.FieldOrProperty.Type.Is( typeof( ILogger ) );
        }

        // Return our own customized strategy.
        protected override DefaultDependencyInjectionStrategy GetStrategy( DependencyContext context )
        {
            return new InjectionStrategy( context );
        }

        // Our customized injection strategy. Decides how to create the field or property.
        // We actually have no customization except that we return a customized pull strategy instead of the default one.
        private class InjectionStrategy : DefaultDependencyInjectionStrategy
        {
            public InjectionStrategy( DependencyContext context ) : base( context )
            {
            }

            protected override IPullStrategy GetPullStrategy( IFieldOrProperty introducedFieldOrProperty )
            {
                return new LoggerPullStrategy( this.Context, introducedFieldOrProperty );
            }
        }

        // Our customized pull strategy. Decides how to assign the field or property from the constructor.
        private class LoggerPullStrategy : DefaultPullStrategy
        {
            public LoggerPullStrategy( DependencyContext context, IFieldOrProperty introducedFieldOrProperty ) : base( context, introducedFieldOrProperty )
            {
            }

            // Returns the type of the required or created constructor parameter. We return ILogger<T> where T is the declaring type
            // (The default behavior would return just ILogger).
            protected override IType ParameterType => 
                ((INamedType) TypeFactory.GetType( typeof(ILogger<>) )).WithTypeArguments( this.IntroducedFieldOrProperty.DeclaringType );
        }
    }

    // A project fabric that registers our framework.
    public class Fabric : ProjectFabric
    {
        public override void AmendProject( IProjectAmender amender )
        {
            amender.Project.DependencyInjectionOptions().RegisterFramework( new LoggerDependencyInjectionFramework() );
        }
    }

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