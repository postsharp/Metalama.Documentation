---
uid: migration
---

# Migrating from PostSharp

You may want to migrate some or all of your projects from [PostSharp](/postsharp) to [Metalama](/metalama). If you decide to do so, this chapter is here to help.

When we designed [Metalama](/metalama) as the successor of [PostSharp](/postsharp), we decided to break backward compatibility. After all, PostSharp Framework was designed in 2004-2010, the years of C# 2.0 and .NET Framework 2.0, and we have maintained backward compatibility since then. We could not take advantage of the new .NET stack (for example, C# 11, .NET 6, and Roslyn), by staying loyal to our 2010 API. So, we opted for a complete redesign of the concepts and APIs.

However, we appreciate that many customers have tens of thousands to millions of lines of code that use PostSharp. This is why we made sure that we did not ask these customers to port these large code bases.

So we took the following decisions:

 * Your _aspects_ will need to be _completely rewritten_.
 * The _business code_ that only _uses_ the aspects _should not normally need any changes_, except for find-in-files and namespace imports replacements.

We hope this is an acceptable compromise.

## In this chapter

Article | Description
-|-
<xref:migrating-aspects> | This article gives the master step-by-step procedure to your migration project, and refers to other articles in this chapter.
<xref:benefits-over-postsharp> | This article presents the benefits of Metalama over PostSharp.
<xref:when-migrate> | This article takes a step back and explains a few points to consider before migrating your aspects to Metalama. Make sure to read this article before taking any decision.
<xref:migration-feature-status> | This article describes the status of PostSharp features in Metalama.
<xref:differences-from-postsharp> | This article explains the major differences between the architectures of PostSharp and Metalama from a theoretical angle.
<xref:migrating-multicasting> | This article explains how to migrate PostSharp attribute multicasting to Metalama.
<xref:migrating-configuration> | This article explains how to migrate PostSharp configuration files like `postsharp.config` to Metalama.


