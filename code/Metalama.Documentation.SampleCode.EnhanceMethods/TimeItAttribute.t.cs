using Metalama.Framework.Aspects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Doc.TimeIt
{
    public class TimeItDemo
    {
        [TimeIt]
        public static int SimulatedDelay1()
        {
            Console.WriteLine("Started executing TimeItDemo.SimulatedDelay1()");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int result;
            System.Threading.Thread.Sleep(2000);
            result = 0;
            sw.Stop();
            Console.WriteLine("Finished executing TimeItDemo.SimulatedDelay1()");
            Console.WriteLine($"Time taken :{sw.ElapsedMilliseconds} ms");
            return result;
        }
        [TimeIt]
        public static int SimulatedDelay2()
        {
            Console.WriteLine("Started executing TimeItDemo.SimulatedDelay2()");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int result;
            System.Threading.Thread.Sleep(3000);
            result = 0;
            sw.Stop();
            Console.WriteLine("Finished executing TimeItDemo.SimulatedDelay2()");
            Console.WriteLine($"Time taken :{sw.ElapsedMilliseconds} ms");
            return result;
        }
        public static void Main(string[] args)
        {
            SimulatedDelay1();
            SimulatedDelay2();
        }
    }
}