using olbaid_mortel_7720.GameplayClasses;
using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Threading;
using olbaid_mortel_7720.GameplayClasses;
using WpfAnimatedGif;

namespace olbaid_mortel_7720.MVVM.Viewmodel
{
  public class EnemyViewModel : NotifyObject
  {
    public List<Enemy> MyEnemies = new List<Enemy>();
    private Player MyPlayer { get; set; }

    private Canvas MyEnemyCanvas;
    
    private string Tag;

    public EnemyViewModel(List<Enemy> myEnemies, Canvas myEnemyCanvas, Player player)
    {
      this.MyEnemies = myEnemies;
      this.MyEnemyCanvas = myEnemyCanvas;
      this.MyPlayer = player;
      this.Tag = "Enemy";

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
      foreach(Enemy enemy in MyEnemies)
      {
        (enemy as EnemyMelee).MoveToPlayer(MyPlayer);
        
        //Places enemy Image at new Position
        ImageBehavior.SetAnimatedSource(enemy.Model, enemy.Image);
        Canvas.SetTop(enemy.Model, enemy.YCoord);
        Canvas.SetLeft(enemy.Model, enemy.XCoord);
      }
    }
    private void CheckforHit(object sender, EventArgs e)
    {
      foreach(Enemy enemy in MyEnemies)
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
     foreach(Enemy enemy in MyEnemies)
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
        MyEnemies.Remove(enemy);
     }
    }
  }
}
