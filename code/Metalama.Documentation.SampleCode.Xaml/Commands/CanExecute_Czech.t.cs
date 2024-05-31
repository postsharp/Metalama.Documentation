using System.Windows;
using System.Windows.Input;
using Metalama.Framework.Fabrics;
using Metalama.Patterns.Xaml;
using Metalama.Patterns.Xaml.Configuration;
using Metalama.Patterns.Xaml.Implementation;
namespace Doc.Command.CanExecute;
public class MojeOkno : Window
{
  public int Počitadlo { get; private set; }
  [Command]
  public void Zvýšit()
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
    ZvýšitCommand = new DelegateCommand(_ => Zvýšit(), _ => MůzemeZvýšit);
    SnížitCommand = new DelegateCommand(_ => Snížit(), _ => MůzemeSnížit);
  }
  public ICommand SnížitCommand { get; }
  public ICommand ZvýšitCommand { get; }
}
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
public class Fabric : ProjectFabric
{
  public override void AmendProject(IProjectAmender amender) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
}