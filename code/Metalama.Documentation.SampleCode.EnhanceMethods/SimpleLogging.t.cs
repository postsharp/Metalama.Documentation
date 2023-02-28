using System;
namespace Doc.SimpleLog
{
    public class SimpleLoggingDemo
    {
        [SimpleLog]
        public static void SayHello(string name)
        {
            Console.WriteLine($"Simply logging a method...");
            Console.WriteLine($"Hello {name}");
            return;
        }
        public static void Main(string[] args)
        {
            SayHello("Gael");
        }
    }
}