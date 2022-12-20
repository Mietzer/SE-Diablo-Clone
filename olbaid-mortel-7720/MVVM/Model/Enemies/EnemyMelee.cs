using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model.Object;
using olbaid_mortel_7720.MVVM.Viewmodel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    public void AttackCoolDown()
    {
      this.IsAttacking = false;
    }

    public override ReadOnlyCollection<CollectableObject> GetPossibleDrops()
    {
      int x = (int)(Hitbox.X + Hitbox.Width / 2);
      int y = (int)(Hitbox.Y + Hitbox.Height / 2);
      List<CollectableObject> drops = new List<CollectableObject>();
      drops.Add(new Medicine(200, 30, x, y));
      drops.Add(new Paralysis(200, 100, x, y));
      drops.Add(new Protection(200, x, y));
      drops.Add(new WeaponUpgrade(200, 5, x, y));
      return drops.AsReadOnly();
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
      Direction lastDirection = Direction;
      bool oldIsMoving = IsMoving;
      bool oldIsAttacking = IsAttacking;
      List<Direction> directions = DecideDirectionPath(player, Hitbox.X + Hitbox.Width / 2, Hitbox.Y + Hitbox.Height / 2);

      Direction item;
      if (directions.Count == 0)
      {
        StopMovement(EventArgs.Empty);
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

    public override void StopMovement(EventArgs e)
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
    public EnemyMelee(int x, int y, MapViewModel mapModel) : base(x, y, 64, 32, 3, 100, 2, mapModel)
    {
      Image = ImageImporter.Import(ImageCategory.MELEE, "melee-walking-left.gif");
      Hitbox = new Rect(x, y + 27, Width, Height - 27);
      GameTimer.ExecuteWithInterval(15, delegate (EventArgs e)
      {
        IsAttacking = true;
      });
    }

    #endregion Constructor
  }
}
