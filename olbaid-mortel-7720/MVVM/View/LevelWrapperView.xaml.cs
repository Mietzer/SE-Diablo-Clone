using olbaid_mortel_7720.MVVM.Viewmodel;
using System.Windows.Controls;

namespace olbaid_mortel_7720.MVVM.View
{
  /// <summary>
  /// Interaktionslogik für LevelWrapperView.xaml
  /// Wrapperclass for gameelements
  /// </summary>
  public partial class LevelWrapperView : UserControl
  {
    public LevelWrapperView(int selectedLevel = 0)
    {
      //TODO: Add Healthbar

      LevelWrapperViewModel vm = new(selectedLevel);
      DataContext = vm;
      InitializeComponent();
    }

  }
}
