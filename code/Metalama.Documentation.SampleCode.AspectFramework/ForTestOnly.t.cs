using System;

namespace Doc.ForTestOnly;

public class MyService
{
    // Normal constructor.
    public MyService() : this(DateTime.Now)
    {
    }

    [ForTestOnly]
    internal MyService(DateTime dateTime)
    {
    }
}

internal class NormalClass
{
    // Usage allowed here.
    private MyService service = new(DateTime.Now.AddDays(1));
}

internal class TestClass
{
    // Usage allowed here.
    private MyService service = new(DateTime.Now.AddDays(1));
}