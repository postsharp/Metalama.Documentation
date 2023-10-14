using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Aspects;
using Metalama.Patterns.Caching.Aspects.Helpers;
using System;
using System.Reflection;
using System.Threading;
namespace Doc.Locking
{
  public sealed class CloudService : IDisposable
  {
    // We use barriers to make sure we wait long enough.
    private readonly Barrier _withoutLockBarrier = new(2);
    [Cache(ProfileName = "Locking")]
    public byte[] ReadFileWithLock(string path)
    {
      object? Invoke(object? instance, object? [] args)
      {
        return ((CloudService)instance).ReadFileWithLock_Source((string)args[0]);
      }
      return _cachingService!.GetFromCacheOrExecute<byte[]>(_cacheRegistration_ReadFileWithLock!, this, new object[] { path }, Invoke);
    }
    private byte[] ReadFileWithLock_Source(string path)
    {
      Console.WriteLine("Doing some very hard work.");
      Thread.Sleep(50);
      return new byte[32];
    }
    [Cache]
    public byte[] ReadFileWithoutLock(string path)
    {
      object? Invoke(object? instance, object? [] args)
      {
        return ((CloudService)instance).ReadFileWithoutLock_Source((string)args[0]);
      }
      return _cachingService!.GetFromCacheOrExecute<byte[]>(_cacheRegistration_ReadFileWithoutLock!, this, new object[] { path }, Invoke);
    }
    private byte[] ReadFileWithoutLock_Source(string path)
    {
      Console.WriteLine("Doing some very hard work.");
      // Simulate a long-running operation.
      this._withoutLockBarrier.SignalAndWait();
      return new byte[32];
    }
    public void Dispose() => this._withoutLockBarrier.Dispose();
    private static readonly CachedMethodMetadata _cacheRegistration_ReadFileWithLock;
    private static readonly CachedMethodMetadata _cacheRegistration_ReadFileWithoutLock;
    private ICachingService _cachingService;
    static CloudService()
    {
      CloudService._cacheRegistration_ReadFileWithoutLock = CachedMethodMetadata.Register(RunTimeHelpers.ThrowIfMissing(typeof(CloudService).GetMethod("ReadFileWithoutLock", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string) }, null)!, "CloudService.ReadFileWithoutLock(string)"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, true);
      CloudService._cacheRegistration_ReadFileWithLock = CachedMethodMetadata.Register(RunTimeHelpers.ThrowIfMissing(typeof(CloudService).GetMethod("ReadFileWithLock", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string) }, null)!, "CloudService.ReadFileWithLock(string)"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = "Locking", SlidingExpiration = null }, true);
    }
    public CloudService(ICachingService? cachingService = default)
    {
      this._cachingService = cachingService ?? throw new System.ArgumentNullException(nameof(cachingService));
    }
  }
}