---
uid: caching-troubleshooting
---
# Troubleshooting Metalama Caching

If you need to troubleshoot the caching aspect, you can enable verbose logging for this feature. 

> [!NOTE]
> Metalama Caching uses an intermediate logging layer called _Flashtrace_. We do not recommend using Flashtrace in your applications for other scenarios at the moment.


## With dependency injection

If your application uses dependency injeciton, add the .NET logging service as usual using the <xref:Microsoft.Extensions.DependencyInjection.LoggingServiceCollectionExtensions.AddLogging*> extension method, and supply a delegate that calls <xref:Microsoft.Extensions.Logging.LoggingBuilderExtensions.SetMinimumLevel*> to set the level to `Debug`.

This is shown in the following code snippet:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/Logging/Logging.Program.cs marker="AddLogging"]


### Example: logging of caching with dependency injection

The following is an update the _getting started_ example, with logging enabled. You can observe detailed logging in the program output.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/Logging/Logging.cs]

## Without dependency injection

The <xref:Metalama.Patterns.Caching.CachingService> object and other components of Metalama Caching acquire the <xref:Flashtrace.IFlashtraceLoggerFactory> interface through the <xref:System.IServiceProvider>. To enable logging, you must provide an implementation of <xref:System.IServiceProvider> that supports the <xref:Flashtrace.IFlashtraceLoggerFactory> interface, then pass this <xref:System.IServiceProvider> to the <xref:Metalama.Patterns.Caching.CachingService.Create*?text=CachingService.Create> method. You can use the <xref:Flashtrace.Loggers.TraceSourceLoggerFactory> class, which relies on the <xref:System.Diagnostics.TraceSource> system class.

If you want to implement the <xref:Flashtrace.IFlashtraceLoggerFactory> yourself, you will find the <xref:Flashtrace.Loggers.SimpleFlashtraceLogger> useful.
