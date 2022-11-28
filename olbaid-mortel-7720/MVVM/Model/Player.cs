using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Models;
using olbaid_mortel_7720.Object;
using System;
using System.Collections.Specialized;
using System.Windows;
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

    private PlayerEffect effect;
    public PlayerEffect Effect
    {
      get => effect;
      set
      {
        if (value == effect) return;
        effect = value;
        OnPropertyChanged(nameof(Effect));
      }
    }

    private BitmapImage weaponOverlay;
    public BitmapImage WeaponOverlay
    {
      get { return weaponOverlay; }
      private set
      {
        if (value == weaponOverlay) return;
        weaponOverlay = value;
        OnPropertyChanged(nameof(WeaponOverlay));
      }
    }

    private Weapon currentWeapon;

    public Weapon CurrentWeapon
    {
      get { return currentWeapon; }
      set
      {
        if (value == currentWeapon) return;
        currentWeapon = value;
        OnPropertyChanged(nameof(CurrentWeapon));
      }
    }

    public bool IsShooting { get; set; }
    public int OverallShots { get; private set; } = 0;
    public int ShotHits { get; private set; } = 0;
    #endregion Properties

    public Player(int x, int y, int height, int width, int health, int stepLength) : base(x, y, height, width, stepLength)
    {
      HealthPoints = health;
      Effect = PlayerEffect.None;
      Hitbox = new Rect(x, y + 25, width, height - 25);
      WeaponOverlay = null;
      CurrentWeapon = new Handgun();
      Bullets.CollectionChanged += Bullets_CollectionChanged;
    }

    #region Methods

    public override void RefreshHitbox()
    {
      this.Hitbox = new Rect(XCoord, YCoord + 25, Width, Height - 25);
    }

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
        Image = ImageImporter.Import(ImageCategory.PLAYER, "player-walking-" + directionString + ".gif");
        WeaponOverlay = ImageImporter.Import(CurrentWeapon.GetCategory(), "walking-" + directionString + ".gif");
      }
    }

    /// <summary>
    /// Stopping animation for player
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public override void StopMovement(object? sender, EventArgs e)
    {
      bool oldIsMoving = IsMoving;
      IsMoving = false;
      if (oldIsMoving != IsMoving || (sender != null && sender.ToString().Equals("Initial")))
      {
        string directionString = this.Direction.ToString().ToLower();
        Image = ImageImporter.Import(ImageCategory.PLAYER, "player-standing-" + directionString + ".gif");
        WeaponOverlay = ImageImporter.Import(CurrentWeapon.GetCategory(), "standing-" + directionString + ".gif");
      }
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
        Image = ImageImporter.Import(ImageCategory.PLAYER, "player-walking-" + directionString + ".gif");
        WeaponOverlay = ImageImporter.Import(CurrentWeapon.GetCategory(), "walking-" + directionString + ".gif");
      }
      else
      {
        Image = ImageImporter.Import(ImageCategory.PLAYER, "player-standing-" + directionString + ".gif");
        WeaponOverlay = ImageImporter.Import(CurrentWeapon.GetCategory(), "standing-" + directionString + ".gif");
      }

      Direction = newDirection;
    }

    public void TakeDamage(int damage)
    {
      HealthPoints -= damage;
    }

    #endregion Methods

    #region Events
    /// <summary>
    /// Counts the hits and Shots of the Player
    /// </summary>
    private void Bullets_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.NewItems != null)
        foreach (var item in e.NewItems)
          OverallShots++;

      if (e.OldItems != null)
        foreach (var item in e.OldItems)
          if ((item as Bullet).HasHit)
            ShotHits++;

    }
    #endregion Events
  }
}
