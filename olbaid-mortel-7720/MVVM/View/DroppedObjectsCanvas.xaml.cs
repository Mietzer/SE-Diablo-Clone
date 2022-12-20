using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Model.Object;
using olbaid_mortel_7720.MVVM.Viewmodel;
using System.Collections.Generic;
using System.Windows.Controls;

namespace olbaid_mortel_7720.MVVM.View
{
  /// <summary>
  /// Interaktionslogik für DropObjectCanvas.xaml
  /// </summary>
  public partial class DroppedObjectsCanvas : UserControl
  {
    public DroppedObjectsCanvas(List<GameObject> dropObjects, Player player)
    {
      InitializeComponent();
      DroppedObjectsViewModel vm = new(dropObjects, EnemyCanvasObject, player);
      DataContext = vm;
    }
  }
}
