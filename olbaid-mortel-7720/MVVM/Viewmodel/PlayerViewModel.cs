using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace olbaid_mortel_7720.MVVM.Viewmodel
{
  public class PlayerViewModel :  NotifyObject
  {
    #region Properties
    public Player MyPlayer { get; set; }
    private int MinX;
    private int MinY;
    private int MaxX;
    private int MaxY;

    private string ShotName = "ShotPlayer";

    private Canvas MyPlayerCanvas;
    #endregion Properties

    public PlayerViewModel(Player player, Canvas playerCanvas)
    {
      MyPlayer = player;
      MyPlayerCanvas = playerCanvas;

      //Save Borders of the Canvas
      MinX = player.XCoordMin;
      MinY = player.YCoordMin;
      MaxX = player.XCoordMax;
      MaxY = player.YCoordMax;

      InitTimer();
    }
    #region Methods
    /// <summary>
    /// Initialize a dispatcher timer to move the bullets
    /// </summary>
    public void InitTimer()
    {
      DispatcherTimer shotMovementTimer = new();
      shotMovementTimer.Tick += new EventHandler(MoveShots);
      shotMovementTimer.Interval = TimeSpan.FromMilliseconds(100);
      shotMovementTimer.Start();

      DispatcherTimer deleteShotTimer = new();
      deleteShotTimer.Tick += new EventHandler(DeleteShots);
      deleteShotTimer.Interval = TimeSpan.FromSeconds(2);
      deleteShotTimer.Start();
    }
    
    /// <summary>
    /// Event to move the shots
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void MoveShots(object? sender, EventArgs e)
    {
      //How many Pixels the bullet should move everytime
      int velocity = 30;

      foreach (FrameworkElement item in MyPlayerCanvas.Children)
      {
        if (item is Rectangle && item.Name == ShotName) //Find shots for our Player
        {
          Bullet b = MyPlayer.Bullets.Where(s => s.Rectangle == item).FirstOrDefault();
          b?.Move(velocity);
        }
      }
    }
    
    /// <summary>
    /// Delete shots that are out of the canvas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void DeleteShots(object? sender, EventArgs e)
    {
      foreach (FrameworkElement item in MyPlayerCanvas.Children)
      {
        if (item is Rectangle && item.Name == ShotName) //Find shots for our Player
        {
          Bullet b = MyPlayer.Bullets.Where(s => s.Rectangle == item).FirstOrDefault();
          //Remove Shot if out of Border
          if (Canvas.GetLeft(item) < MinX - item.Width || Canvas.GetLeft(item) > MaxX
           || Canvas.GetTop(item) < MinY - item.Height || Canvas.GetTop(item) > MaxY)
          {
            MyPlayerCanvas.Children.Remove(item);
            MyPlayer.Bullets.Remove(b);
            break; //Break, cause List has been changed
          }
        }
      }
    }

    /// <summary>
    /// Creates a new Bullet
    /// </summary>
    /// <param name="p">Targetpoint for Bullet</param>
    public void Shoot(Point p)
    {
      double playerMidX = MyPlayer.XCoord + MyPlayer.Width / 2
           , playerMidY = MyPlayer.YCoord + MyPlayer.Height / 2;

      // Direction the bullet is going
      Vector vector = new Vector(p.X - playerMidX, p.Y - playerMidY);
      vector.Normalize();
      Brush bulletImage = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/bullet.png")));
      Bullet bullet = new Bullet(5, 10, vector, bulletImage, ShotName);
      
      //Add to Player
      MyPlayer.IsShooting = true;
      MyPlayer.Bullets.Add(bullet);

      //Shot on Players left
      if (vector.X < 0)
      {
        playerMidX -= bullet.Rectangle.Width;
        
        //Above
        if (vector.Y < 0)
        {
          playerMidY -= bullet.Rectangle.Height;
        }
      }
      
      //Shot on Players right and above
      else if (vector.X > 0 && vector.Y < 0)
      {
        playerMidY -= bullet.Rectangle.Height;
      }
      
      // Add to Canvas
      bullet.Show(MyPlayerCanvas, playerMidX, playerMidY);
      
      MyPlayer.UpdateViewDirection(GetPlayerView(vector.X, vector.Y));
    }
    
    public void StopShooting()
    {
      MyPlayer.IsShooting = false;
    }
    
    /// <summary>
    /// Get the ViewDirection of the Player
    /// </summary>
    /// <param name="x">X of the view vector</param>
    /// <param name="y">Y of the view vector</param>
    /// <returns></returns>
    private Direction GetPlayerView(double x, double y)
    {
      const double proportion = 0.8;
      if (x > 0 && y > -proportion && y < proportion)
      {
        return Direction.Right;
      }
      else if (x < 0 && y > -proportion && y < proportion)
      {
        return Direction.Left;
      }
      else if (y > 0 && x > -proportion && x < proportion)
      {
        return Direction.Down;
      }
      else if (y < 0 && x > -proportion && x < proportion)
      {
        return Direction.Up;
      }
      else
      {
        return Direction.Down;
      }
    }

    #endregion Methods

    #region Commands
    #endregion Commands

  }
}
