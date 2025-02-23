﻿using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Viewmodel;
using System;
using System.Windows;

namespace olbaid_mortel_7720.MVVM.Model.Enemies
{
  public class EnemyRareMelee : EnemyMelee
  {
    public EnemyRareMelee(int x, int y, MapViewModel mapModel) : base(x, y, mapModel)
    {
      this.Health = base.Health * 2;
      this.Damage = base.Damage * 2;
      Image = ImageImporter.Import(ImageCategory.MELEE, "rare-walking-left.gif");
      Hitbox = new Rect(x, y + 22, Width, Height - 22);
    }

    public override void RefreshHitbox()
    {
      this.Hitbox = new Rect(XCoord, YCoord + 22, Width, Height - 22);
    }

    public override void Attack(Player player)
    {
      bool oldIsAttacking = IsAttacking;
      base.Attack(player);
      if (oldIsAttacking != IsAttacking)
      {
        string directionString = Direction.ToString().ToLower();
        Image = ImageImporter.Import(ImageCategory.MELEE, "rare-attacking-" + directionString + ".gif");
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
        Image = ImageImporter.Import(ImageCategory.MELEE, "rare-walking-" + directionString + ".gif");
      }
    }

    public override void StopMovement(EventArgs e)
    {
      bool oldIsMoving = IsMoving;
      IsMoving = false;
      if (oldIsMoving != IsMoving && !IsAttacking)
      {
        string directionString = Direction.ToString().ToLower();
        Image = ImageImporter.Import(ImageCategory.MELEE, "rare-standing-" + directionString + ".gif");
      }
    }
  }
}
