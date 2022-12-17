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
    public MapViewModel Vm;
    public MapView(Map map)
    {
      InitializeComponent();
      Vm = new MapViewModel(MyCanvas, map);
    }
  }
}

