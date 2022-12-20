
using olbaid_mortel_7720.Helper;

namespace olbaid_mortel_7720.MVVM.Model.Object
{
  public class WeaponUpgrade : CollectableObject
  {
    private int damage;
    public WeaponUpgrade(int lifetime, int damage, int x, int y) : base("Medicine", true, lifetime, x, y)
    {
      this.damage = damage;
      category = ImageCategory.ITEMS;
      imageString = "upgrade.png";
    }

    public override void WhenCollected(Player player)
    {
      player.WeaponUpgrade(damage);
    }
  }
}
