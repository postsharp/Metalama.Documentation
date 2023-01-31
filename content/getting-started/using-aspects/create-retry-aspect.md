---
uid: create-retry-aspect
---

# An aspect to retry a method 
Sometimes you have to call a service via a method call and the service may be flaky and works only sometimes. So you can decide to call the method at least three times. You can use the `Retry` aspect as shown below to make sure that the call to the emthod is done as many times as you want. 

The following example shows how to use this aspect and how to create it. Right now don't concentrate on the creation part as that will be described in detail in the next chapter. 

## Sample  

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/Retry.cs name="Retry aspect with sample code"]



