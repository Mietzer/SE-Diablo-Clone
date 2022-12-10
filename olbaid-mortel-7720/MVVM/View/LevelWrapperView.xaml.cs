using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Utils;
using olbaid_mortel_7720.MVVM.Viewmodel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace olbaid_mortel_7720.MVVM.View
{
  /// <summary>
  /// Interaktionslogik für LevelWrapperView.xaml
  /// Wrapperclass for gameelements
  /// </summary>
  public partial class LevelWrapperView : UserControl
  {
    public LevelWrapperView()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Add Event to View
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
      double x = GlobalVariables.MaxX * PlayerControl.ActualWidth / ScalingViewBox.ActualWidth;
      double y = GlobalVariables.MaxY * PlayerControl.ActualWidth / ScalingViewBox.ActualWidth;

      GlobalVariables.MaxX = Convert.ToInt32(x);
      GlobalVariables.MaxY = Convert.ToInt32(y);

      Window window = Window.GetWindow(this);
      window.KeyDown += PauseLevel;
    }

    /// <summary>
    /// Remove Event from Window as it's not used anymore
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UserControl_Unloaded(object sender, RoutedEventArgs e)
    {
      Window window = NavigationLocator.MainViewModel as Window;
      window.KeyDown -= PauseLevel;
    }
    private void PauseLevel(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Space || e.Key == Key.Escape || e.Key == Key.P)
        (DataContext as LevelWrapperViewModel).TogglePause();
    }


  }
}
