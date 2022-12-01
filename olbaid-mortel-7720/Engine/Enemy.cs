﻿using olbaid_mortel_7720.MVVM.Model;
using System.Windows.Controls;


namespace olbaid_mortel_7720.Engine
{
  public abstract class Enemy : Entity
  {

    protected const int MAX_SAME_DIRECTION = 13;
    protected int sameDirectionCounter = 0;

    private int health;
    private int damage;

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

    protected Enemy(int x, int y, int height, int width, int steplength, int health, int damage) : base(x, y, height, width, steplength)
    {
      this.health = health;
      this.damage = damage;
    }

    public void TakeDamage(int damage)
    {
      health = health - damage;
    }
  }
}
