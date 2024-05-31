// Warning LAMA5206 on `BorderWidth`: `No property-changed method was found using the default naming convention, with candidate member name 'OnBorderWidthChanged'.`
// Warning LAMA5206 on `BorderWidth`: `No property-changing method was found using the default naming convention, with candidate member name 'OnBorderWidthChanging'.`
// Warning LAMA5206 on `BorderWidth`: `No validate method was found using the default naming convention, with candidate member name 'ValidateBorderWidth'.`
using Metalama.Patterns.Contracts;
using Metalama.Patterns.Xaml;
using System;
using System.Windows;
using System.Windows.Controls;
namespace Doc.DependencyProperties.Simple;
internal class MyControl : UserControl
{
  [DependencyProperty]
  [Positive]
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
    BorderWidthProperty = DependencyProperty.Register("BorderWidth", typeof(double), typeof(MyControl), new PropertyMetadata() { CoerceValueCallback = (d, value) =>
    {
      value = ApplyBorderWidthContracts((double)value);
      return value;
    } });
  }
  private static double ApplyBorderWidthContracts(double value)
  {
    if (value is < 0)
    {
      throw new ArgumentOutOfRangeException("value", "The 'value' parameter must be greater than or equal to 0.");
    }
    return value;
  }
}