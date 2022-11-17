﻿using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using olbaid_mortel_7720.GameplayClasses;

namespace olbaid_mortel_7720.MVVM.Viewmodel
{
  public class EnemyViewModel : NotifyObject
  {
    public List<Enemy> MyEnemy = new List<Enemy>();
    public Player MyPlayer { get; set; }
    private int MinX;
    private int MinY;
    private int MaxX;
    private int MaxY;

    private Canvas MyEnemyCanvas;

    private string Tag;

    public EnemyViewModel(List<Enemy> myenemy, Canvas MyEnemyCanvas, Player player)
    {
      this.MyEnemy = myenemy;
      this.MyEnemyCanvas = MyEnemyCanvas;

      if(MyEnemy is EnemyMelee)
      {
        this.Tag = "EnemyMelee";
      }
      else if(MyEnemy is EnemyRanged)
      {
        this.Tag = "EnemyRanged";
      }
      foreach(Enemy enemy in MyEnemy)
      {
        MinX = enemy.XCoordMin;
        MinY = enemy.YCoordMin;
        MaxX = enemy.XCoordMax;
        MaxY = enemy.YCoordMax;
      }

      this.MyPlayer = player;

      InitTimer();
    }

    public void InitTimer()
    {
      DispatcherTimer movementTimer = new();
      movementTimer.Tick += new EventHandler(Move);
      movementTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);
      movementTimer.Start();
    }

    private void Move(object sender, EventArgs e)
    {
      foreach(Enemy enemy in MyEnemy)
      {
        (enemy as EnemyMelee).MoveToPlayer(MyPlayer);
        Canvas.SetTop(enemy.Model, enemy.YCoord);
        Canvas.SetLeft(enemy.Model, enemy.XCoord);
      }
      
    }

  }
}
