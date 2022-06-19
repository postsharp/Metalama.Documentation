// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

using System;
using System.Threading;

namespace Doc.IntroduceMethod
{
    [ToString]
    internal class MyClass { }

    internal static class IdGenerator
    {
        private static int _nextId;

        public static int GetId() => Interlocked.Increment( ref _nextId );
    }

    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine( new MyClass().ToString() );
            Console.WriteLine( new MyClass().ToString() );
            Console.WriteLine( new MyClass().ToString() );
        }
    }
}