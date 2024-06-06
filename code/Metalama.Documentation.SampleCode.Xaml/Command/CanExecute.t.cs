using System.Windows;
using Metalama.Patterns.Xaml;
namespace Doc.Command.CanExecute;
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
  public MyWindow()
  {
    this.IncrementCommand = new global::Metalama.Patterns.Xaml.Implementation.DelegateCommand(new global::System.Action<global::System.Object>(_ => this.Increment()), new global::System.Func<global::System.Object, global::System.Boolean>(_ => (bool)this.CanExecuteIncrement));
    this.DecrementCommand = new global::Metalama.Patterns.Xaml.Implementation.DelegateCommand(new global::System.Action<global::System.Object>(_ => this.Decrement()), new global::System.Func<global::System.Object, global::System.Boolean>(_ => (bool)this.CanExecuteDecrement));
  }
  public global::System.Windows.Input.ICommand DecrementCommand { get; }
  public global::System.Windows.Input.ICommand IncrementCommand { get; }
}