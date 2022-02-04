---
uid: Metalama.Framework.Metrics
summary: *content
---

This namespace allows you to consume and implement metrics.

To consume a metric, start from a declaration, call the extension method <xref:Metalama.Framework.Metrics.MetricsExtensions.Metrics%2A?text=MetricsExtensions.Metric>, then call <xref:Metalama.Framework.Metrics.Metrics%601.Get%2A>.


## Class Diagram

```mermaid
classDiagram
    
    class IMeasurable {
        
    }

    class IDeclaration {

    }

    class IMetric~TMeasurable~ {

    }

    class YourCustomMetric {

    }

     class YourCustomMetricProvider {
        <<sdk>>

    }

    class IMetricProvider~TMetric~ {
        GetMetric(IMeasurable)
    }

    class Metrics~TMeasurable~ {
        Get()
    }

    class MetricsExtensions    {
        <<extension class>>
        Metrics()$
    }

    MetricsExtensions --> IMeasurable : extension methods
    MetricsExtensions --> Metrics : exposes
    Metrics --> IMetric~TMeasurable~ : exposes

    IDeclaration --|> IMeasurable : implements

    YourCustomMetric --<| IMetric~TMeasurable~ : implements
    YourCustomMetricProvider --<| IMetricProvider~TMetric~ : implements
    YourCustomMetricProvider --> YourCustomMetric : computes

    IMetricProvider~TMetric~ --> IMetric~TMeasurable~ : computes

    IMetric~TMeasurable~ --> IMeasurable : applies to

   
```


## Namespace members