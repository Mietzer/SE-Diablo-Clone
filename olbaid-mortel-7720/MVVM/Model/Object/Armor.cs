
using olbaid_mortel_7720.Helper;

namespace olbaid_mortel_7720.MVVM.Model.Object
{
  /// <summary>
  /// Class Armor for all Object that give the Player Armor
  /// </summary>
  public class Armor : CollectableObject
  {
    public Armor(int lifetime) : base("Medicine", true, lifetime)
    {
      category = ImageCategory.ITEMS;
      imageString = "helmet.png";
    }

    public override void WhenCollected(Player player)
    {
      player.Armor();
    }
  }
}
