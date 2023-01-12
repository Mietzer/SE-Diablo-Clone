using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Viewmodel;
using System.Windows.Controls;

namespace olbaid_mortel_7720.MVVM.View
{
  /// <summary>
  /// Interaktionslogik für MapView.xaml
  /// </summary>
  public partial class MapView : UserControl
  {
    #region Properties
    public MapViewModel ViewModel;
    #endregion Properties

    #region Constructor
    public MapView(Map map)
    {
      InitializeComponent();
      ViewModel = new MapViewModel(MyCanvas, map);
    }
    #endregion Constructor

  }
}

