using olbaid_mortel_7720.MVVM.Utils;
using System;
using System.Windows.Controls;

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

    private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
      double x = GlobalVariables.MaxX * PlayerControl.ActualWidth / ScalingViewBox.ActualWidth;
      double y = GlobalVariables.MaxX * PlayerControl.ActualWidth / ScalingViewBox.ActualWidth;

      GlobalVariables.MaxX = Convert.ToInt32(x);
      GlobalVariables.MaxY = Convert.ToInt32(y);

    }
  }
}
