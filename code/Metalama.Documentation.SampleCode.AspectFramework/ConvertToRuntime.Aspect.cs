using System;
using System.Linq;
using Metalama.Framework.Aspects;

namespace Metalama.Documentation.SampleCode.AspectFramework.ConvertToRunTime
{
    internal class ConvertToRunTimeAspect : OverrideMethodAspect
    {
        public override dynamic? OverrideMethod()
        {
            var parameterNamesCompileTime = meta.Target.Parameters.Select(p => p.Name).ToList();
            var parameterNames = meta.RunTime(parameterNamesCompileTime);
            var buildTime = meta.RunTime(new Guid("13c139ea-42f5-4726-894d-550406357978"));
            var parameterType = meta.RunTime( meta.Target.Parameters[0].Type.ToType() );

            return null;
        }
    }
}
