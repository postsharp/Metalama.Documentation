using System.Windows;
using Metalama.Patterns.Observability;
using Metalama.Patterns.Xaml;
namespace Doc.Command.CanExecute_Observable;
[Observable]
public class MyWindow : Window, global::System.ComponentModel.INotifyPropertyChanged
{
  private int _counter;
  public int Counter
  {
    get
    {
      return this._counter;
    }
    private set
    {
      if ((this._counter != value))
      {
        this._counter = value;
        this.OnPropertyChanged("CanExecuteDecrement");
        this.OnPropertyChanged("CanExecuteIncrement");
        this.OnPropertyChanged("Counter");
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
    this.IncrementCommand = new global::Metalama.Patterns.Xaml.Implementation.DelegateCommand(new global::System.Action<global::System.Object>(_ => this.Increment()), new global::System.Func<global::System.Object, global::System.Boolean>(_ => (bool)this.CanExecuteIncrement), this, "CanExecuteIncrement");
    this.DecrementCommand = new global::Metalama.Patterns.Xaml.Implementation.DelegateCommand(new global::System.Action<global::System.Object>(_ => this.Decrement()), new global::System.Func<global::System.Object, global::System.Boolean>(_ => (bool)this.CanExecuteDecrement), this, "CanExecuteDecrement");
  }
  public global::System.Windows.Input.ICommand DecrementCommand { get; }
  public global::System.Windows.Input.ICommand IncrementCommand { get; }
  protected virtual void OnPropertyChanged(global::System.String propertyName)
  {
    this.PropertyChanged?.Invoke(this, new global::System.ComponentModel.PropertyChangedEventArgs(propertyName));
  }
  public event global::System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
}