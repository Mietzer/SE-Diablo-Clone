using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.MVVM.Model;
using System.Windows.Shapes;
using System.Windows.Media;


namespace olbaid_mortel_7720.GameplayClasses
{
  public abstract class Enemy : Entity
  {

    private int health;
    private int damage;

    public int Health
    {
      get { return health; }
      set
      {
        health = value;
        OnPropertyChanged(nameof(Health));
      }
    }

    private Rectangle model;

    public Rectangle Model
    {
      get { return model; }
      private set
      {
        model = value;
        OnPropertyChanged(nameof(Model));
      }
    }

    public int Damage
    {
      get { return damage; }
      set
      {
        damage = value;
        OnPropertyChanged(nameof(Damage));
      }
    }



    public abstract void Attack(Player player);

    public Enemy(int x, int y, int height, int width, int steplength, int health, int damage) : base(x, y, height, width, steplength)
    {
      this.health = health;
      this.damage = damage;
      this.model = new Rectangle() { Tag = "Enemy", Height = 20, Width = 20, Fill = Brushes.Blue };
    }

    public void TakeDamage(int damage)
    {
      this.health = health - damage;
    }


  }
}
