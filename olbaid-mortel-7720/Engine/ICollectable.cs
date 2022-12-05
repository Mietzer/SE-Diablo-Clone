using olbaid_mortel_7720.MVVM.Model;

namespace olbaid_mortel_7720.Engine
{
  /// <summary>
  /// Implementing a collectable object
  /// </summary>
  public interface ICollectable
  {
    /// <summary>
    /// Callback when the object is collected by the player
    /// </summary>
    /// <param name="player">The player model</param>
    void OnCollect(Player player);
  }
}