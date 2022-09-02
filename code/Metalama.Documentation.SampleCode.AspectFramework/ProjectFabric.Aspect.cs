// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;
using System;
using System.Linq;

namespace Doc.ProjectFabric_
{
    internal class Fabric : ProjectFabric
    {
        public override void AmendProject( IProjectAmender project )
        {
            project.With( p => p.Types.SelectMany( t => t.Methods ) ).AddAspect<Log>();
        }
    }

    public class Log : OverrideMethodAspect
    {
        public override dynamic? OverrideMethod()
        {
            Console.WriteLine( $"Executing {meta.Target.Method}." );

            try
            {
                return meta.Proceed();
            }
            finally
            {
                Console.WriteLine( $"Exiting {meta.Target.Method}." );
            }
        }
    }
}