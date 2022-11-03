using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace olbaid_mortel_7720.MVVN.Views;

/// <summary>
/// View component for the health bar in the overall player's gui.
/// </summary>
public partial class PlayerHealthbarView : UserControl
{
    public PlayerHealthbarView()
    {
        InitializeComponent();
    }

    private void PlayerHealthbarView_OnLoaded(object sender, RoutedEventArgs e)
    {
        int sizeOfText = (int)Math.Round(this.ActualHeight / 2);
        TxtPercentage.FontSize = sizeOfText;
        TxtPercentage.Margin = new Thickness(0, 0, sizeOfText, 0);
    }
}