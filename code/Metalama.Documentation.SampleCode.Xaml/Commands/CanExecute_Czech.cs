// This is public domain Metalama sample code.

using System.Windows;
using Metalama.Patterns.Xaml;

namespace Doc.Command.CanExecute_Czech;

public class MojeOkno : Window
{
    public int Počitadlo { get; private set; }

    [Command]
    public void VykonatZvýšení()
    {
        this.Počitadlo++;
    }

    public bool MůzemeZvýšit => this.Počitadlo < 10;

    [Command]
    public void Snížit()
    {
        this.Počitadlo--;
    }

    public bool MůzemeSnížit => this.Počitadlo > 0;
}