using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Models;

namespace olbaid_mortel_7720.MVVM.Model.Object
{
  /// <summary>
  /// Class paralysis for all objects that can be Collectet and slow down / paralyse the player
  /// </summary>
  public class Paralysis : CollectableObject
  {
    private int duration;

    public Paralysis(int lifetime, int duration) : base("Paralysis", true, lifetime)
    {
      this.duration = duration;
      category = ImageCategory.ITEMS;
      imageString = "paralysispotion.png";
    }

    public override void WhenCollected(Player player)
    {
      player.Effect = PlayerEffect.Poisoned;
      // TODO: method with timer in player class
      player.Poisoned(duration);
    }
  }
}
