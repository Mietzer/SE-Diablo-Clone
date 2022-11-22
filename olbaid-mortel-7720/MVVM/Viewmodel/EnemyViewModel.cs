using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Threading;
using WpfAnimatedGif;
using System.Windows;
using System.Windows.Media;
using olbaid_mortel_7720.MVVM.Utils;
using System.Linq;
using System.Windows.Shapes;
using olbaid_mortel_7720.MVVM.Model.Enemies;
//TODO: CodeCleanup, Regions, Kommentare
namespace olbaid_mortel_7720.MVVM.Viewmodel
{
  public class EnemyViewModel : NotifyObject
  {
    public List<Enemy> MyEnemies = new List<Enemy>();
    private Player MyPlayer { get; set; }

    private Canvas MyEnemyCanvas;
    
    private string ShotName = "ShotEnemy";

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

      DispatcherTimer movementShots = new();
      movementTimer.Tick += new EventHandler(MoveShots);
      movementTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);
      movementTimer.Start();

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
        if(enemy is EnemyMelee)
        {
          (enemy as EnemyMelee).MoveToPlayer(MyPlayer);

          //Places enemy Image at new Position
          ImageBehavior.SetAnimatedSource(enemy.Model, enemy.Image);
          Canvas.SetTop(enemy.Model, enemy.YCoord);
          Canvas.SetLeft(enemy.Model, enemy.XCoord);
        }
        if(enemy is EnemyRanged)
        {
          (enemy as EnemyRanged).KeepDistance(MyPlayer);
          ImageBehavior.SetAnimatedSource(enemy.Model, enemy.Image);
          Canvas.SetTop(enemy.Model, enemy.YCoord);
          Canvas.SetLeft(enemy.Model, enemy.XCoord);
        }
        
      }
    }
    private void CheckforHit(object sender, EventArgs e)
    {
      foreach (Enemy enemy in MyEnemies)
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

        if(enemy is EnemyRanged)
        {
          if ((enemy as EnemyRanged).isShooting)
          {
            
            Point p = new Point(MyPlayer.XCoord, MyPlayer.YCoord);
            Shoot(enemy as EnemyRanged, p);
            (enemy as EnemyRanged).ShotCoolDown();
          }
          else
            (enemy as EnemyRanged).ShotCoolDown();
        }

        foreach(Bullet bullet in enemy.Bullets)
        {
          if (bullet.Hitbox.IntersectsWith(MyPlayer.Hitbox))
          {
            enemy.Attack(MyPlayer);
          }
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
    private void Shoot(EnemyRanged enemy, Point p)
    {
      double enemyShootX = enemy.XCoord + MyPlayer.Width / 2;
      double enemyShootY = enemy.YCoord + MyPlayer.Height / 4 * 3;

      // Direction the bullet is going
      Vector vector = new Vector(p.X - enemyShootX, p.Y - enemyShootY);
      vector.Normalize();
      Brush bulletImage = new ImageBrush(RessourceImporter.Import(ImageCategory.BULLETS, "bullet.png"));
      Bullet bullet = new Bullet(2, 4, vector, bulletImage, ShotName);

      //Add to Enemies
      enemy.Bullets.Add(bullet);

      //Shot on Enemies left
      if (vector.X < 0)
      {
        enemyShootX -= bullet.Rectangle.Width;

        //Above
        if (vector.Y < 0)
        {
          enemyShootY -= bullet.Rectangle.Height;
        }
      }

      //Shot on Enemies right and above
      else if (vector.X > 0 && vector.Y < 0)
      {
        enemyShootY -= bullet.Rectangle.Height;
      }

      // Add to Canvas
      bullet.Show(MyEnemyCanvas, enemyShootX, enemyShootY);
    }

    private void MoveShots(object sender, EventArgs e)
    {
      //How many Pixels the bullet should move everytime
      int velocity = 20;
      List<FrameworkElement> deleteList = new List<FrameworkElement>();

      foreach (FrameworkElement item in MyEnemyCanvas.Children)
      {
        foreach(Enemy enemy in MyEnemies)
        {
          if (item is Rectangle && item.Name == ShotName) //Find shots for each enemy
          {
            //TODO: Anpassen auf Enemies und Bulletentfernung fixen

            Bullet b = enemy.Bullets.Where(s => s.Rectangle == item).FirstOrDefault();
            b?.Move(velocity);

            if (Canvas.GetLeft(item) < GlobalVariables.MinX - item.Width || Canvas.GetLeft(item) > GlobalVariables.MaxX
             || Canvas.GetTop(item) < GlobalVariables.MinY - item.Height || Canvas.GetTop(item) > GlobalVariables.MaxY)
            {
              //Remove from List and Register Rectangle to remove from Canvas
              deleteList.Add(item);
              enemy.Bullets.Remove(b);
            }
          }
        }
      }
      //Now delete
      foreach (FrameworkElement item in deleteList)
        MyEnemyCanvas.Children.Remove(item);
    }


  }
  }

