using System.Windows;
using Metalama.Framework.Fabrics;
using Metalama.Patterns.Xaml;
using Metalama.Patterns.Xaml.Configuration;
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
    this.ZvýšitCommand = new global::Metalama.Patterns.Xaml.Implementation.DelegateCommand(new global::System.Action<global::System.Object>(_ => this.Zvýšit()), new global::System.Func<global::System.Object, global::System.Boolean>(_ => (bool)this.MůzemeZvýšit));
    this.SnížitCommand = new global::Metalama.Patterns.Xaml.Implementation.DelegateCommand(new global::System.Action<global::System.Object>(_ => this.Snížit()), new global::System.Func<global::System.Object, global::System.Boolean>(_ => (bool)this.MůzemeSnížit));
  }
  public global::System.Windows.Input.ICommand SnížitCommand { get; }
  public global::System.Windows.Input.ICommand ZvýšitCommand { get; }
}
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
public class Fabric : ProjectFabric
{
  public override void AmendProject(IProjectAmender amender) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
}
#pragma warning restore CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052