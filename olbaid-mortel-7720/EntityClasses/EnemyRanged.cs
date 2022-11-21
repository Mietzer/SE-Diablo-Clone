using olbaid_mortel_7720.MVVM.Model;
using System;

namespace olbaid_mortel_7720.GameplayClasses
{
  public class EnemyRanged : Enemy
  {

    public EnemyRanged(int x, int y, int heigth, int width, int steplength, int health, int damage) : base(health, damage, x, y, heigth, width, steplength)
    {

    }

    public override void Attack(Player player)
    {
      player.TakeDamage(this.Damage);
    }

    public override void StopMovement(object? sender, EventArgs e)
    {
      throw new NotImplementedException();
    }
  }
}
