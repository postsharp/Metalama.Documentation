// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;
using Metalama.Patterns.Xaml;
using System.Windows.Controls;

namespace Doc.DependencyProperties.Contract;

internal class MyControl : UserControl
{
    [DependencyProperty]
    [Positive]
    public double BorderWidth { get; set; }
}