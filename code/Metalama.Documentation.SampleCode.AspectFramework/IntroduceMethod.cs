using System;
using System.Threading;

namespace Doc.IntroduceMethod
{

    [ToString]
    internal class MyClass
    {
    }

    internal static class IdGenerator
    {
        static int _nextId;
        public static int GetId() => Interlocked.Increment(ref _nextId);
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