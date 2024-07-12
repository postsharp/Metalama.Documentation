using System.Windows;
using System.Windows.Input;
using Metalama.Patterns.Wpf;
using Metalama.Patterns.Wpf.Implementation;
namespace Doc.Command.CanExecute_Explicit;
public class MyWindow : Window
{
  public int Counter { get; private set; }
  public bool CanExecuteIncrement => this.Counter < 10;
  public bool CanExecuteDecrement => this.Counter > 0;
  [Command(CanExecuteProperty = nameof(CanExecuteIncrement))]
  public void Increment()
  {
    this.Counter++;
  }
  [Command(CanExecuteProperty = nameof(CanExecuteDecrement))]
  public void Decrement()
  {
    this.Counter--;
  }
  public MyWindow()
  {
    IncrementCommand = new DelegateCommand(_ => Increment(), _ => CanExecuteIncrement);
    DecrementCommand = new DelegateCommand(_ => Decrement(), _ => CanExecuteDecrement);
  }
  public ICommand DecrementCommand { get; }
  public ICommand IncrementCommand { get; }
}