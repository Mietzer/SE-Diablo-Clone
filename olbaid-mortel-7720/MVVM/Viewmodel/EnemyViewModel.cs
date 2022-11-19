﻿using olbaid_mortel_7720.GameplayClasses;
using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Threading;

namespace olbaid_mortel_7720.MVVM.Viewmodel
{
  public class EnemyViewModel : NotifyObject
  {
    public List<Enemy> MyEnemy = new List<Enemy>();
    private Player MyPlayer { get; set; }

    private Canvas MyEnemyCanvas;

    private string Tag;

    public EnemyViewModel(List<Enemy> myenemy, Canvas MyEnemyCanvas, Player player)
    {
      this.MyEnemy = myenemy;
      this.MyEnemyCanvas = MyEnemyCanvas;
      this.Tag = "Enemy";
     

      this.MyPlayer = player;

      InitTimer();
    }

    public void InitTimer()
    {
      //Tick for EnemyMovement
      DispatcherTimer movementTimer = new();
      movementTimer.Tick += new EventHandler(Move);
      movementTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);
      movementTimer.Start();

      //Tick for HitReg
      DispatcherTimer checkforHit = new();
      checkforHit.Tick += new EventHandler(CheckforHit);
      checkforHit.Interval = new TimeSpan(0, 0, 0, 0, 20);
      checkforHit.Start();

      //Tick for EnemyRemoval if health <= 0
      DispatcherTimer removeEnemy = new();
      removeEnemy.Tick += new EventHandler(RemoveEnemy);
      removeEnemy.Interval = new TimeSpan(0, 0, 0, 0, 20);
      removeEnemy.Start();
    }

    private void Move(object sender, EventArgs e)
    {
      foreach (Enemy enemy in MyEnemy)
      {
        (enemy as EnemyMelee).MoveToPlayer(MyPlayer);
        //Places enemy Rectangle at new Position
        Canvas.SetTop(enemy.Model, enemy.YCoord);
        Canvas.SetLeft(enemy.Model, enemy.XCoord);
      }
    }
    private void CheckforHit(object sender, EventArgs e)
    {
      foreach(Enemy enemy in MyEnemy)
      {
        foreach(Bullet bullet in MyPlayer.Bullets)
        {
          // Checks if bullet hits Enemyhitbox
          if (enemy.Hitbox.IntersectsWith(bullet.Hitbox))
          {
            enemy.TakeDamage(20);
          }
        }

        //Checks if Enemy hits Playerhitbox
        if (enemy is EnemyMelee && enemy.Hitbox.IntersectsWith(MyPlayer.Hitbox))
        {
          enemy.Attack(MyPlayer);
        }
      }
    }

    private void RemoveEnemy(object sender, EventArgs e)
    {
     List<Enemy> deleteList = new List<Enemy>();
     foreach(Enemy enemy in MyEnemy)
      {
        // Search for Enemies with 0 or less health
        if(enemy.Health <= 0)
        {
          //Add them to deleteList
          deleteList.Add(enemy);
        }
      }

     // Delete them off the canvas
     foreach(Enemy enemy in deleteList)
     {
        MyEnemyCanvas.Children.Remove(enemy.Model);
        MyEnemy.Remove(enemy);
     }


    }

  }
}
