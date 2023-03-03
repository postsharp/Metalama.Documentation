---
uid: compile-time-testing
level: 400
---

# Testing compile-time code

When you build complex aspects, it is a good idea to move the complex compile-time logic, typically some code that queries the code model, to compile-time classes that are not aspects. It is possible to build unit tests for these compile-time classes. 

## Benefits 

The benefit of unit-testing compile-time classes are:

* it is generally simpler to create a complete test coverage with unit tests than with aspect tests (see <xref:aspect-testing>),
* it is easier to debug unit tests than aspect tests.

## To create unit tests for your compile-time code

### Step 1. Disable pruning of compile-time code

In the project that defines the compile-time code, set the `MetalamaRemoveCompileTimeOnlyCode` property to `False`:

```xml
<Project>
    <PropertyGroup>
        <MetalamaRemoveCompileTimeOnlyCode>False</MetalamaRemoveCompileTimeOnlyCode>
    </PropertyGroup>
</Project>
```

If you skip this step, calling any compile-time code from a unit test will throw an exception.

### Step 2. Create a Xunit test project

Create a Xunit test project as usual. 

It is highly recommended that you target .NET 6.0 because temporary files cannot be cleaned up automatically with lower .NET versions.

### Step 3. Add the Metalama.Testing.UnitTesting project

```xml
<Project>
    <ItemGroup>
        <PackageReference Include="Metalama.Testing.UnitTesting" Version="CHANGE ME" />
    </ItemGroup>
</Project>
```

### Step 4. Create a test class derived from UnitTestClass

Create a new test class and make it derive from <xref:Metalama.Testing.UnitTesting.UnitTestClass>.

```cs
 public class MyTests : UnitTestClass { }

```

### Step 5. Create test methods

Each test method _must_ call the <xref:Metalama.Testing.UnitTesting.UnitTestClass.CreateTestContext> and _must_ dispose of the context at the end of the test method.

Then, typically, your test would call the  <xref:Metalama.Testing.UnitTesting.TestContext.CreateCompilation*?text=context.CreateCompilation> method to get an <xref:Metalama.Framework.Code.ICompilation>.

```cs

public class MyTests : UnitTestClass
{
    [Fact]
    public void SimpleTest()
    {
        // Create a test context and dispose of it at the end of the test.
        using var testContext = this.CreateTestContext();

        // Create a compilation
        var code = @"
class C 
{
    void M1 () {}

    void M2()
    {
        var x = 0;
        x++; 
    }
}

";

        var compilation = testContext.CreateCompilation( code );

        var type = compilation.Types.OfName( "C" ).Single();

        var m1 = type.Methods.OfName( "M1" ).Single();
        
        // Do any assertion. Typically call your compile-time code here.
        Assert.Equal( 0, m1.Parameters.Count );
    }
}
```

> [!NOTE]
> Some APIs require the execution context to be set and assigned to your compilation. There is currently no public API to change the execution context.
