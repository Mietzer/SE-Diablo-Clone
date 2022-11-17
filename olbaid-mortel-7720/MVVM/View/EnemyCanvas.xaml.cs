﻿using olbaid_mortel_7720.GameplayClasses;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace olbaid_mortel_7720.MVVM.View
{
  /// <summary>
  /// Interaktionslogik für EnemyCanvas.xaml
  /// </summary>
  public partial class EnemyCanvas : UserControl
  {
    public List<Enemy> MyEnemy = new List<Enemy>();
    private int MinX;
    private int MinY;
    private int MaxX;
    private int MaxY;

    public EnemyCanvas(List<Enemy> enemyList, Player player)
    {

      this.MyEnemy = enemyList;
      foreach(Enemy enemy in enemyList)
      {
        MinX = enemy.XCoordMin;
        MinY = enemy.YCoordMin;
        MaxX = enemy.XCoordMax;
        MaxY = enemy.YCoordMax;
      }
      

      InitializeComponent();
      EnemyViewModel vm = new(enemyList, EnemyCanvasObject, player);
      DataContext = vm;
    }
  }
}
