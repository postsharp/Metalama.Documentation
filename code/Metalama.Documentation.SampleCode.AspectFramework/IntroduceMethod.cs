using System;

namespace Doc.IntroduceMethod;

[ToString]
class MyClass
{

}

class Program
{
    static void Main()
    {
        Console.WriteLine( new MyClass().ToString() );
        Console.WriteLine( new MyClass().ToString() );
        Console.WriteLine( new MyClass().ToString() );
    }
}


