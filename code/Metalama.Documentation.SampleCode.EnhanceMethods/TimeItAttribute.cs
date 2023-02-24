using System;
using System.Threading;

namespace Doc.TimeIt
{

    public class TimeItDemo
    {
        [TimeIt]
        public static int SimulatedDelay1()
        {
            //Simulating a random delay between 500 ms to 2 secs
            Thread.Sleep(new Random().Next(500,2000));
            return 0;
        }
        [TimeIt]
        public static int SimulatedDelay2()
        {
            //Simulating a random delay of 3 secs
            Thread.Sleep(3000);
            return 0;
        }

        public static void Main(string[] args)
        {
            SimulatedDelay1();
            SimulatedDelay2();
        }
    }
}
