using System;
namespace Doc.SimpleLog
{
  public class Program
  {
    [Log]
    public static void SayHello(string name)
    {
      Console.WriteLine($"Simply logging a method...");
      Console.WriteLine($"Hello {name}");
      return;
    }
    public static void Main()
    {
      SayHello("Your Majesty");
    }
  }
}