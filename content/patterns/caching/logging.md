---
uid: caching-logging-interaction
title: "Troubleshooting Caching"
product: "postsharp"
categories: "Metalama;AOP;Metaprogramming"
---
# Troubleshooting Caching

If you need to troubleshoot the caching aspect, you can enable logging for this feature. The procedure is different whether or not you are using Metalama Logging in your application.


## Enabling logging of caching with Metalama Logging

If you use Metalama Logging for logging, you can enable detailed logging of the caching aspect by enabling the <xref:Flashtrace.LoggingRoles.Caching> logging role for the whole application or a specific type or namespace. 

The following code shows how to log the details of the caching aspect to the system console for the whole application:

```csharp
LoggingServices.DefaultBackend = new ConsoleLoggingBackend();
LoggingServices.DefaultBackend.DefaultVerbosity.SetMinimalLevel(LogLevel.Debug, LoggingRoles.Caching);
```


## Enabling logging of caching with System.Diagnostics

If you are not using Metalama Logging, you can enable logging of the caching aspect by configuring the <xref:System.Diagnostics.TraceSource> named `Metalama.Cache` in you `app.config` or `web.config` file: 

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.7.2" />
    </startup>
	<system.diagnostics>
		<sources>
			<source name="Metalama.Cache" switchName="Metalama.Cache" >
				<listeners>
					<add name="console" />
				</listeners>
			</source>
		</sources>
		<switches>
			<add name="Metalama.Cache" value="-1" />
		</switches>
		<sharedListeners>
			<add name="console" type="System.Diagnostics.ConsoleTraceListener" initializeData="false"/>
		</sharedListeners>
		<trace autoflush="true" indentsize="4">
			<listeners>
				<add name="console" />
			</listeners>
		</trace>
	</system.diagnostics>
</configuration>
```

## See Also

