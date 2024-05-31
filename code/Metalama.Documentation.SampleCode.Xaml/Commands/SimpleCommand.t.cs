// Warning LAMA5206 on `Increment`: `No can-execute method or can-execute property was found using the default naming convention, with candidate member name 'CanExecuteIncrement'.`
// Warning LAMA5206 on `Decrement`: `No can-execute method or can-execute property was found using the default naming convention, with candidate member name 'CanExecuteDecrement'.`
using System.Windows;
using System.Windows.Input;
using Metalama.Patterns.Xaml;
using Metalama.Patterns.Xaml.Implementation;
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