using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace olbaid_mortel_7720.MVVM.Model.Enemies
{
  public class EnemyRareRanged : EnemyRanged
  {
    public EnemyRareRanged(int x, int y, int heigth, int width, int steplength, int health, int damage) : base(x, y, heigth, width, steplength, health, damage)
    {
      this.Health = base.Health * 10;
    }

  }
}
