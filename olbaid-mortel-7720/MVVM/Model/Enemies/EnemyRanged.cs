using olbaid_mortel_7720.Helper;
using System.Timers;
using System;
using olbaid_mortel_7720.MVVM.Utils;
using olbaid_mortel_7720.Engine;
using System.Collections.Generic;
using System.Windows;

//TODO: CodeCleanup, Regions, Kommentare

namespace olbaid_mortel_7720.MVVM.Model.Enemies
{
  public class EnemyRanged : Enemy
  {
    #region Properties
    
    private readonly Timer shootCooldownTimer;
    
    #endregion Properties

    #region Constructor
    public EnemyRanged(int x, int y) : base(x, y, 64, 32, 3, 50, 2)
    {
      Image = ImageImporter.Import(ImageCategory.RANGED, "ranged-walking-left.gif");
      Hitbox = new Rect(x, y + 22, Width, Height - 22);
      shootCooldownTimer = new Timer();
      shootCooldownTimer.Interval = 3000;
      shootCooldownTimer.AutoReset = false;
      IsAttacking = false;
    }

    #endregion Constructor

    #region Methods
    public override void RefreshHitbox()
    {
      this.Hitbox = new Rect(XCoord, YCoord + 22, Width, Height - 22);
    }
    
    public void ShotCoolDown() // Starts shot timer for enemies
    {
      shootCooldownTimer.Start();
      IsAttacking = false;
      shootCooldownTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
    }

    private void OnTimedEvent(object source, ElapsedEventArgs e) // Shoots then resets timer
    {
      IsAttacking = true;
      shootCooldownTimer.Stop();
    }

    public virtual void KeepDistance(Player player)
    {
      const int tolerance = 5;
      const int nearestDistance = 150;
      const int farthestDistance = 200;
      Direction lastDirection = Direction;
      bool oldIsMoving = IsMoving;
      bool oldIsAttacking = base.IsAttacking;
      int xDistance = Math.Abs(player.XCoord - this.XCoord);
      int yDistance = Math.Abs(player.YCoord - this.YCoord);

      List<Direction> directions = new List<Direction>();

      //Checks distance between player and enemy and checks where to move

      if(xDistance >= nearestDistance && xDistance <= farthestDistance && yDistance >= nearestDistance && yDistance <= farthestDistance)
      {
        return;
      }
      if (xDistance < nearestDistance&& player.XCoord + player.Width / 2 + tolerance * 2 < XCoord + Width / 2 && XCoord < GlobalVariables.MaxX)
        directions.Add(Direction.Right);
      if (xDistance < nearestDistance && player.XCoord + player.Width / 2 - tolerance * 2 > XCoord + Width / 2 && XCoord > GlobalVariables.MinX)
        directions.Add(Direction.Left);
      if (yDistance < nearestDistance && player.YCoord + player.Height / 2 + tolerance < YCoord + Height / 2 && YCoord < GlobalVariables.MaxY)
        directions.Add(Direction.Down);
      if (yDistance < nearestDistance && player.YCoord + player.Height / 2 - tolerance > YCoord + Height / 2 && YCoord > GlobalVariables.MinY)
        directions.Add(Direction.Up);
      if (xDistance > farthestDistance && player.XCoord + player.Width / 2 + tolerance * 2 < XCoord + Width / 2 && XCoord < GlobalVariables.MaxX)
        directions.Add(Direction.Left);
      if (xDistance > farthestDistance && player.XCoord + player.Width / 2 - tolerance * 2 > XCoord + Width / 2 && XCoord >= GlobalVariables.MinX)
        directions.Add(Direction.Right);
      if (yDistance > farthestDistance && player.YCoord + player.Height / 2 + tolerance < YCoord + Height / 2 && YCoord < GlobalVariables.MaxY)
        directions.Add(Direction.Up);
      if (yDistance > farthestDistance && player.YCoord + player.Height / 2 - tolerance > YCoord + Height / 2 && YCoord > GlobalVariables.MinY)
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
      base.IsAttacking = false;

      //Switch-Case for enemy movement

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
        Image = ImageImporter.Import(ImageCategory.RANGED, "ranged-walking-" + directionString + ".gif");
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
      if (oldIsMoving != IsMoving && !base.IsAttacking)
      {
        string directionString = Direction.ToString().ToLower();
        Image = ImageImporter.Import(ImageCategory.RANGED, "ranged-standing-" + directionString + ".gif");
      }
    }

    #endregion Methods
  }
}
