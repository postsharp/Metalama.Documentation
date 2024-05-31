using System.Windows;
using Metalama.Patterns.Xaml;
namespace Doc.Command.CanExecute;
public class MyWindow : Window
{
  int _counter;
  [Command]
  public void Increment()
  {
    this._counter++;
  }
  public bool CanExecuteIncrement => this._counter < 10;
  [Command]
  public void Decrement()
  {
    this._counter--;
  }
  public bool CanExecuteDecrement => this._counter > 0;
  public MyWindow()
  {
    this.IncrementCommand = new global::Metalama.Patterns.Xaml.Implementation.DelegateCommand(new global::System.Action<global::System.Object>(_ => this.Increment()), new global::System.Func<global::System.Object, global::System.Boolean>(_ => (bool)this.CanExecuteIncrement));
    this.DecrementCommand = new global::Metalama.Patterns.Xaml.Implementation.DelegateCommand(new global::System.Action<global::System.Object>(_ => this.Decrement()), new global::System.Func<global::System.Object, global::System.Boolean>(_ => (bool)this.CanExecuteDecrement));
  }
  public global::System.Windows.Input.ICommand DecrementCommand { get; }
  public global::System.Windows.Input.ICommand IncrementCommand { get; }
}