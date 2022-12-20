using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model.Object;
using olbaid_mortel_7720.MVVM.Viewmodel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace olbaid_mortel_7720.MVVM.Model.Enemies
{
  public class EnemyBoss : Enemy
  {

    public EnemyBoss(int x, int y, MapViewModel mapModel) : base(x, y, 64 * 2, 32 * 2, 5, 1000, 10, mapModel)
    {
      Image = ImageImporter.Import(ImageCategory.BOSS, "boss-walking-left.gif");
      Hitbox = new Rect(x, y + 21 * 2, Width, Height - 21 * 2);
      GameTimer.ExecuteWithInterval(15, delegate (EventArgs e)
      {
        IsAttacking = true;
      });
    }

    public override void RefreshHitbox()
    {
      this.Hitbox = new Rect(XCoord, YCoord + 27, Width, Height - 27);
    }

    public void ChangePhase()
    {
      if(Health <= 750 && Health > 500)
      {
        StepLength = 6;
        Damage = 12;
      }
      else if(Health <= 500 && Health > 250)
      {
        StepLength = 7;
        Damage = 14;
      }
      else if(Health <= 250)
      {
        StepLength = 10;
        Damage = 18;
      }
    }
    public void AttackCoolDown()
    {
      this.IsAttacking = false;
    }
    public override void Attack(Player player)
    {
      bool oldIsAttacking = IsAttacking;
      IsAttacking = true;
      IsMoving = false;
      if (oldIsAttacking != IsAttacking)
      {
        string directionString = Direction.ToString().ToLower();
        Image = ImageImporter.Import(ImageCategory.BOSS, "boss-attacking-" + directionString + ".gif");
      }
      player.TakeDamage(Damage);
    }

    public override ReadOnlyCollection<CollectableObject> GetPossibleDrops()
    {
      List<CollectableObject> drops = new List<CollectableObject>();
      drops.Add(new LevelKey(1920 / 2, 1080 / 2));
      return drops.AsReadOnly();
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
        Image = ImageImporter.Import(ImageCategory.BOSS, "boss-walking-" + directionString + ".gif");
      }
    }

    public override void StopMovement(EventArgs e)
    {
      bool oldIsMoving = IsMoving;
      IsMoving = false;
      if (oldIsMoving != IsMoving && !IsAttacking)
      {
        string directionString = Direction.ToString().ToLower();
        Image = ImageImporter.Import(ImageCategory.BOSS, "boss-standing-" + directionString + ".gif");
      }
    }
  }
}
