// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Microsoft.Extensions.Hosting;
using System;
using Metalama.Documentation.Helpers.ConsoleApp;

namespace Doc.GettingStarted
{
    public sealed class ConsoleMain : IConsoleMain
    {
        private readonly CloudCalculator _cloudCalculator;
        
        public ConsoleMain( CloudCalculator cloudCalculator )
        {
            this._cloudCalculator = cloudCalculator;
        }

        public void Execute()
        {
            for ( var i = 0; i < 3; i++ )
            {
                var value = this._cloudCalculator.Add( 1, 1 );
                Console.WriteLine( $"CloudCalculator returned {value}." );
            }

            Console.WriteLine( $"In total, CloudCalculator performed {this._cloudCalculator.OperationCount} operation(s)." );
        }
    }
}