using System.Windows;
using System.Windows.Input;
using Metalama.Patterns.Wpf;
using Metalama.Patterns.Wpf.Implementation;
namespace Doc.Command.CanExecute_Czech;
public class MojeOkno : Window
{
  public int Počitadlo { get; private set; }
  [Command]
  public void VykonatZvýšení()
  {
    this.Počitadlo++;
  }
  public bool MůžemeVykonatZvýšení => this.Počitadlo < 10;
  [Command]
  public void Snížit()
  {
    this.Počitadlo--;
  }
  public bool MůžemeSnížit => this.Počitadlo > 0;
  public MojeOkno()
  {
    VykonatZvýšeníPříkaz = new DelegateCommand(_ => VykonatZvýšení(), _ => MůžemeVykonatZvýšení);
    SnížitPříkaz = new DelegateCommand(_ => Snížit(), _ => MůžemeSnížit);
  }
  public ICommand SnížitPříkaz { get; }
  public ICommand VykonatZvýšeníPříkaz { get; }
}