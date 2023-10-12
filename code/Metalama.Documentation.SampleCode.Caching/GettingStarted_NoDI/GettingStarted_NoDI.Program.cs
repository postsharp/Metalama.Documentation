// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Patterns.Caching;
using System;

namespace Doc.GettingStarted_NoDI
{
    internal static class Program
    {
        public static void Main()
        {
            // Set up the default caching service.
            CachingService.Default = CachingService.Create();

            // Execute the program.
            for ( var i = 0; i < 3; i++ )
            {
                var value = CloudCalculator.Add( 1, 1 );
                Console.WriteLine( $"CloudCalculator returned {value}." );
            }

            Console.WriteLine( $"In total, CloudCalculator performed {CloudCalculator.OperationCount} operation(s)." );
        }
    }
}