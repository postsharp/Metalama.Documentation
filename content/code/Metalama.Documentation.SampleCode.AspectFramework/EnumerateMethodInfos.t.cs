using System;
using System.Collections.Generic;
using System.Reflection;
namespace Doc.EnumerateMethodInfos
{
  [EnumerateMethodAspect]
  internal class Foo
  {
    private void Method1()
    {
    }
    private void Method2(int x, string y)
    {
    }
    public IReadOnlyList<MethodInfo> GetMethods()
    {
      var methods = new List<MethodInfo>();
      methods.Add(typeof(Foo).GetMethod("Method1", BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null)!);
      methods.Add(typeof(Foo).GetMethod("Method2", BindingFlags.NonPublic | BindingFlags.Instance, null, new[]{typeof(int), typeof(string)}, null)!);
      return methods;
    }
  }
}