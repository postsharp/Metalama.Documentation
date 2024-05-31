using System.Windows;
using Metalama.Patterns.Observability;
using Metalama.Patterns.Xaml;

namespace Doc.Command.CanExecute_Observable;

[Observable]
public class MyWindow : Window
{
    public int Counter { get; private set; }

    [Command]
    public void Increment()
    {
        this.Counter++;
    }

    public bool CanExecuteIncrement => this.Counter < 10;

    [Command]
    public void Decrement()
    {
        this.Counter--;
    }

    public bool CanExecuteDecrement => this.Counter > 0;

}

