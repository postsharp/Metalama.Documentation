---
uid: obsolete-method-blocker
---

# Throwing exception on call of obsolete method

Sometimes we write some functions and over time these functions become obsolete for several reasons. You can add the `[Obsolete]` attribute on these methods. This will generate a compiler warning if a call is made to any such method.

However, if you want the program to break on such calls, you can create the following aspect.


[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/ThrowOnCall.cs name="Finding obsolete calls and blocking them"]

> [!NOTE]
> Note how the implementation of the obsolete method is ignored and an exception is thrown.

