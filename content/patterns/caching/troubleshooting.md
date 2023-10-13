---
uid: caching-troubleshooting
---
# Troubleshooting Metalama Caching

If you need to troubleshoot the caching feature, you can enable verbose logging.

> [!NOTE]
> Metalama Caching employs an intermediate logging layer, _Flashtrace_. However, we do not currently recommend using Flashtrace in your applications for other scenarios.

## With Dependency Injection

If your application utilizes dependency injection, add the .NET logging service as usual using the <xref:Microsoft.Extensions.DependencyInjection.LoggingServiceCollectionExtensions.AddLogging*> extension method. Then, supply a delegate that calls <xref:Microsoft.Extensions.Logging.LoggingBuilderExtensions.SetMinimumLevel*> to set the level to `Debug`.

The following code snippet illustrates this:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/Logging/Logging.Program.cs marker="AddLogging"]

### Example: Logging of Caching with Dependency Injection

The following is an update to the _getting started_ example, with logging enabled. You can observe detailed logging in the program output.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/Logging/Logging.cs]

## Without dependency injection

The <xref:Metalama.Patterns.Caching.CachingService> object and other components of Metalama Caching acquire the <xref:Flashtrace.IFlashtraceLoggerFactory> interface through the <xref:System.IServiceProvider>. To enable logging, you must provide an implementation of <xref:System.IServiceProvider> that supports the <xref:Flashtrace.IFlashtraceLoggerFactory> interface. Then, pass this <xref:System.IServiceProvider> to the <xref:Metalama.Patterns.Caching.CachingService.Create*?text=CachingService.Create> method. You can use the <xref:Flashtrace.Loggers.TraceSourceLoggerFactory> class, which relies on the <xref:System.Diagnostics.TraceSource> system class.

If you plan to implement the <xref:Flashtrace.IFlashtraceLoggerFactory> yourself, you may find the <xref:Flashtrace.Loggers.SimpleFlashtraceLogger> useful.
