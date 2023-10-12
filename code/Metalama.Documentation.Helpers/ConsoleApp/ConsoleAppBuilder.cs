// This is public domain Metalama sample code.

using Microsoft.Extensions.DependencyInjection;

namespace Metalama.Documentation.Helpers.ConsoleApp;

public class ConsoleAppBuilder
{
    private readonly ServiceCollection _serviceCollection = new();

    public IServiceCollection Services => this._serviceCollection;

    public ConsoleApp Build( IReadOnlyList<string>? arguments = null )
    {
        this._serviceCollection.AddSingleton<IConsoleHost>( _ => new ConsoleHost( arguments ) );

        return new ConsoleApp( this._serviceCollection.BuildServiceProvider() );
    }
}