using Metalama.Framework.Aspects;

namespace Doc.ParseExpression
{
    internal class LogAttribute : OverrideMethodAspect
    {
        public override dynamic? OverrideMethod()
        {
            var logger = meta.ParseExpression( "this._logger" );

            logger.Value?.WriteLine( $"Executing {meta.Target.Method}." );

            return meta.Proceed();
        }
    }
}