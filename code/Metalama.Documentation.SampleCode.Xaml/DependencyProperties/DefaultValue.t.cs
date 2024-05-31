// Warning LAMA5206 on `BorderWidth`: `No property-changed method was found using the default naming convention, with candidate member name 'OnBorderWidthChanged'.`
// Warning LAMA5206 on `BorderWidth`: `No property-changing method was found using the default naming convention, with candidate member name 'OnBorderWidthChanging'.`
// Warning LAMA5206 on `BorderWidth`: `No validate method was found using the default naming convention, with candidate member name 'ValidateBorderWidth'.`
using Metalama.Patterns.Xaml;
using System.Windows;
using System.Windows.Controls;
namespace Doc.DependencyProperties.DefaultValue;
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
  public static readonly DependencyProperty BorderWidthProperty;
  static MyControl()
  {
    BorderWidthProperty = DependencyProperty.Register("BorderWidth", typeof(double), typeof(MyControl), new PropertyMetadata((double)5));
  }
}