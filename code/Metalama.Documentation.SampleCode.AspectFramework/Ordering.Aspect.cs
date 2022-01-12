// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using Metalama.Documentation.SampleCode.AspectFramework.Ordering;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Linq;

[assembly: AspectOrder( typeof(Aspect1), typeof(Aspect2) )]

namespace Metalama.Documentation.SampleCode.AspectFramework.Ordering
{
    internal class Aspect1 : TypeAspect
    {
        public override void BuildAspect( IAspectBuilder<INamedType> builder )
        {
            foreach ( var m in builder.Target.Methods )
            {
                builder.Advices.OverrideMethod( m, nameof(this.Override) );
            }
        }

        [Introduce]
        public static void IntroducedMethod1()
        {
            Console.WriteLine( "Method introduced by Aspect1." );
        }

        [Template]
        private dynamic? Override()
        {
            Console.WriteLine(
                $"Executing Aspect1 on {meta.Target.Method.Name}. Methods present before applying Aspect1: "
                + string.Join( ", ", meta.Target.Type.Methods.Select( m => m.Name ).ToArray() ) );

            return meta.Proceed();
        }
    }

    internal class Aspect2 : TypeAspect
    {
        public override void BuildAspect( IAspectBuilder<INamedType> builder )
        {
            foreach ( var m in builder.Target.Methods )
            {
                builder.Advices.OverrideMethod( m, nameof(this.Override) );
            }
        }

        [Introduce]
        public static void IntroducedMethod2()
        {
            Console.WriteLine( "Method introduced by Aspect2." );
        }

        [Template]
        private dynamic? Override()
        {
            Console.WriteLine(
                $"Executing Aspect2 on {meta.Target.Method.Name}. Methods present before applying Aspect2: "
                + string.Join( ", ", meta.Target.Type.Methods.Select( m => m.Name ).ToArray() ) );

            return meta.Proceed();
        }
    }
}