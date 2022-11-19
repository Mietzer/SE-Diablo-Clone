using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.MVVM.Model;
using System.Windows.Media;
using System.Windows.Shapes;

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


  }
}
