// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code.SyntaxBuilders;
using System;
using System.Linq;

namespace Doc.SwitchStatementBuilder_;

[Dispatch]
public class FruitProcessor
{
    private void ProcessApple( string args ) { }

    private void ProcessOrange( string args ) { }

    private void ProcessPear( string args ) { }
}