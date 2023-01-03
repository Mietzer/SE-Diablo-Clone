using olbaid_mortel_7720.Helper;

namespace olbaid_mortel_7720.MVVM.Model.Object
{
  public class WeaponUpgrade : CollectableObject
  {
    #region Properties
    private int damage;
    #endregion Properties

    #region Constructor
    public WeaponUpgrade(int lifetime, int damage, int x, int y) : base("WeaponUpgrade", true, lifetime, x, y)
    {
      this.damage = damage;
      category = ImageCategory.ITEMS;
      imageString = "upgrade.png";
    }
    #endregion Constructor

    #region Methods
    public override void WhenCollected(Player player)
    {
      player.UpgradeWeapon(damage);
    }
    #endregion Methods
  }
}
