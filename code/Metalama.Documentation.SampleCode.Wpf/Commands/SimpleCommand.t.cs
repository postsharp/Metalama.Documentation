using System.Windows;
using System.Windows.Input;
using Metalama.Patterns.Wpf;
using Metalama.Patterns.Wpf.Implementation;
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
    IncrementCommand = new DelegateCommand(_ => Increment(), null);
    DecrementCommand = new DelegateCommand(_ => Decrement(), null);
  }
  public ICommand DecrementCommand { get; }
  public ICommand IncrementCommand { get; }
}