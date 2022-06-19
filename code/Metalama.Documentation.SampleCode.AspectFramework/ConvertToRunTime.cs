// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

using System;

namespace Doc.ConvertToRunTime
{
    internal class Foo
    {
        [ConvertToRunTimeAspect]
        private void Bar( string a, int c, DateTime e )
        {
            Console.WriteLine( $"Method({a}, {c}, {e})" );
        }
    }
}