// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Patterns.Caching.Aspects;
using System;

namespace Doc.GettingStarted
{
    public sealed class CloudCalculator
    {
        public int OperationCount { get; private set; }

        [Cache]
        public int Add( int a, int b )
        {
            Console.WriteLine( "Doing some very hard work." );

            this.OperationCount++;

            return a + b;
        }
    }
}