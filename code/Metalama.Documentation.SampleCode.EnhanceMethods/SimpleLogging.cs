using System;

namespace Doc.SimpleLog
{
    public class SimpleLoggingDemo
    {
        [SimpleLog]
        public static void SayHello(string name)
        {
            Console.WriteLine($"Hello {name}");
        }
        public static void Main(string[] args)
        {
            SayHello("Gael");
        }
    }
}
