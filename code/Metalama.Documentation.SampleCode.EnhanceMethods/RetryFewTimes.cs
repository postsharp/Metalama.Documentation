using System;
using System.Threading;
using Metalama.Framework.Aspects;

namespace Doc.RetryFew
{
    public class Exchange
    {

        [RetryFewTimes]
        public double GetExchangeRate()
        {
            //Simulating an exchange 
            //Where sometimes the rate is not available
            int n = new Random(5).Next(20);
            if (n % 2 == 0)
                return 0.5345;
            else throw new Exception("Value is not available");
        }

    }
    public class RetryFewTimesDemo
    {
        public static void Main(string[] args)
        {
            Exchange x = new Exchange();
            var rate = x.GetExchangeRate();
            Console.WriteLine(rate);
        }
    }
}
