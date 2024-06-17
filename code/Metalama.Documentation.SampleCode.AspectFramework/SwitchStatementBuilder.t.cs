using Metalama.Framework.Aspects;
using Metalama.Framework.Code.SyntaxBuilders;
using System;
using System.Linq;
namespace Doc.SwitchStatementBuilder_;
[Dispatch]
public class FruitProcessor
{
  private void ProcessApple(string args)
  {
  }
  private void ProcessOrange(string args)
  {
  }
  private void ProcessPear(string args)
  {
  }
  public void Execute(string messageName, string args)
  {
    switch (messageName)
    {
      case "Apple":
        ProcessApple(args);
        break;
      case "Orange":
        ProcessOrange(args);
        break;
      case "Pear":
        ProcessPear(args);
        break;
      default:
        throw new ArgumentOutOfRangeException();
    }
  }
}