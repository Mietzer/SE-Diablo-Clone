using olbaid_mortel_7720.MVVM.Model;
using System;


namespace olbaid_mortel_7720.GameplayClasses
{
  public class EnemyMelee : Enemy
  {

    override public void Attack(Player player)
    {
      if (this.Hitbox.IntersectsWith(player.Hitbox))
      {
        player.TakeDamage(this.Damage);
      }
    }

    public void MoveToPlayer(Player player)
    {
      if (player.XCoord < this.XCoord)
      {
        MoveLeft();
      }
      if (player.XCoord > this.XCoord)
      {
        MoveRight();
      }
      if (player.YCoord < this.YCoord)
      {
        MoveUp();
      }
      if (player.XCoord > this.YCoord)
      {
        MoveDown();
      }
    }

    public override void Stop(object sender, EventArgs e)
    {
      throw new NotImplementedException();
    }

    public EnemyMelee(int x, int y, int heigth, int width, int steplength, int health, int damage) : base(x, y, heigth, width, steplength, health, damage)
    {

    }
  }
}
