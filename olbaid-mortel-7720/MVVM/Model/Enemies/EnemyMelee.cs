using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.Helper;
using System;
using System.Collections.Generic;
using System.Windows;
//TODO: CodeCleanup, Regions, Kommentare

namespace olbaid_mortel_7720.MVVM.Model.Enemies
{
  public class EnemyMelee : Enemy
  {
    #region Methods
    public override void RefreshHitbox()
    {
      this.Hitbox = new Rect(XCoord, YCoord + 27, Width, Height - 27);
    }
    
    public override void Attack(Player player)
    {
      bool oldIsAttacking = IsAttacking;
      IsAttacking = true;
      IsMoving = false;
      if (oldIsAttacking != IsAttacking)
      {
        string directionString = Direction.ToString().ToLower();
        Image = ImageImporter.Import(ImageCategory.MELEE, "melee-attacking-" + directionString + ".gif");
      }
      player.TakeDamage(Damage);
    }

    public virtual void MoveToPlayer(Player player)
    {
      const int tolerance = 5;
      Direction lastDirection = Direction;
      bool oldIsMoving = IsMoving;
      bool oldIsAttacking = IsAttacking;
      List<Direction> directions = new List<Direction>();

      //Compares player coordinates with enemy coordianten then decides which direction to go
      if (player.XCoord + player.Width / 2 + tolerance * 2 < XCoord + Width / 2)
        directions.Add(Direction.Left);
      if (player.XCoord + player.Width / 2 - tolerance * 2 > XCoord + Width / 2)
        directions.Add(Direction.Right);
      if (player.YCoord + player.Height / 2 + tolerance < YCoord + Height / 2)
        directions.Add(Direction.Up);
      if (player.YCoord + player.Height / 2 - tolerance > YCoord + Height / 2)
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
        Image = ImageImporter.Import(ImageCategory.MELEE, "melee-walking-" + directionString + ".gif");
      }
    }

    public override void StopMovement(object? sender, EventArgs e)
    {
      bool oldIsMoving = IsMoving;
      IsMoving = false;
      if (oldIsMoving != IsMoving && !IsAttacking)
      {
        string directionString = Direction.ToString().ToLower();
        Image = ImageImporter.Import(ImageCategory.MELEE, "melee-standing-" + directionString + ".gif");
      }
    }
    #endregion Methods

    #region Constructor
    public EnemyMelee(int x, int y) : base(x, y, 64, 32, 3, 100, 2)
    {
      Image = ImageImporter.Import(ImageCategory.MELEE, "melee-walking-left.gif");
      Hitbox = new Rect(x, y + 27, Width, Height - 27);
    }

    #endregion Constructor
  }
}
