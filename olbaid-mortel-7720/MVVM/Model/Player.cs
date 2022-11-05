using olbaid_mortel_7720.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace olbaid_mortel_7720.MVVM.Model
{
  public class Player : Entity
  {
    #region Properties
    private int healthPoints;
    public int HealthPoints
    {
      get { return healthPoints; }
      private set
      {
        healthPoints = value;
        OnPropertyChanged(nameof(HealthPoints));
      }
    }
    #endregion Properties

    public Player(int x, int y, int xMin, int yMin, int xMax, int yMax, int height, int width, int health, int stepLength) : base(x, y, xMin, yMin, xMax, yMax, height, width, stepLength)
    {
      HealthPoints = health;
    }

    #region Methods
    public void Move(object sender, KeyEventArgs e)
    {
      switch (e.Key)
      {
        case Key.W:
          MoveUp();
          break;
        case Key.A:
          MoveLeft();
          break;
        case Key.S:
          MoveDown();
          break;
        case Key.D:
          MoveRight();
          break;
      }
    }

    public void TakeDamage(int damage)
    {
      HealthPoints -= damage;
    }
    #endregion Methods

  }
}
