using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.DependencyInjection;
using Metalama.Framework.DependencyInjection.Implementation;
using Metalama.Framework.Fabrics;
using Microsoft.Extensions.Logging;

#pragma warning disable CS0649, CS8618

[assembly: AspectOrder( typeof(Doc.CustomFramework.LogAttribute), typeof(DependencyAttribute))] 

namespace Doc.CustomFramework
{
    public class LoggerDependencyInjectionFramework : DefaultDependencyInjectionFramework
    {
        public override bool CanHandleDependency( DependencyContext context )
        {
            return context.FieldOrProperty.Type.Is( typeof( ILogger ) );
        }

        protected override DefaultDependencyInjectionStrategy GetStrategy( DependencyContext context )
        {
            return new InjectionStrategy( context );
        }

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

        private class LoggerPullStrategy : DefaultPullStrategy
        {
            public LoggerPullStrategy( DependencyContext context, IFieldOrProperty introducedFieldOrProperty ) : base( context, introducedFieldOrProperty )
            {
            }

            protected override IType ParameterType => 
                ((INamedType) TypeFactory.GetType( typeof(ILogger<>) )).ConstructGenericInstance( this.IntroducedFieldOrProperty.DeclaringType );
        }
    }

    public class Fabric : ProjectFabric
    {
        public override void AmendProject( IProjectAmender amender )
        {
            amender.Project.DependencyInjectionOptions().RegisterFramework( new LoggerDependencyInjectionFramework() );
        }
    }

    public class LogAttribute : OverrideMethodAspect
    {
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