
// See https://aka.ms/new-console-template for more information

using Metalama.Documentation.QuickStart;
using System.Diagnostics;
namespace DebugDemo
{
    public class Demo
    {
        public static void Main(string[] args)
        {
            Debugger.Break();
            DoThis();
        }

        [Log]
        public static void DoThis()
        {
            Console.WriteLine("Doing this");
            DoThat();
        }
        [Log]
        public static void DoThat()
        {
            Console.WriteLine("Doing that");
        }
    }
}
