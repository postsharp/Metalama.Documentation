using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Aspects;
using Metalama.Patterns.Caching.Aspects.Helpers;
using System;
using System.Reflection;
namespace Doc.GettingStarted_NoDI
{
  public static class CloudCalculator
  {
    public static int OperationCount { get; private set; }
    [Cache]
    public static int Add(int a, int b)
    {
      static object? Invoke(object? instance, object? [] args)
      {
        return CloudCalculator.Add_Source((int)args[0], (int)args[1]);
      }
      return ((ICachingService)CachingService.Default).GetFromCacheOrExecute<int>(_cacheRegistration_Add!, null, new object[] { a, b }, Invoke);
    }
    private static int Add_Source(int a, int b)
    {
      Console.WriteLine("Doing some very hard work.");
      OperationCount++;
      return a + b;
    }
    private static readonly CachedMethodMetadata _cacheRegistration_Add;
    static CloudCalculator()
    {
      CloudCalculator._cacheRegistration_Add = CachedMethodMetadata.Register(RunTimeHelpers.ThrowIfMissing(typeof(CloudCalculator).GetMethod("Add", BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(int), typeof(int) }, null)!, "CloudCalculator.Add(int, int)"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, false);
    }
  }
}