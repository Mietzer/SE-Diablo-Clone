using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model.Object.Weapons;
using olbaid_mortel_7720.MVVM.Models;
using olbaid_mortel_7720.MVVM.Viewmodel;
using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;


namespace olbaid_mortel_7720.MVVM.Model
{
  public delegate void EndingEvent();

  public class Player : Entity
  {
    #region Properties
    private int healthPoints;
    private bool oneShotProtection;
    public int HealthPoints
    {
      get { return healthPoints; }
      private set
      {
        healthPoints = value;
        if (healthPoints <= 0)
        {
          Die();
          base.Dispose();
        }
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

    private bool weapononcooldown;
    private Weapon currentWeapon;
    private Weapon primaryweapon;
    private Weapon secondaryweapon;
    public Weapon CurrentWeapon
    {
      get { return currentWeapon; }
      set
      {
        if (value == currentWeapon) return;
        currentWeapon = value;
        WeaponOverlay = ImageImporter.Import(CurrentWeapon.GetCategory(), "walking-" + this.Direction.ToString().ToLower() + ".gif");
        OnPropertyChanged(nameof(CurrentWeapon));
      }
    }

    public bool IsShooting { get; set; }
    public int OverallShots { get; private set; } = 0;
    public int ShotHits { get; private set; } = 0;
    #endregion Properties

    #region Constructor
    public Player(int x, int y, int height, int width, MapViewModel mapModel) : base(x, y, height, width, 5, mapModel)
    {
      HealthPoints = 100;
      weapononcooldown = false;
      oneShotProtection = false;
      Effect = PlayerEffect.None;
      Hitbox = new Rect(x, y + 25, width, height - 25);
      WeaponOverlay = null;
      primaryweapon = new Handgun();
      secondaryweapon = new Rifle();
      CurrentWeapon = primaryweapon;
      Bullets.CollectionChanged += Bullets_CollectionChanged;
    }

    ~Player()
    {
    }
    #endregion Constructor

    #region Methods
    public override void RefreshHitbox()
    {
      this.Hitbox = new Rect(XCoord, YCoord + 25, Width, Height - 25);
    }

    /// <summary>
    /// Moving and animating the player
    /// </summary>
    /// <param name="key"></param>
    public void Move(Key key)
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
    /// Weapon Selection with Key 1 and 2 for Player
    /// </summary>
    /// <param name="key"></param>
    public void WeaponSelection(Key key)
    {
      switch (key)
      {
        case Key.D1:
          if (CurrentWeapon == this.secondaryweapon)
            this.secondaryweapon = CurrentWeapon;
          CurrentWeapon = this.primaryweapon;
          break;
        case Key.D2:
          if (!weapononcooldown)
          {
            weapononcooldown = true;
            GameTimer.ExecuteWithInterval(200, delegate (EventArgs args) { this.weapononcooldown = false; }, true);
            if (CurrentWeapon == this.primaryweapon)
              this.primaryweapon = CurrentWeapon;
            CurrentWeapon = this.secondaryweapon;
            GameTimer.ExecuteWithInterval(50, delegate (EventArgs args) { if (CurrentWeapon == this.secondaryweapon) { this.secondaryweapon = CurrentWeapon; } CurrentWeapon = this.primaryweapon; WeaponSwap?.Invoke(this, EventArgs.Empty); }, true);

          }
          break;
      }
      WeaponSwap?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Stopping animation for player
    /// </summary>
    /// <param name="e"></param>
    public override void StopMovement(EventArgs e)
    {
      bool oldIsMoving = IsMoving;
      IsMoving = false;
      if (oldIsMoving != IsMoving || ((e as InitialEventArgs) != null && (e as InitialEventArgs).IsInitial))
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

    /// <summary>
    /// Player is takes damage
    /// </summary>
    /// <param name="damage">How much</param>
    public void TakeDamage(int damage)
    {
      HealthPoints -= damage;

      if (!oneShotProtection)
      {
        HealthPoints -= damage;

        if (HealthPoints <= 0)
        {
          Die();
        }
      }
      else
      {
        oneShotProtection = false;
        Effect = PlayerEffect.None;
      }
    }

    /// <summary>
    /// Player is being healed
    /// </summary>
    /// <param name="amount">How much</param>
    public void Heal(int amount)
    {
      Effect = PlayerEffect.Healing;
      GameTimer.ExecuteWithInterval(amount, delegate (EventArgs args) { }, progress => { if (HealthPoints < 100) HealthPoints += 1; }, true);
      GameTimer.ExecuteWithInterval(50 + amount, delegate (EventArgs args) { Effect = PlayerEffect.None; }, true);
    }

    /// <summary>
    /// Player is being Poisoned
    /// </summary>
    /// <param name="amount">How much</param>
    public void Poison(int amount)
    {
      Effect = PlayerEffect.Poisoned;
      StepLength -= 2;
      GameTimer.ExecuteWithInterval(amount, delegate (EventArgs args)
      {
        Effect = PlayerEffect.None;
        StepLength += 2;
      }, progress => { if (HealthPoints > 15 && (int)progress % 5 == 0) HealthPoints -= 1; }, true);
    }

    /// <summary>
    /// Player is gets Armor
    /// </summary>
    /// <param name="amount">How much</param>
    public void BeProtected()
    {
      Effect = PlayerEffect.Protected;
      oneShotProtection = true;
    }

    /// <summary>
    /// Player Weapon gets Bonus Damage 
    /// </summary>
    /// <param name="amount">How much</param>
    public void UpgradeWeapon(int damage)
    {
      currentWeapon.UpgradeDamage(damage);
    }

    /// <summary>
    /// Player has got the level key
    /// </summary>
    public void GetKey()
    {
      Won();
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

    /// <summary>
    /// Event for the Player to die -> Game Over
    /// </summary>
    public event EndingEvent PlayerDied;

    /// <summary>
    /// Event for the Player to win -> Next Level
    /// </summary>
    public event EndingEvent PlayerWon;

    protected virtual void Die()
    {
      Bullets.CollectionChanged -= Bullets_CollectionChanged;
      HealthPoints = 10;
      PlayerDied?.Invoke();
    }
    protected virtual void Won()
    {
      Bullets.CollectionChanged -= Bullets_CollectionChanged;
      PlayerWon?.Invoke();
    }

    public event EventHandler WeaponSwap;
    #endregion Events
  }
}
