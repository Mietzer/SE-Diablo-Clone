using System;
using System.Collections.Specialized;


namespace olbaid_mortel_7720.MVVM.Model
{
    public delegate void DeathEvent();

    public class Player : Entity
    {
        #region Properties

        private int maxhealthPoints;
        private int healthPoints;
        private bool armor;
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

        public Player(int x, int y, int height, int width, MapViewModel mapModel) : base(x, y, height, width, 5, mapModel)
        {
            HealthPoints = 100;
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
                    this.secondaryweapon = CurrentWeapon;
                    CurrentWeapon = this.primaryweapon;
                    break;
                case Key.D2:
                    this.primaryweapon = CurrentWeapon;
                    CurrentWeapon = this.secondaryweapon;

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
        }
      if (!armor)
      {
        HealthPoints -= damage;

        if (HealthPoints <= 0)
        {
          Death();
    }
}
      else
{
    armor = false;
    Effect = PlayerEffect.None;
}
    }

    /// <summary>
    /// Player is being healed
    /// </summary>
    /// <param name="amount">How much</param>
    public void Heal(int amount)
{
    if (maxhealthPoints != HealthPoints && maxhealthPoints > HealthPoints)
    {
        Effect = PlayerEffect.Healing;
        GameTimer.ExecuteWithInterval(amount, delegate (EventArgs args) { }, progress => { HealthPoints += 1; }, true);
    }
}
/// <summary>
/// Player is being Poisoned
/// </summary>
/// <param name="amount">How much</param>
public void Poisoned(int amount)
{
    Effect = PlayerEffect.Poisoned;
    GameTimer.ExecuteWithInterval(amount, delegate (EventArgs args) { }, progress => { HealthPoints -= 1; }, true);
}
/// <summary>
/// Player is gets Armor
/// </summary>
/// <param name="amount">How much</param>
public void Armor()
{
    Effect = PlayerEffect.Protected;
    armor = true;
}
/// <summary>
/// Player Weapon gets Bonus Damage 
/// </summary>
/// <param name="amount">How much</param>
public void WeaponUpgrade(int damage)
{
    currentWeapon.DamageUpgrade(damage);
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

public event DeathEvent PlayerDied;

protected virtual void Die()
{
    //TODO: Rest Clean Up Impelemtieren von Bluescren View 
    Bullets.CollectionChanged -= Bullets_CollectionChanged;
    HealthPoints = 10;
    PlayerDied?.Invoke();
}

public event EventHandler WeaponSwap;
    #endregion Events
  }
}
