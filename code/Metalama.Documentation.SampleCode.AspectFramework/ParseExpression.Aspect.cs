// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code.SyntaxBuilders;

namespace Doc.ParseExpression
{
    internal class LogAttribute : OverrideMethodAspect
    {
        public override dynamic? OverrideMethod()
        {
            var logger = ExpressionFactory.Parse( "this._logger" );

            logger.Value?.WriteLine( $"Executing {meta.Target.Method}." );

            return meta.Proceed();
        }
    }
}