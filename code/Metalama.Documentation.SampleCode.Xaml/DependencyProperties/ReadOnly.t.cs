using Metalama.Patterns.Xaml;
using System.Windows;
using System.Windows.Controls;
namespace Doc.DependencyProperties.ReadOnly;
internal class MyControl : UserControl
{
  [DependencyProperty]
  public double BorderWidth
  {
    get
    {
      return (double)GetValue(BorderWidthProperty);
    }
    private set
    {
      this.SetValue(BorderWidthProperty, value);
    }
  }
  public static readonly DependencyProperty BorderWidthProperty;
  static MyControl()
  {
    BorderWidthProperty = DependencyProperty.Register("BorderWidth", typeof(double), typeof(MyControl));
  }
}