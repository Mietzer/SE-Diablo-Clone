using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

    //Movement Direction
    public bool moveLeft { get; set; }
    public bool moveRight { get; set; }
    public bool moveUp { get; set; }
    public bool moveDown { get; set; }

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
      DispatcherTimer movementTimer = new();
      movementTimer.Tick += new EventHandler(Move);
      movementTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);
      movementTimer.Start();

      DispatcherTimer shotMovementTimer = new();
      shotMovementTimer.Tick += new EventHandler(MoveShots);
      shotMovementTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
      shotMovementTimer.Start();
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
      List<FrameworkElement> deleteList = new List<FrameworkElement>();

      foreach (FrameworkElement item in MyPlayerCanvas.Children)
      {
        if (item is Rectangle && item.Name == ShotName) //Find shots for our Player
        {
          Bullet b = MyPlayer.Bullets.Where(s => s.Rectangle == item).FirstOrDefault();
          b?.Move(velocity);
          
          if (Canvas.GetLeft(item) < MinX - item.Width || Canvas.GetLeft(item) > MaxX
           || Canvas.GetTop(item) < MinY - item.Height || Canvas.GetTop(item) > MaxY)
          {
            //Remove from List and Register Rectangle to remove from Canvas
            deleteList.Add(item);
            MyPlayer.Bullets.Remove(b);
          }
        }
      }
      //Now delete
      foreach (FrameworkElement item in deleteList)
        MyPlayerCanvas.Children.Remove(item);
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

    private void Move(object sender, EventArgs e)
    {
      if (moveDown)
        MyPlayer.Move(sender, Key.S);
      else if (moveUp)
        MyPlayer.Move(sender, Key.W);

      if (moveLeft)
        MyPlayer.Move(sender, Key.A);
      else if (moveRight)
        MyPlayer.Move(sender, Key.D);
    }
    #endregion Methods

    #region Commands
    #endregion Commands

  }
}
