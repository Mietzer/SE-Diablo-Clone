
using olbaid_mortel_7720.Helper;

namespace olbaid_mortel_7720.MVVM.Model.Object
{
  /// <summary>
  /// Class Key as Levelkey
  /// </summary>
  public class LevelKey : CollectableObject
  {

    public LevelKey(int x, int y) : base("Key", true, int.MaxValue, x, y)
    {
      category = ImageCategory.ITEMS;
      imageString = "key.png";
    }

    public override void WhenCollected(Player player)
    {
      player.GetKey();
    }
  }
}
