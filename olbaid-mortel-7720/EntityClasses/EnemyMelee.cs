using olbaid_mortel_7720.Engine;
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
      Direction lastDirection = this.Direction;
      List<Direction> directions = new List<Direction>();
      if(player.XCoord + player.Width / 2 < this.XCoord + this.Width / 2)
      {
        directions.Add(Direction.Left);
      }
      if(player.XCoord + player.Width / 2 > this.XCoord + this.Width / 2)
      {
        directions.Add(Direction.Right);
      }
      if(player.YCoord + player.Height / 2 < this.YCoord + this.Height / 2)
      {
        directions.Add(Direction.Up);
      }
      if(player.YCoord + player.Height / 2 > this.YCoord + this.Height / 2)
      {
        directions.Add(Direction.Down);
      }

      Direction item;
      if (directions.Contains(lastDirection))
      {
        item = lastDirection;
      }
      else
      {
        Random random = new Random();
        int index = random.Next(0, directions.Count);
        if (directions.Count == 0) return;
        item = directions[index];
      }
      switch (item)
      {
        case Direction.Up:
          this.MoveUp();
          break;
        case Direction.Down:
          this.MoveDown();
          break;
        case Direction.Left:
          this.MoveLeft();
          break;
        case Direction.Right:
          this.MoveRight();
          break;
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
