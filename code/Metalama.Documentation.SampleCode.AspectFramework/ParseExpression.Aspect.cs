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