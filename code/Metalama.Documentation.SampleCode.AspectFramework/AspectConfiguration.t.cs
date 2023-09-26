// Warning CS0618 on `ProjectExtension`: `'ProjectExtension' is obsolete: 'Use IHierarchicalOptions.'`
// Warning CS0618 on `project.Extension<LoggingOptions>()`: `'IProject.Extension<T>()' is obsolete: 'Use IDeclaration.Enhancements().GetOptions<T> to get or amender.Outbound.Configure<T>(...) to set an option.'`
using System;
namespace Doc.AspectConfiguration
{
  // Some target code.
  public class SomeClass
  {
    public void SomeMethod()
    {
      Console.WriteLine("MyCategory: Executing SomeClass.SomeMethod().");
      return;
    }
  }
}