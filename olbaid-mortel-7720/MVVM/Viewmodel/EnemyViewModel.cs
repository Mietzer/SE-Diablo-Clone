using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Model.Enemies;
using olbaid_mortel_7720.MVVM.Model.Object;
using olbaid_mortel_7720.MVVM.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfAnimatedGif;
//TODO: CodeCleanup, Regions, Kommentare
namespace olbaid_mortel_7720.MVVM.Viewmodel
{
  public class EnemyViewModel : NotifyObject
  {
    #region Properties
    public List<Enemy> MyEnemies = new List<Enemy>();
    private Player MyPlayer { get; set; }

    private Canvas MyEnemyCanvas;

    private string ShotName = "ShotEnemy";

    private string Tag;

    #endregion Properties

    #region Constructor
    public EnemyViewModel(List<Enemy> myEnemies, Canvas myEnemyCanvas, Player player)
    {
      this.MyEnemies = myEnemies;
      this.MyEnemyCanvas = myEnemyCanvas;
      this.MyPlayer = player;
      this.Tag = "Enemy";

      InitTimer();
    }

    #endregion Constructor

    #region Methods 
    public void InitTimer()
    {
      GameTimer timer = GameTimer.Instance;
      timer.GameTick += Move;
      timer.GameTick += RemoveEnemy;
      timer.GameTick += MoveShots;
      timer.GameTick += CheckforHit;
    }

    private void Move(EventArgs e)
    {
      foreach (Enemy enemy in MyEnemies)
      {
        if (enemy != null && enemy is EnemyMelee)
        {
          (enemy as EnemyMelee).MoveToPlayer(MyPlayer);

          //Places enemy Image at new Position
          ImageBehavior.SetAnimatedSource(enemy.Model, enemy.Image);
          Canvas.SetTop(enemy.Model, enemy.YCoord);
          Canvas.SetLeft(enemy.Model, enemy.XCoord);
        }
        if (enemy != null && enemy is EnemyRanged)
        {
          (enemy as EnemyRanged).KeepDistance(MyPlayer);
          ImageBehavior.SetAnimatedSource(enemy.Model, enemy.Image);
          Canvas.SetTop(enemy.Model, enemy.YCoord);
          Canvas.SetLeft(enemy.Model, enemy.XCoord);
        }

      }
    }
    private void CheckforHit(EventArgs e)
    {
      //Hit on enemmy
      foreach (Bullet bullet in MyPlayer.Bullets.Where(b => !b.HasHit))
      {
        // Checks if bullet hits Enemyhitbox
        Enemy enemy = MyEnemies?.FirstOrDefault(ene => ene.Hitbox.IntersectsWith(bullet.Hitbox));
        enemy?.TakeDamage(MyPlayer.CurrentWeapon.Damage);
        //Mark Bullet as deletable
        if (enemy != null)
          bullet.HasHit = true;
      }

      //Hit on Player
      foreach (Enemy enemy in MyEnemies)
      {
        //Checks if Enemy hits Playerhitbox
        if (enemy != null && enemy is EnemyMelee && enemy.Hitbox.IntersectsWith(MyPlayer.Hitbox))
        {
          if((enemy as EnemyMelee).IsAttacking)
          {
            enemy.Attack(MyPlayer);
            (enemy as EnemyMelee).AttackCoolDown();
          }
        }

        if (enemy != null && enemy is EnemyRanged)
        {
          if ((enemy as EnemyRanged).IsAttacking)
          {
            //TODO: Maybe some randomness to weaken Enemys (Maybe own Property for Enemy for shot accuracy (Boss and Rare -> Better, Normal ->worse)
            Point p = new Point(MyPlayer.XCoord + MyPlayer.Width / 2, MyPlayer.YCoord + MyPlayer.Height / 2);
            Shoot(enemy as EnemyRanged, p);
            (enemy as EnemyRanged).ShotCoolDown();
          }
          else
            (enemy as EnemyRanged).ShotCoolDown();
        }

        //Checks if enemy bullet hits Playerhitbox
        foreach (Bullet bullet in enemy.Bullets)
        {
          if (!bullet.HasHit && bullet.Hitbox.IntersectsWith(MyPlayer.Hitbox))
          {
            enemy.Attack(MyPlayer);
            bullet.HasHit = true;
          }
        }
      }
    }

    private void RemoveEnemy(EventArgs e)
    {
      List<Enemy> deleteList = new List<Enemy>();
      List<Bullet> deleteBullets = new List<Bullet>();
      foreach (Enemy enemy in MyEnemies)
      {
        // Search for Enemies with 0 or less health
        if (enemy != null && enemy.Health <= 0)
        {
          Random rnd = new Random();
          CollectableObject collectable = enemy.GetPossibleDrops()[rnd.Next(0, enemy.GetPossibleDrops().Count)];
          collectable.Spawn(MyEnemyCanvas, enemy.XCoord - enemy.Width / 2, enemy.YCoord - enemy.Height / 2);
        
          DoubleAnimation animation = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(350), FillBehavior.Stop);
          animation.Completed += delegate
          {
            MyEnemyCanvas.Children.Remove(enemy.Model);
          };
          enemy.Model.BeginAnimation(UIElement.OpacityProperty, animation);
          //Add them to deleteList
          deleteList.Add(enemy);

          //Deletes bullets, that would not be deleted because of enemy deletion
          foreach (Bullet bullet in enemy.Bullets)
          {
            deleteBullets.Add(bullet);
          }
        }
      }

      // Delete them off the canvas
      foreach (Enemy enemy in deleteList)
      {
        MyEnemies.Remove(enemy);
      }

      //Delete bullets of the canvas
      foreach (Bullet bullet in deleteBullets)
      {
        MyEnemyCanvas.Children.Remove(bullet.Rectangle);
      }
    }
    private void Shoot(EnemyRanged enemy, Point p)
    {
      if (enemy.StayAggro)
      {
        double enemyShootX = enemy.XCoord + MyPlayer.Width / 2;
        double enemyShootY = enemy.YCoord + MyPlayer.Height / 4 * 3;

        // Direction the bullet is going
        Vector vector = new Vector(p.X - enemyShootX, p.Y - enemyShootY);
        vector.Normalize();
        Brush bulletImage = new ImageBrush(ImageImporter.Import(ImageCategory.BULLETS, "ranged-bullet.png"));
        Bullet bullet = new Bullet(3, 6, vector, bulletImage, ShotName);

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
    }
      

    private void MoveShots(EventArgs e)
    {
      //How many Pixels the bullet should move everytime
      int velocity = 10;
      List<FrameworkElement> deleteList = new List<FrameworkElement>();

      foreach (FrameworkElement item in MyEnemyCanvas.Children)
      {
        foreach (Enemy enemy in MyEnemies)
        {
          if (item is Rectangle && item.Name == ShotName && enemy != null) //Find shots for each enemy
          {
            Bullet b = enemy.Bullets.Where(s => s.Rectangle == item).FirstOrDefault();
            if (b != null)
            {
              b.Move(velocity);

              if (Canvas.GetLeft(item) < GlobalVariables.MinX - item.Width || Canvas.GetLeft(item) > GlobalVariables.MaxX
               || Canvas.GetTop(item) < GlobalVariables.MinY - item.Height || Canvas.GetTop(item) > GlobalVariables.MaxY
               || b.HasHit
               || enemy.Barriers.Any(barrier => barrier.Type == Barrier.BarrierType.Wall && barrier.Hitbox.IntersectsWith(b.Hitbox)))
              {
                //Remove from List and Register Rectangle to remove from Canvas
                deleteList.Add(item);
                enemy.Bullets.Remove(b);
              }
            }
          }
        }
      }
      //Now delete
      foreach (FrameworkElement item in deleteList)
        MyEnemyCanvas.Children.Remove(item);
    }

    #endregion Methods
  }
}

