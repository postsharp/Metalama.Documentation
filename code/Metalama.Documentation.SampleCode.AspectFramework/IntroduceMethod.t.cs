using System;
using System.Runtime.CompilerServices;

namespace Doc.IntroduceMethod;

[ToString]
class MyClass
{


    public override string ToString()
    {
        return $"{GetType().Name} Id={RuntimeHelpers.GetHashCode(this)}";
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine(new MyClass().ToString());
        Console.WriteLine(new MyClass().ToString());
        Console.WriteLine(new MyClass().ToString());
    }
}
