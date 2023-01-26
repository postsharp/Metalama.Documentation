using System;
using System.Collections.Generic;
namespace Doc.ConvertToRunTime
{
  internal class Foo
  {
    [ConvertToRunTimeAspect]
    private void Bar(string a, int c, DateTime e)
    {
      var parameterNames = new List<string>{"a", "c", "e"};
      var buildTime = new Guid("13c139ea-42f5-4726-894d-550406357978");
      var parameterType = typeof(string);
      return;
    }
  }
}