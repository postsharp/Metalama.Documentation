using Metalama.Patterns.Xaml;
using System.Windows;
using System.Windows.Controls;
namespace Doc.DependencyProperties.OnPropertyChanged;
internal class MyControl : UserControl
{
  [DependencyProperty]
  public double BorderWidth
  {
    get
    {
      return (double)GetValue(BorderWidthProperty);
    }
    set
    {
      this.SetValue(BorderWidthProperty, value);
    }
  }
  [DependencyProperty]
  public double AvailableWidth
  {
    get
    {
      return (double)GetValue(AvailableWidthProperty);
    }
    private set
    {
      this.SetValue(AvailableWidthProperty, value);
    }
  }
  private void OnBorderWidthChanged()
  {
    this.AvailableWidth = this.Width - this.BorderWidth * 2;
  }
  public static readonly DependencyProperty AvailableWidthProperty;
  public static readonly DependencyProperty BorderWidthProperty;
  static MyControl()
  {
    BorderWidthProperty = DependencyProperty.Register("BorderWidth", typeof(double), typeof(MyControl), new PropertyMetadata((d, e) => ((MyControl)d).OnBorderWidthChanged()));
    AvailableWidthProperty = DependencyProperty.Register("AvailableWidth", typeof(double), typeof(MyControl));
  }
}