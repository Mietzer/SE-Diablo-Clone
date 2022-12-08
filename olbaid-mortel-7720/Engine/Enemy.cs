﻿using olbaid_mortel_7720.MVVM.Model;
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
    #endregion Properties

    #region Methods
    protected Enemy(int x, int y, int height, int width, int steplength, int health, int damage, MapViewModel mapModel) : base(x, y, height, width, steplength, mapModel)
    {
      this.health = health;
      this.damage = damage;
      pathfinder = Pathfinder.Initialize(this.Barriers);
    }

    public void TakeDamage(int damage)
    {
      health = health - damage;
    }

    protected List<Direction> DecideDirectionPath(Player player, int x, int y, int nearest = 0, int farthest = 0)
    {
      const int tolerance = 5;
      List<Direction> directions = new();
      
      Vector2 targetVector = pathfinder.FindPath(new Point(x, y), new Point(player.XCoord, player.YCoord));
      
      int xDiff = Math.Abs(player.XCoord - x);
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
      
      int yDiff = Math.Abs(player.YCoord - y);
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
      
      return directions;
    }
    
    public abstract void Attack(Player player);
    #endregion Methods
  }
}
