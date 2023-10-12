// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

namespace Metalama.Documentation.Helpers.ConsoleApp;

public interface IConsoleMain
{
    void Execute();
}

public interface IConsoleHost
{
    public IReadOnlyList<string> Arguments { get; }
}

internal class ConsoleHost : IConsoleHost
{
    public ConsoleHost( IReadOnlyList<string>? arguments ) 
    {
        this.Arguments = arguments ?? Array.Empty<string>();
    }

    public IReadOnlyList<string> Arguments { get; }
}