﻿using olbaid_mortel_7720.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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
    
    private BitmapImage weaponOverlay;
    public BitmapImage WeaponOverlay
    {
      get { return weaponOverlay; }
      private set
      {
        weaponOverlay = value;
        OnPropertyChanged(nameof(WeaponOverlay));
      }
    }
    
    public bool IsShooting { get; set; }
    #endregion Properties

    public Player(int x, int y, int xMin, int yMin, int xMax, int yMax, int height, int width, int health, int stepLength) : base(x, y, xMin, yMin, xMax, yMax, height, width, stepLength)
    {
      HealthPoints = health;
      WeaponOverlay = null;
    }

    #region Methods
    /// <summary>
    /// Moving and animating the player
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="key"></param>
    public void Move(object sender, Key key)
    {
      Direction oldDirection = Direction;
      bool oldIsMoving = IsMoving;
      IsMoving = true;
      switch (key)
      {
        case Key.W:
          MoveUp();
          break;
        case Key.S:
          MoveDown();
          break;
        case Key.A:
          MoveLeft();
          break;
        case Key.D:
          MoveRight();
          break;
      }
      if ((oldDirection != Direction || oldIsMoving != IsMoving) && !IsShooting)
      {
        string directionString = this.Direction.ToString().ToLower();
        Image = new BitmapImage(new Uri("pack://application:,,,/Images/Entities/Player/player-walking-" + directionString + ".gif"));
        WeaponOverlay = new BitmapImage(new Uri("pack://application:,,,/Images/Weapons/Player/Handgun/walking-" + directionString + ".gif"));
      }
    }

    /// <summary>
    /// Stopping animation for player
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public override void Stop(object sender, EventArgs e)
    {
      IsMoving = false;
      string directionString = this.Direction.ToString().ToLower();
      Image = new BitmapImage(new Uri("pack://application:,,,/Images/Entities/Player/player-standing-" + directionString + ".gif"));
      WeaponOverlay = new BitmapImage(new Uri("pack://application:,,,/Images/Weapons/Player/Handgun/standing-" + directionString + ".gif"));
    }
    
    /// <summary>
    /// Set the correct view for the player when he is shooting
    /// </summary>
    /// <param name="newDirection"></param>
    public void UpdateViewDirection(Direction newDirection)
    {
      if (Direction == newDirection) return;
      
      string directionString = newDirection.ToString().ToLower();
      if (IsMoving)
      {
        Image = new BitmapImage(new Uri("pack://application:,,,/Images/Entities/Player/player-walking-" + directionString + ".gif"));
        WeaponOverlay = new BitmapImage(new Uri("pack://application:,,,/Images/Weapons/Player/Handgun/walking-" + directionString + ".gif"));
      }
      else
      {
        Image = new BitmapImage(new Uri("pack://application:,,,/Images/Entities/Player/player-standing-" + directionString + ".gif"));
        WeaponOverlay = new BitmapImage(new Uri("pack://application:,,,/Images/Weapons/Player/Handgun/standing-" + directionString + ".gif"));
      }
      
      Direction = newDirection;
    }

    public void TakeDamage(int damage)
    {
      HealthPoints -= damage;
    }
    #endregion Methods

  }
}
