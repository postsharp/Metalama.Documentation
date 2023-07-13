---
uid: run-time-testing
level: 300
---

# Unit testing aspects at run time

(TODO: This article is currently in draft form.)

The objective of this approach is to apply an aspect to a specific target code, then create standard unit tests to verify that the resulting code behaves as expected.

For example, to test a logging aspect, you could configure your logging aspect to log to an in-memory `StringWriter`. Then, use a standard unit test to confirm that a logged method, when invoked from the test method, yields the expected result in the `StringWriter`. This concept is illustrated in the code snippet below.

```cs
class MyTests
{
    StringWriter _logger = new();

    public void TestVoidMethod()
    {
        this.VoidMethod(5);

        Assert.Equal( @"
Entering VoidMethod(5).
Oops
VoidMethod(5) succeeded.
",
        _logger.ToString());
    }

    [Log]
    private void VoidMethod(int p)
    {
        _logger.WriteLine("Oops");
    }
}
```

[comment]: # (TODO: cover dependency injection)


> [!div class="see-also"]
> <xref:video-testing>
