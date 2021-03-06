// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using System.Linq;

namespace Doc.ProgrammaticInitializer
{
    internal class AddMethodNamesAspect : TypeAspect
    {
        public override void BuildAspect( IAspectBuilder<INamedType> builder )
        {
            // Create an expression that contains the an array with all method names.
            var expressionBuilder = new ExpressionBuilder();
            expressionBuilder.AppendVerbatim( "new string[] {" );
            var i = 0;

            foreach ( var methodName in builder.Target.Methods.Select( m => m.Name ).Distinct() )
            {
                if ( i > 0 )
                {
                    expressionBuilder.AppendVerbatim( ", " );
                }

                expressionBuilder.AppendLiteral( methodName );

                i++;
            }

            expressionBuilder.AppendVerbatim( "}" );

            // Introduce a field and initialize it to that array.
            builder.Advice.IntroduceField(
                builder.Target,
                "_methodNames",
                typeof(string[]),
                buildField: f => f.InitializerExpression = expressionBuilder.ToExpression() );
        }
    }
}