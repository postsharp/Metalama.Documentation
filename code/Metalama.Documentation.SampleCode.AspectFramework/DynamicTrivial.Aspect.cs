// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

using Metalama.Framework.Aspects;

namespace Doc.DynamicTrivial
{
    internal class LogAttribute : OverrideMethodAspect
    {
        public override dynamic? OverrideMethod()
        {
            meta.This._logger.WriteLine( $"Executing {meta.Target.Method}." );

            return meta.Proceed();
        }
    }
}