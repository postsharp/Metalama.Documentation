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
            System.Threading.Thread.Sleep(2000);
            return 0;
        }
        [TimeIt]
        public static int SimulatedDelay2()
        {
            System.Threading.Thread.Sleep(3000);
            return 0;
        }

        public static void Main(string[] args)
        {
            SimulatedDelay1();
            SimulatedDelay2();
        }
    }
}
