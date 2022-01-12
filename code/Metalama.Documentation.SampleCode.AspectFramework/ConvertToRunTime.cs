// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.ConvertToRunTime
{
    internal class TargetCode
    {
        [ConvertToRunTimeAspect]
        private void Method( string a, int c, DateTime e )
        {
            Console.WriteLine( $"Method({a}, {c}, {e})" );
        }
    }
}