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
using System.Windows.Shapes;



namespace olbaid_mortel_7720.MVVM.Viewmodel
{
  public class PlayerViewModel : NotifyObject
  {
    #region Properties
    public Player MyPlayer { get; set; }

    //Movement Direction
    public bool MoveLeft { get; set; }
    public bool MoveRight { get; set; }
    public bool MoveUp { get; set; }
    public bool MoveDown { get; set; }

    public bool PrimaryWeapon { get; set; }
    public bool SecondaryWeapon { get; set; }

    private string shotName = "ShotPlayer";

    private Canvas myPlayerCanvas;
    #endregion Properties

    public PlayerViewModel(Player player, Canvas playerCanvas)
    {
      MyPlayer = player;
      MyPlayer.StopMovement(new InitialEventArgs());
      myPlayerCanvas = playerCanvas;

      InitTimer();
    }
    #region Methods
    /// <summary>
    /// Initialize a dispatcher timer to move the bullets
    /// </summary>
    public void InitTimer()
    {
      GameTimer timer = GameTimer.Instance;
      timer.Execute(Move, nameof(this.Move) + GetHashCode());
      timer.Execute(MoveShots, nameof(this.MoveShots) + GetHashCode());
    }

    public void Dispose()
    {
      //Remove from timer
      GameTimer timer = GameTimer.Instance;
      timer.RemoveByName(nameof(this.Move) + GetHashCode());
      timer.RemoveByName(nameof(this.MoveShots) + GetHashCode());

      //Kill Player
      MyPlayer.TakeDamage(MyPlayer.HealthPoints);
      MyPlayer = null;
    }

    /// <summary>
    /// Event to move the shots
    /// </summary>
    /// <param name="e"></param>
    public void MoveShots(EventArgs e)
    {
      //How many Pixels the bullet should move everytime
      int velocity = 10;
      List<FrameworkElement> deleteList = new List<FrameworkElement>();

      foreach (FrameworkElement item in myPlayerCanvas.Children)
      {
        if (item is Rectangle && item.Name == shotName) //Find shots for our Player
        {
          Bullet b = MyPlayer.Bullets.Where(s => s.Rectangle == item).FirstOrDefault();
          b?.Move(velocity);

          if (Canvas.GetLeft(item) < GlobalVariables.MinX - item.Width || Canvas.GetLeft(item) > GlobalVariables.MaxX
           || Canvas.GetTop(item) < GlobalVariables.MinY - item.Height || Canvas.GetTop(item) > GlobalVariables.MaxY
           || b.HasHit
           || MyPlayer.Barriers.Any(barrier => barrier.Type == Barrier.BarrierType.Wall && barrier.Hitbox.IntersectsWith(b.Hitbox)))
          {
            //Remove from List and Register Rectangle to remove from Canvas
            deleteList.Add(item);
            MyPlayer.Bullets.Remove(b);
          }
        }
      }
      //Now delete
      foreach (FrameworkElement item in deleteList)
        myPlayerCanvas.Children.Remove(item);
    }

    /// <summary>
    /// Creates a new Bullet
    /// </summary>
    /// <param name="p">Targetpoint for Bullet</param>
    public void Shoot(Point p)
    {
      double playerShootX = MyPlayer.XCoord + MyPlayer.Width / 2
         , playerShootY = MyPlayer.YCoord + MyPlayer.Height / 2;

      // Direction the bullet is going
      Vector vector = new Vector(p.X - playerShootX, p.Y - playerShootY);
      vector.Normalize();
      Bullet bullet = new Bullet(vector, MyPlayer.currentWeapon.Munition);

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
      bullet.Show(myPlayerCanvas, playerShootX, playerShootY);

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
    /// <param name="e"></param>
    private void Move(EventArgs e)
    {
      if (MoveDown)
        MyPlayer.Move(Key.S);
      else if (MoveUp)
        MyPlayer.Move(Key.W);

      if (MoveLeft)
        MyPlayer.Move(Key.A);
      else if (MoveRight)
        MyPlayer.Move(Key.D);

      if (!MoveDown && !MoveUp && !MoveLeft && !MoveRight)
        MyPlayer.StopMovement(e);
    }

    /// <summary>
    /// Method to Select the Player Weapon
    /// </summary>
    /// <param name="e"></param>
    private void WeaponSelection(EventArgs e)
    {
      if (PrimaryWeapon)
      {
        MyPlayer.WeaponSelection(Key.D1);
        PrimaryWeapon = false;
      }

      else if (SecondaryWeapon)
      {
        MyPlayer.WeaponSelection(Key.D2);
        SecondaryWeapon = false;
      }

    }
    #endregion Methods

    #region Commands
    #endregion Commands

  }
}
