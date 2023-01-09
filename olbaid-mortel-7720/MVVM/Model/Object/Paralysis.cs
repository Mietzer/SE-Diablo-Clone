using olbaid_mortel_7720.Helper;

namespace olbaid_mortel_7720.MVVM.Model.Object
{
  /// <summary>
  /// Class paralysis for all objects that can be Collectet and slow down / paralyse the player
  /// </summary>
  public class Paralysis : CollectableObject
  {
    #region Properties
    private int duration;
    #endregion Properties

    #region Constructor
    public Paralysis(int lifetime, int duration, int x, int y) : base("Paralysis", true, lifetime, x, y)
    {
      this.duration = duration;
      category = ImageCategory.ITEMS;
      imageString = "paralysispotion.png";
    }
    #endregion Constructor

    #region Methods
    public override void WhenCollected(Player player)
    {
      player.Poison(duration);
    }
    #endregion Methods

  }
}
