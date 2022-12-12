
namespace olbaid_mortel_7720.MVVM.Model.Object
{
    /// <summary>
    /// Class Medicine for all Object that Heal the Player
    /// </summary>
    public class Medicine : CollectableObject
    {
        private int lifepoints;

        public Medicine(int lifetime, int lifepoints) : base("Medicine", true, lifetime)
        {
          this.lifepoints = lifepoints;
        }

        public override void WhenCollected(Player player)
        {
          player.Heal(lifepoints);
        }
    }
}
