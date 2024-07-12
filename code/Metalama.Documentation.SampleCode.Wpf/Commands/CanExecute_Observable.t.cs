using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Metalama.Patterns.Observability;
using Metalama.Patterns.Wpf;
using Metalama.Patterns.Wpf.Implementation;
namespace Doc.Command.CanExecute_Observable;
[Observable]
public class MyWindow : Window, INotifyPropertyChanged
{
  private int _counter;
  public int Counter
  {
    get
    {
      return _counter;
    }
    private set
    {
      if (_counter != value)
      {
        _counter = value;
        OnPropertyChanged("CanExecuteDecrement");
        OnPropertyChanged("CanExecuteIncrement");
        OnPropertyChanged("Counter");
      }
    }
  }
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
    IncrementCommand = new DelegateCommand(_ => Increment(), _ => CanExecuteIncrement, this, "CanExecuteIncrement");
    DecrementCommand = new DelegateCommand(_ => Decrement(), _ => CanExecuteDecrement, this, "CanExecuteDecrement");
  }
  public ICommand DecrementCommand { get; }
  public ICommand IncrementCommand { get; }
  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}