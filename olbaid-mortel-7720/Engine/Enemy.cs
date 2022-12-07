using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Model.Object;
using olbaid_mortel_7720.MVVM.Viewmodel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;


namespace olbaid_mortel_7720.Engine
{
  public abstract class Enemy : Entity
  {
    #region Properties
    protected const int MAX_SAME_DIRECTION = 13;
    protected int sameDirectionCounter = 0;

    private int health;
    private int damage;
    
    public abstract ReadOnlyCollection<CollectableObject> GetPossibleDrops();

    public int Health
    {
      get { return health; }
      set
      {
        if (value == health) return;
        //Delete Picture if Enemy dies
        if (health <= 0)
          Model = null;
        health = value;
        OnPropertyChanged(nameof(Health));
      }
    }

    private Image model;

    public Image Model
    {
      get { return model; }
      set
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
        if (value == damage) return;
        damage = value;
        OnPropertyChanged(nameof(Damage));
      }
    }

    public bool IsAttacking { get; protected set; }

    public abstract void Attack(Player player);
    #endregion Properties

    #region Methods
    protected Enemy(int x, int y, int height, int width, int steplength, int health, int damage, MapViewModel mapModel) : base(x, y, height, width, steplength, mapModel)
    {
      this.health = health;
      this.damage = damage;
    }

    public void TakeDamage(int damage)
    {
      health = health - damage;
    }

    protected List<Direction> DecideDirectionPath(Player player, int x, int y, int nearest = 0, int farthest = 0)
    {
      const int tolerance = 5;
      List<Direction> directions = new();
      
      int xDistance = player.XCoord - x;
      int yDistance = player.YCoord - y;
      
      if (xDistance > tolerance + nearest)
      {
        directions.Add(Direction.Right);
      }
      else if (xDistance < -tolerance - nearest)
      {
        directions.Add(Direction.Left);
      }
      if (yDistance > tolerance + nearest)
      {
        directions.Add(Direction.Down);
      }
      else if (yDistance < -tolerance - nearest)
      {
        directions.Add(Direction.Up);
      }

      return directions;
    }
    #endregion Methods
  }
}
