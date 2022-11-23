using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace olbaid_mortel_7720.MVVM.Model.Enemies
{
  public class EnemyRareMelee : EnemyMelee
  {
    public EnemyRareMelee(int x, int y, int heigth, int width, int steplength, int health, int damage) : base(x, y, heigth, width, steplength, health, damage)
    {
      this.Health = base.Health * 10;
      Image = RessourceImporter.Import(ImageCategory.MELEE, "rare-walking-left.gif");
      Hitbox = new Rect(x, y + 22, width, heigth - 22);
    }
    
    public override void RefreshHitbox()
    {
      this.Hitbox = new Rect(XCoord, YCoord + 22, Width, Height - 22);
    }
    
    public override void Attack(Player player)
    {
      base.Attack(player);
      bool oldIsAttacking = IsAttacking;
      IsAttacking = true;
      IsMoving = false;
      if (oldIsAttacking != IsAttacking)
      {
        string directionString = Direction.ToString().ToLower();
        Image = RessourceImporter.Import(ImageCategory.MELEE, "melee-attacking-" + directionString + ".gif");
      }
    }
    
    public override void MoveToPlayer(Player player)
    {
      Direction lastDirection = Direction;
      bool oldIsMoving = IsMoving;
      bool oldIsAttacking = IsAttacking;
      base.MoveToPlayer(player);
      if (lastDirection != Direction || oldIsMoving != IsMoving || oldIsAttacking)
      {
        string directionString = Direction.ToString().ToLower();
        Image = RessourceImporter.Import(ImageCategory.MELEE, "rare-walking-" + directionString + ".gif");
      }
    }
    
    public override void StopMovement(object? sender, EventArgs e)
    {
      bool oldIsMoving = IsMoving;
      IsMoving = false;
      if (oldIsMoving != IsMoving && !IsAttacking)
      {
        string directionString = Direction.ToString().ToLower();
        Image = RessourceImporter.Import(ImageCategory.MELEE, "rare-standing-" + directionString + ".gif");
      }
    }
  }
}
