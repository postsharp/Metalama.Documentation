---
uid: cache-locking
title: "Preventing Concurrent Execution of Cached Methods"
product: "postsharp"
categories: "Metalama;AOP;Metaprogramming"
---
# Preventing concurrent execution of cached methods

When the evaluation of a method consumes significant resources or time, you may want to prevent a situation where several threads, processes or machines are evaluating the same method with the same parameters at the same time. You can achieve it by instructing Metalama to use a concept of _lock manager_, abstracted by the <xref:Metalama.Patterns.Caching.Locking.ILockFactory> interface.

Metalama implements two lock managers: the default <xref:Metalama.Patterns.Caching.Locking.NullLockFactory>, and <xref:Metalama.Patterns.Caching.Locking.LocalLockFactory>. 

## Preventing concurrent execution in the current process

By default, the caching aspect allows concurrent execution of the same method with the same arguments.

The <xref:Metalama.Patterns.Caching.Locking.LocalLockFactory> class implements a locking strategy that prevents execution of methods running in the current process (or <xref:System.AppDomain>, to be exact). 

To configure the lock manager, you have to set the <xref:Metalama.Patterns.Caching.CachingProfile.LockFactory> property of the relevant <xref:Metalama.Patterns.Caching.CachingProfile>. Each caching profile must be set up separately. 

To start using <xref:Metalama.Patterns.Caching.Locking.LocalLockFactory>, go to the code that initialized the Metalama Caching by calling <xref:Metalama.Patterns.Caching.Building.CachingServiceFactory.AddCaching*?text=serviceCollection.AddCaching>  or <xref:Metalama.Patterns.Caching.CachingService.Create*?text=CachingService.Create>. Supply a delegate that calls <xref:Metalama.Patterns.Caching.Building.ICachingServiceBuilder.AddProfile*> and set the <xref:Metalama.Patterns.Caching.CachingProfile.LockFactory?CachingProfile.LockFactory> property.


> [!NOTE]
> Each instance of the <xref:Metalama.Patterns.Caching.Locking.LocalLockFactory> class maintains its own set of locks. However, whether several profiles use the same or a different instance of the <xref:Metalama.Patterns.Caching.Locking.LocalLockFactory> does not matter because each method is associated with one and only one profile. 


For instance, the following snippet activates <xref:Metalama.Patterns.Caching.Locking.LocalLockFactory> for the `Locking` logging profile:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/Locking/Locking.Program.cs marker="AddCaching"]


### Example: locking vs non-locking caching access

The following example demonstrates two versions of a simulated `ReadFile` method: one without cache locking, and the second with cache locking.  The fake implementations make the behavior deterministic.

The main program executes these methods twice in parallel and compare their result. When locking is enabled, both executions return exactly the same instance, which means that the methods did _not_ execute in parallel, which is exactly what cache locking is about. 

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/Locking/Locking.cs]


## Handling lock timeouts

By default (unless you use the default <xref:Metalama.Patterns.Caching.Locking.NullLockFactory>), the caching aspect will wait for a lock during an infinite amount of time. Suppose that the thread that evaluates the method gets stuck (e.g. it is involved in a deadlock). Because of the locking mechanism, all threads evaluating the same method will also get stuck. To avoid this situation, you can choose to implement a timeout behavior. 

> [!NOTE]
> This section only covers the time it takes to acquire a lock. It does not cover the execution time of the method that has already acquired the lock.


Two properties of the <xref:Metalama.Patterns.Caching.CachingProfile> class influence the timeout behavior:

* <xref:Metalama.Patterns.Caching.CachingProfile.AcquireLockTimeout> determines the maximum time that the caching aspect will wait for the lock manager to acquire a lock. To specify an infinite waiting time, set this property to `TimeSpan.FromMilliseconds( -1 )`. The default behavior is to wait infinitely. 

* <xref:Metalama.Patterns.Caching.CachingProfile.OnLockTimeout> is a delegate invoked when the caching aspect could not acquire a lock because of a timeout. The default behavior is to throw a <xref:System.TimeoutException>. To ignore the lock and to proceed with the method implementation, replace this property with a delegate that does not do anything.


## Implementing a distributed lock manager

Implementing a distributed locking algorithm is a highly complex task and we at Metalama decided not to get involved in this business (just as we do not provide the implementation of a cache itself). However, Metalama gives you the ability to use any third-party implementation.

To create make your lock manager work with the caching aspect, you should implement the <xref:Metalama.Patterns.Caching.Locking.ILockFactory> and <xref:Metalama.Patterns.Caching.Locking.ILockHandle> interfaces. 

