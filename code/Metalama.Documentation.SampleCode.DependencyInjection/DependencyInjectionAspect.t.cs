using System;
using Metalama.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
#pragma warning disable CS0169 // Field is never used
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
namespace Doc.DependencyInjectionAspect
{
  public class DependencyInjectionAspect
  {
    // This dependency will be optional because the field is nullable.
    [Dependency]
    private ILogger? _logger;
    // This dependency will be required because the field is non-nullable.
    [Dependency]
    private IHostEnvironment _environment;
    [Dependency(IsLazy = true)]
    private IHostApplicationLifetime _lifetime
    {
      get
      {
        return _lifetimeCache ??= _lifetimeFunc!.Invoke();
      }
      set
      {
        throw new NotSupportedException("Cannot set '_lifetime' because of the dependency aspect.");
      }
    }
    public void DoWork()
    {
      this._logger?.LogDebug("Doing some work.");
      if (!this._environment.IsProduction())
      {
        this._lifetime.StopApplication();
      }
    }
    private IHostApplicationLifetime? _lifetimeCache;
    private Func<IHostApplicationLifetime> _lifetimeFunc;
    public DependencyInjectionAspect(Func<IHostApplicationLifetime>? lifetime = default, IHostEnvironment? environment = default, ILogger<DependencyInjectionAspect> logger = default)
    {
      this._lifetimeFunc = lifetime ?? throw new System.ArgumentNullException(nameof(lifetime));
      this._environment = environment ?? throw new System.ArgumentNullException(nameof(environment));
      this._logger = logger;
    }
  }
}