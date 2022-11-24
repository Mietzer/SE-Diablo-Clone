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
  public class EnemyRareRanged : EnemyRanged
  {
    public EnemyRareRanged(int x, int y, int heigth, int width, int steplength, int health, int damage) : base(x, y, heigth, width, steplength, health, damage)
    {
      this.Health = base.Health * 10;
      Image = ImageImporter.Import(ImageCategory.RANGED, "rare-walking-left.gif");
      Hitbox = new Rect(x, y + 19, width, heigth - 19);
    }
    
    public override void RefreshHitbox()
    {
      this.Hitbox = new Rect(XCoord, YCoord + 19, Width, Height - 19);
    }

    public override void KeepDistance(Player player)
    {
      Direction lastDirection = Direction;
      bool oldIsMoving = IsMoving;
      bool oldIsAttacking = IsAttacking;
      base.KeepDistance(player);
      if (lastDirection != Direction || oldIsMoving != IsMoving || oldIsAttacking)
      {
        string directionString = Direction.ToString().ToLower();
        Image = ImageImporter.Import(ImageCategory.RANGED, "rare-walking-" + directionString + ".gif");
      }
    }
    
    public override void StopMovement(object? sender, EventArgs e)
    {
      bool oldIsMoving = IsMoving;
      IsMoving = false;
      if (oldIsMoving != IsMoving && !IsAttacking)
      {
        string directionString = Direction.ToString().ToLower();
        Image = ImageImporter.Import(ImageCategory.RANGED, "rare-standing-" + directionString + ".gif");
      }
    }
  }
}
