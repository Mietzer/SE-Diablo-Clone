using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace olbaid_mortel_7720.MVVM.Viewmodel
{
  public class PlayerViewModel : NotifyObject
  {
    #region Properties
    public Player MyPlayer { get; set; }

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
      MyPlayer.StopMovement("Initial", null);
      MyPlayerCanvas = playerCanvas;

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

          if (Canvas.GetLeft(item) < GlobalVariables.MinX - item.Width || Canvas.GetLeft(item) > GlobalVariables.MaxX
           || Canvas.GetTop(item) < GlobalVariables.MinY - item.Height || Canvas.GetTop(item) > GlobalVariables.MaxY)
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
      double playerShootX = MyPlayer.XCoord + MyPlayer.Width / 2
           , playerShootY = MyPlayer.YCoord + MyPlayer.Height / 4 * 3;

      // Direction the bullet is going
      Vector vector = new Vector(p.X - playerShootX, p.Y - playerShootY);
      vector.Normalize();
      Brush bulletImage = new ImageBrush(RessourceImporter.Import(ImageCategory.BULLETS, "bullet.png"));
      Bullet bullet = new Bullet(2, 4, vector, bulletImage, ShotName);

      //Add to Player
      MyPlayer.IsShooting = true;
      MyPlayer.Bullets.Add(bullet);

      //Shot on Players left
      if (vector.X < 0)
      {
        playerShootX -= bullet.Rectangle.Width;
        
        //Above
        if (vector.Y < 0)
        {
          playerShootY -= bullet.Rectangle.Height;
        }
      }

      //Shot on Players right and above
      else if (vector.X > 0 && vector.Y < 0)
      {
        playerShootY -= bullet.Rectangle.Height;
      }

      // Add to Canvas
      bullet.Show(MyPlayerCanvas, playerShootX, playerShootY);
      
      MyPlayer.UpdateViewDirection(GetPlayerView(vector.X, vector.Y));
    }

    /// <summary>
    /// Method stopping the shooting action
    /// </summary>
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

    /// <summary>
    /// Method to move the Player Canvas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

      if (!moveDown && !moveUp && !moveLeft && !moveRight)
        MyPlayer.StopMovement(sender, e);
    }
    #endregion Methods

    #region Commands
    #endregion Commands

  }
}
