using olbaid_mortel_7720.MVVM.Viewmodel;
using System.Windows;
using System.Windows.Controls;

namespace olbaid_mortel_7720.MVVM.View
{
  /// <summary>
  /// Interaktionslogik für LevelSelectionView.xaml
  /// </summary>
  public partial class LevelSelectionView : UserControl
  {
    public LevelSelectionView()
    {
      LevelSelectionViewModel vm = new();
      DataContext = vm;
      InitializeComponent();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
      Window w = Window.GetWindow(this);
      (DataContext as LevelSelectionViewModel).SetWindow(w as MainWindow);
    }
  }
}
