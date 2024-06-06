// Warning LAMA5206 on `Increment`: `No can-execute method or can-execute property was found using the default naming convention, with candidate member name 'CanExecuteIncrement'.`
// Warning LAMA5206 on `Decrement`: `No can-execute method or can-execute property was found using the default naming convention, with candidate member name 'CanExecuteDecrement'.`
using System.Windows;
using Metalama.Patterns.Xaml;
namespace Doc.Command.SimpleCommand;
public class MyWindow : Window
{
  public int Counter { get; private set; }
  [Command]
  public void Increment()
  {
    this.Counter++;
  }
  [Command]
  public void Decrement()
  {
    this.Counter--;
  }
  public MyWindow()
  {
    this.IncrementCommand = new global::Metalama.Patterns.Xaml.Implementation.DelegateCommand(new global::System.Action<global::System.Object>(_ => this.Increment()), null);
    this.DecrementCommand = new global::Metalama.Patterns.Xaml.Implementation.DelegateCommand(new global::System.Action<global::System.Object>(_ => this.Decrement()), null);
  }
  public global::System.Windows.Input.ICommand DecrementCommand { get; }
  public global::System.Windows.Input.ICommand IncrementCommand { get; }
}