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
      for (var i = 0;; i++)
      {
        try
        {
          //Simulating an exchange
          //Where sometimes the rate is not available
          int n = new Random(5).Next(20);
          if (n % 2 == 0)
            return 0.5345;
          else
            throw new Exception("Value is not available");
        }
        catch (Exception e)when (i < 3)
        {
          Console.WriteLine($"{e.Message}. Retrying in 100 ms.");
          Thread.Sleep(100);
        }
      }
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