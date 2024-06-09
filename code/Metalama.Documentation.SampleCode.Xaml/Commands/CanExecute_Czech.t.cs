using System.Windows;
using System.Windows.Input;
using Metalama.Framework.Fabrics;
using Metalama.Patterns.Xaml;
using Metalama.Patterns.Xaml.Configuration;
using Metalama.Patterns.Xaml.Implementation;
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
  public MojeOkno()
  {
    VykonatZvýšeníCommand = new DelegateCommand(_ => VykonatZvýšení(), null);
    SnížitCommand = new DelegateCommand(_ => Snížit(), null);
  }
  public ICommand SnížitCommand { get; }
  public ICommand VykonatZvýšeníCommand { get; }
}