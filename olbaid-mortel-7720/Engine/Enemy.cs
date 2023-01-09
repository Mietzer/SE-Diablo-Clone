using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Model.Object;
using olbaid_mortel_7720.MVVM.Viewmodel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Windows;
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
    private static Pathfinder pathfinder;

    public virtual ReadOnlyCollection<CollectableObject> GetPossibleDrops() { return new ReadOnlyCollection<CollectableObject>(new List<CollectableObject>()); }

    public string DeathPoint { get; set; }
    public int Health
    {
      get { return health; }
      set
      {
        if (value == health) return;
        health = value;

        //Delete Picture if Enemy dies
        if (health <= 0)
          base.Dispose();

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
    #endregion Properties

    #region Constructor
    protected Enemy(int x, int y, int height, int width, int steplength, int health, int damage, MapViewModel mapModel) : base(x, y, height, width, steplength, mapModel)
    {
      this.health = health;
      this.damage = damage;

      if (pathfinder == null)
        pathfinder = Pathfinder.Initialize(this.Barriers);
    }

    ~Enemy() { }
    #endregion Constructor

    #region Methods

    public void TakeDamage(int points)
    {
      if (Health > 0)
        Health -= points;
      if (Health <= 0)
      {
        EnemyDeathPoint edp = new EnemyDeathPoint();
        edp.X = this.XCoord;
        edp.Y = this.YCoord;
        edp.Drops = GetPossibleDrops();
        OnDeath(edp);
      }
    }

    protected List<Direction> DecideDirectionPath(Player player, double x, double y, int nearest = 0, int farthest = 0)
    {
      const int tolerance = 5;
      Direction lastDirection = Direction;
      List<Direction> directions = new();

      Vector2 targetVector = pathfinder.FindPath(new Point(x, y), new Point(player.Hitbox.X + player.Hitbox.Width / 2, player.Hitbox.Y + player.Hitbox.Height), lastDirection);

      int xDiff = (int)Math.Abs(player.Hitbox.X - x);
      if (xDiff > farthest)
      {
        if (targetVector.X > tolerance + nearest) directions.Add(Direction.Right);
        else if (targetVector.X < -tolerance - nearest) directions.Add(Direction.Left);
      }
      else if (xDiff < nearest)
      {
        if (targetVector.X > tolerance) directions.Add(Direction.Left);
        else if (targetVector.X < -tolerance) directions.Add(Direction.Right);
      }

      int yDiff = (int)Math.Abs(player.Hitbox.Y - y);
      if (yDiff > farthest)
      {
        if (targetVector.Y > tolerance + nearest) directions.Add(Direction.Down);
        else if (targetVector.Y < -tolerance - nearest) directions.Add(Direction.Up);
      }
      else if (yDiff < nearest)
      {
        if (targetVector.Y > tolerance) directions.Add(Direction.Up);
        else if (targetVector.Y < -tolerance) directions.Add(Direction.Down);
      }

      if (directions.Count > 0)
      {
        switch (lastDirection)
        {
          case Direction.Down:
            directions.RemoveAll(dir => dir == Direction.Up);
            break;
          case Direction.Up:
            directions.RemoveAll(dir => dir == Direction.Down);
            break;
          case Direction.Left:
            directions.RemoveAll(dir => dir == Direction.Right);
            break;
          case Direction.Right:
            directions.RemoveAll(dir => dir == Direction.Left);
            break;
        }
      }

      return directions;
    }

    public abstract void Attack(Player player);
    #endregion Methods

    #region Events
    public event EventHandler EventDeath;
    protected virtual void OnDeath(EnemyDeathPoint e)
    {
      EventHandler<EnemyDeathPoint> handler = IsDeath;
      if (handler != null)
      {
        handler(this, e);
      }
    }

    public event EventHandler<EnemyDeathPoint> IsDeath;
    #endregion Events
  }
}
