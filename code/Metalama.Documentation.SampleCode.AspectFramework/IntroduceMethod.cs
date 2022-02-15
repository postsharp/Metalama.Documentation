using System;

namespace Doc.IntroduceMethod;

[ToString]
internal class MyClass
{
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