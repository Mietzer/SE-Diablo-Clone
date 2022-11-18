using olbaid_mortel_7720.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace olbaid_mortel_7720.GameplayClasses
{
   public class EnemyRanged : Enemy
  {

    public EnemyRanged(int x, int y, int xMin, int yMin, int xMax, int yMax, int heigth, int width, int steplength, int health, int damage) : base(health, damage, x, y, xMin, yMin, xMax, yMax, heigth, width, steplength)
    {

    }

    public override void Attack(Player player)
    {
      player.TakeDamage(this.Damage);
    }

    public override void Stop(object sender, EventArgs e)
    {
      throw new NotImplementedException();
    }
  }
}
