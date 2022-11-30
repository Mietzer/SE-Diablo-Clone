using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Viewmodel;
using System.Collections.Generic;
using System.Windows.Controls;

namespace olbaid_mortel_7720.MVVM.View
{
  /// <summary>
  /// Interaktionslogik für EnemyCanvas.xaml
  /// </summary>
  public partial class EnemyCanvas : UserControl
  {
    public List<Enemy> MyEnemy = new List<Enemy>();


    public EnemyCanvas(List<Enemy> enemyList, Player player)
    {
      this.MyEnemy = enemyList;

      InitializeComponent();
      EnemyViewModel vm = new(enemyList, EnemyCanvasObject, player);
      DataContext = vm;
    }
  }
}
