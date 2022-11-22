using olbaid_mortel_7720.Helper;
using System.Timers;
using System;
using olbaid_mortel_7720.MVVM.Utils;
using olbaid_mortel_7720.Engine;
using System.Collections.Generic;
//TODO: CodeCleanup, Regions, Kommentare

namespace olbaid_mortel_7720.MVVM.Model.Enemies
{
  public class EnemyRanged : Enemy
  {
    private Timer t;

    public bool isShooting { get; private set; }

    public Timer T
    {
      get { return t; }
      private set { t = value; }
    }
    public EnemyRanged(int x, int y, int heigth, int width, int steplength, int health, int damage) : base(x, y, heigth, width, steplength, health, damage)
    {
      Image = RessourceImporter.Import(ImageCategory.RANGED, "ranged-walking-left.gif");
      t = new Timer();
      t.Interval = 2500;
      t.AutoReset = false;
      isShooting = false;
    }

    public void ShotCoolDown() // Starts shot timer for enemies
    {
      t.Start();
      isShooting = false;
      t.Elapsed += new ElapsedEventHandler(OnTimedEvent);
    }

    private void OnTimedEvent(object source, ElapsedEventArgs e) // Shoots then resets timer
    {
      isShooting = true;
      t.Stop();
    }

    public void KeepDistance(Player player)
    {
      const int tolerance = 5;
      Direction lastDirection = Direction;
      bool oldIsMoving = IsMoving;
      bool oldIsAttacking = IsAttacking;
      int xDistance = Math.Abs(player.XCoord - this.XCoord);
      int yDistance = Math.Abs(player.YCoord - this.YCoord);

      List<Direction> directions = new List<Direction>();
      
      if (xDistance <= 100 && player.XCoord + player.Width / 2 + tolerance * 2 < XCoord + Width / 2 && XCoord < GlobalVariables.MaxX)
        directions.Add(Direction.Right);
      if (xDistance <= 100 && player.XCoord + player.Width / 2 - tolerance * 2 > XCoord + Width / 2 && XCoord > GlobalVariables.MinX)
        directions.Add(Direction.Left);
      if (yDistance <= 100 && player.YCoord + player.Height / 2 + tolerance < YCoord + Height / 2 && YCoord < GlobalVariables.MaxY)
        directions.Add(Direction.Down);
      if (yDistance <= 100 && player.YCoord + player.Height / 2 - tolerance > YCoord + Height / 2 && YCoord > GlobalVariables.MinY)
        directions.Add(Direction.Up);
      if (xDistance >= 100 && player.XCoord + player.Width / 2 + tolerance * 2 < XCoord + Width / 2 && XCoord < GlobalVariables.MaxX)
        directions.Add(Direction.Left);
      if (xDistance >= 100 && player.XCoord + player.Width / 2 - tolerance * 2 > XCoord + Width / 2 && XCoord > GlobalVariables.MinX)
        directions.Add(Direction.Right);
      if (yDistance >= 100 && player.YCoord + player.Height / 2 + tolerance < YCoord + Height / 2 && YCoord < GlobalVariables.MaxY)
        directions.Add(Direction.Up);
      if (yDistance >= 100 && player.YCoord + player.Height / 2 - tolerance > YCoord + Height / 2 && YCoord > GlobalVariables.MinY)
        directions.Add(Direction.Down);


      Direction item;
      if (directions.Count == 0)
      {
        StopMovement(null, null);
        return;
      }
      if (directions.Contains(lastDirection) && sameDirectionCounter <= MAX_SAME_DIRECTION)
      {
        item = lastDirection;
        sameDirectionCounter++;
      }
      else
      {
        Random random = new Random();
        int index = random.Next(0, directions.Count);
        item = directions[index];
        sameDirectionCounter = 0;
      }
      IsMoving = true;
      IsAttacking = false;
      switch (item)
      {
        case Direction.Up:
          MoveUp();
          break;
        case Direction.Down:
          MoveDown();
          break;
        case Direction.Left:
          MoveLeft();
          break;
        case Direction.Right:
          MoveRight();
          break;
      }
      if (lastDirection != item || oldIsMoving != IsMoving || oldIsAttacking)
      {
        string directionString = Direction.ToString().ToLower();
        Image = RessourceImporter.Import(ImageCategory.RANGED, "ranged-walking-" + directionString + ".gif");
      }
    }
    public override void Attack(Player player)
    {
      player.TakeDamage(Damage);
    }

    public override void StopMovement(object? sender, EventArgs e)
    {
      bool oldIsMoving = IsMoving;
      IsMoving = false;
      if (oldIsMoving != IsMoving && !IsAttacking)
      {
        string directionString = Direction.ToString().ToLower();
        Image = RessourceImporter.Import(ImageCategory.RANGED, "ranged-standing-" + directionString + ".gif");
      }
    }
  }
}
