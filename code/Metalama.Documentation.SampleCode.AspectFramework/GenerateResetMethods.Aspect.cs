using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.Linq;

namespace Doc.GenerateResetMethods
{
    public class GenerateResetMethodsAttribute : TypeAspect
    {
        public override void BuildAspect( IAspectBuilder<INamedType> builder )
        {
            base.BuildAspect( builder );

            foreach ( var field in builder.Target.FieldsAndProperties.Where( f => f.Writeability != Writeability.None ) )
            {
                var method = builder.Advice.IntroduceMethod( builder.Target, nameof( Reset ), args: new { field = field, T = field.Type } );
                method.Name = "Reset" + CamelCase( field.Name );

            }
        }

        private static string CamelCase( string s )
        {
            s = s.TrimStart( '_' );

            return s[0].ToString().ToUpperInvariant() + s.Substring( 1 );

        }

        [Template]
        public void Reset<[CompileTime] T>( IFieldOrProperty field )
        {
            field.Invokers.Final.SetValue( meta.This, default( T ) );
        }
    }
}
