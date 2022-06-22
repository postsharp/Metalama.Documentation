using Metalama.Framework.Aspects;
using Metalama.Framework.DependencyInjection;

#pragma warning disable CS0649, CS8618

[assembly: AspectOrder( typeof(Doc.DependencyInjection.LogAttribute), typeof(DependencyAttribute))] 

namespace Doc.DependencyInjection
{

    public class LogAttribute : OverrideMethodAspect
    {
        [IntroduceDependency]
        private readonly IMessageWriter _messageWriter;

        public override dynamic? OverrideMethod()
        {
            try
            {
                this._messageWriter.Write( $"{meta.Target.Method} started." );

                return meta.Proceed();
            }
            finally
            {
                this._messageWriter.Write( $"{meta.Target.Method} completed." );
            }

        }
    }


}