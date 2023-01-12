
using olbaid_mortel_7720.Helper;

namespace olbaid_mortel_7720.MVVM.Model.Object
{
  /// <summary>
  /// Class Key as Levelkey
  /// </summary>
  public class LevelKey : CollectableObject
  {
    #region Constructor
    public LevelKey(int x, int y) : base("Key", true, int.MaxValue, x, y)
    {
      category = ImageCategory.ITEMS;
      imageString = "key.png";
    }
    #endregion Constructor

    #region Methods
    public override void WhenCollected(Player player)
    {
      player.GetKey();
    }
    #endregion Methods

  }
}
