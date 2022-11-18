using olbaid_mortel_7720.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace olbaid_mortel_7720.GameplayClasses
{
  public class EnemyMelee : Enemy
  {

    override public void Attack(Player player)
    {
        player.TakeDamage(this.Damage);
    }

    public void MoveToPlayer(Player player)
    {
      if(player.XCoord < this.XCoord)
      {
        MoveLeft();
      }
      if(player.XCoord > this.XCoord)
      {
        MoveRight();
      }
      if(player.YCoord < this.YCoord)
      {
        MoveUp();
      }
      if(player.XCoord > this.YCoord)
      {
        MoveDown();
      }
    }

    public override void Stop(object sender, EventArgs e)
    {
      throw new NotImplementedException();
    }

    public EnemyMelee(int x, int y, int xMin, int yMin, int xMax, int yMax, int heigth, int width, int steplength, int health, int damage) : base(x, y, xMin, yMin, xMax, yMax, heigth, width, steplength, health, damage)
    {
 
    }
  }
}
