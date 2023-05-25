---
uid: license-credits
---

# Inspecting license credit consumption

The Metalama licensing model is based on the number of _credits_ that any project can use. Each edition of Metalama has a different number of credits included, except Metalama Ultimate, which has no limit. Credits are consumed when a project uses an aspect or an aspect library. It can be difficult to manually keep track of the list of aspects in use, so we have built a tool to help you with this task: `metalama license credits`.

To install the `metalama` command-line tool, see <xref:dotnet-tool>.


> [!NOTE]
> It is important to remember that credit limits are enforced on a per-project basis. For each C# project, the number of credits is determined by the number of aspect classes or libraries used by this project, regardless of the aspects used by referenced projects. So if two projects in a solution use two completely disjoint sets of three aspects, the number of credits required by this solution is three and not six.


## When is credit data collected?

The `metalama license credits` tool creates reports based on files created during the build. Writing these files takes some small performance overhead, so it is turned off by default. Collecting credit data is enabled in the following circumstances:

* During the trial period, i.e., when using an evaluation license.
* When there are not enough license credits to cover the needs of a project, i.e., in case of licensing issues.
* When the MSBuild property `MetalamaWriteLicenseCreditData` is set to `True`.

## Determining which Metalama edition to buy

At the end of your trial period, we hope you will decide to continue using Metalama and will need to see if Metalama Free is enough for you or which paying edition you should purchase.

To help you in this decision, follow this procedure:

1. Install the `metalama` command-line tool as described in <xref:dotnet-tool>.
2. Rebuild all projects that use Metalama using `dotnet build -t:rebuild`. 
3. Use the command `metalama license credits projects` and ensure all projects are listed there. If any is missing, go back to the previous point.
4. Use the command `metalama license credits summary`.

You will see an output similar to this:

```text
Considering builds from Thursday, 18 May 2023 00:00. 
Use -d, -w or -h option to change the time horizon.

------------------------------+--------------------------+------------------------------
  Maximum Credit Requirements | Maximum Metalama Version | Maximum Metalama Build Date 
------------------------------|--------------------------|------------------------------
  3                           | 2023.1.2.83-dev-debug    | 05/23/2023                 
 
```

It includes the following pieces of information:

* The _Maximum Credit Requirements_ number indicates the number of credits required to build your project. You need a Metalama edition that includes at least that number of credits. To see the credits allotments of editions, see https://www.postsharp.net/metalama/pricing.
* The _Maximum Metalama Build Date_ indicates the build date of the latest Metalama version used by your projects. You will need a maintenance subscription that ends after this date.


## Seeing how aspects and aspect libraries are consuming credits

To see which aspects or aspect libraries consume credits, use the `metalama license credits summary` command.

Here is an example output:


```text
Considering builds from Thursday, 18 May 2023 00:00. 
Use -d, -w or -h option to change the time horizon.

------------------------------------------------------------------------+-----------+-------------+-----------
 Item Name                                                             | Item Kind | Credit Cost | Projects |
-----------------------------------------------------------------------|-----------|-------------|----------┤
 CacheAttribute                                                        | UserClass | 1.0         | 5        |
 CacheKeyMemberAttribute                                               | UserClass | 1.0         | 2        |
 CloneableAttribute                                                    | UserClass | 1.0         | 4        |
 EnrichExceptionAttribute                                              | UserClass | 1.0         | 1        |
 EnumViewModelAttribute                                                | UserClass | 1.0         | 1        |
 GenerateCacheKeyAspect                                                | UserClass | 1.0         | 2        |
 IgnoreValuesAttribute                                                 | UserClass | 1.0         | 1        |
 LogAttribute                                                          | UserClass | 1.0         | 9        |
 Metalama.Samples.NotifyPropertyChanged.NotifyPropertyChangedAttribute | UserClass | 1.0         | 1        |
 Metalama.Samples.ToString.ToStringAttribute                           | UserClass | 1.0         | 2        |
 NotifyPropertyChangedAttribute                                        | UserClass | 1.0         | 2        |
 OptionalValueTypeAttribute                                            | UserClass | 1.0         | 1        |
 ReportAndSwallowExceptionsAttribute                                   | UserClass | 1.0         | 1        |
 RetryAttribute                                                        | UserClass | 1.0         | 5        |
 TrackChangesAttribute                                                 | UserClass | 1.0         | 4        |


```

By default, this report will include all projects built during the last seven days.  You can use the `-d`, `-w` or `-h` option to change the time horizon. To analyze a specific project, use the `-p` option and specify the project name.

For instance, the following command lists the credit usages of the `Metalama.Samples.Cache4` project:

```powershell
metalama license credits details -p Metalama.Samples.Cache4
```


  
