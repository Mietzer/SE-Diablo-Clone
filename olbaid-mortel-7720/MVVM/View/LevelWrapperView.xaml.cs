using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Viewmodel;
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

      if (e.Key == Key.Space || e.Key == Key.Enter)
        (DataContext as LevelWrapperViewModel).LeaveMatch();

      if (e.Key == Key.R)
        (DataContext as LevelWrapperViewModel).RestartMatch();

    }
  }
}
