using System;
using System.Runtime.CompilerServices;

namespace Doc.IntroduceMethod;

[ToString]
internal class MyClass
{
    public override string ToString()
    {
        return $"{GetType().Name} Id={RuntimeHelpers.GetHashCode(this)}";
    }
}

internal class Program
{
    private static void Main()
    {
        Console.WriteLine(new MyClass().ToString());
        Console.WriteLine(new MyClass().ToString());
        Console.WriteLine(new MyClass().ToString());
    }
}