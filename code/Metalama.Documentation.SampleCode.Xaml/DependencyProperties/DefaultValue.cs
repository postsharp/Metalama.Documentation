﻿// This is public domain Metalama sample code.

using Metalama.Patterns.Xaml;
using System.Windows.Controls;

namespace Doc.DependencyProperties.DefaultValue;

internal class MyControl : UserControl
{
    [DependencyProperty]
    public double BorderWidth { get; set; } = 5;
}