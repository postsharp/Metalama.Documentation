---
uid: run-time-testing
level: 300
---

# Unit testing aspects at run time

(This article is just a sketch.)

The idea of this approach is to apply the aspect to some target code and to create normal unit tests that verify that the resulting code has the desired behavior.

For instance, if you want to test a logging aspect, you can make your logging aspect log to an in-memory `StringWriter`, and use a standard unit test to verify that some logged method, when called from the test method, produces the expected result in the `StringWriter`. This idea is drafted in the following code snippet.



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
        _logger.ToString();

 )

    }

    [Log]
    private void VoidMethod( int p ) 
    { 
        _logger.WriteLine("Oops");
    }


}


```


[comment]: # (TODO: cover dependency injection)
