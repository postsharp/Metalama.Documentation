// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.IO;
using System.Linq;

namespace Doc.DynamicCodeModel
{
    internal class LogAttribute : OverrideMethodAspect
    {
        public override dynamic? OverrideMethod()
        {
            var loggerField = meta.Target.Type.FieldsAndProperties.Where( x => x.Type.Is( typeof(TextWriter) ) )
                .Single();

            loggerField.Invokers.Final.GetValue( meta.This ).WriteLine( $"Executing {meta.Target.Method}." );

            return meta.Proceed();
        }
    }
}