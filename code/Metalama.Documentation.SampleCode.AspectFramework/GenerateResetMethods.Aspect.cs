// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

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
                builder.Advice.IntroduceMethod(
                    builder.Target,
                    nameof(this.Reset),
                    args: new { field = field, T = field.Type },
                    buildMethod: m =>
                    {
                        m.Name = "Reset" + CamelCase( field.Name );
                    } );
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
            field.Invokers.Final.SetValue( meta.This, default(T) );
        }
    }
}