// Warning LAMA5206 on `Title`: `No property-changing method was found using the default naming convention, with candidate member name 'OnTitleChanging'.`
// Warning LAMA5206 on `Title`: `No validate method was found using the default naming convention, with candidate member name 'ValidateTitle'.`
using Metalama.Patterns.Xaml;
using System;
using System.Windows;
using System.Windows.Controls;
namespace Doc.DependencyProperties.OnPropertyChanging;
internal class MyControl : UserControl
{
  [DependencyProperty]
  public string Title
  {
    get
    {
      return (string)GetValue(TitleProperty);
    }
    set
    {
      this.SetValue(TitleProperty, value);
    }
  }
  private void OnTitleChanged(string value)
  {
    if (value.Contains("foo"))
    {
      throw new ArgumentOutOfRangeException(nameof(value));
    }
  }
  public static readonly DependencyProperty TitleProperty;
  static MyControl()
  {
    TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(MyControl), new PropertyMetadata((d, e) => ((MyControl)d).OnTitleChanged((string)e.NewValue)));
  }
}