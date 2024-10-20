using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Aspects;
using Metalama.Patterns.Caching.Aspects.Helpers;
using System;
using System.IO;
using System.Reflection;
namespace Doc.Formatter;
public sealed class FileSystem
{
  public int OperationCount { get; private set; }
  [Cache]
  public byte[] ReadAll(FileInfo file)
  {
    static object? Invoke(object? instance, object? [] args)
    {
      return ((FileSystem)instance).ReadAll_Source((FileInfo)args[0]);
    }
    return _cachingService.GetFromCacheOrExecute<byte[]>(_cacheRegistration_ReadAll, this, new object[] { file }, Invoke);
  }
  private byte[] ReadAll_Source(FileInfo file)
  {
    this.OperationCount++;
    Console.WriteLine("Reading the whole file.");
    return new byte[100 + this.OperationCount];
  }
  private static readonly CachedMethodMetadata _cacheRegistration_ReadAll;
  private ICachingService _cachingService;
  static FileSystem()
  {
    _cacheRegistration_ReadAll = CachedMethodMetadata.Register(typeof(FileSystem).GetMethod("ReadAll", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(FileInfo) }, null).ThrowIfMissing("FileSystem.ReadAll(FileInfo)"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, true);
  }
  public FileSystem(ICachingService? cachingService = default)
  {
    this._cachingService = cachingService ?? throw new System.ArgumentNullException(nameof(cachingService));
  }
}