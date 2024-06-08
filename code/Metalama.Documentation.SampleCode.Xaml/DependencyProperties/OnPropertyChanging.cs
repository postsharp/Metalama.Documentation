// This is public domain Metalama sample code.

using Metalama.Patterns.Xaml;
using System;
using System.Windows.Controls;

#pragma warning disable CA1307

namespace Doc.DependencyProperties.OnPropertyChanging;

internal class MyControl : UserControl
{
    [DependencyProperty]
    public string Title { get; set; }

    private void OnTitleChanged( string value )
    {
        if ( value.Contains( "foo" ) )
        {
            throw new ArgumentOutOfRangeException( nameof(value) );
        }
    }
}