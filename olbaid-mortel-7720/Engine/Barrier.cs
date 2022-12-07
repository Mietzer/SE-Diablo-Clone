using olbaid_mortel_7720.MVVM.Model.Object;
using System.Windows;

namespace olbaid_mortel_7720.Engine
{
  /// <summary>
  /// Container for a barrier
  /// </summary>
  public class Barrier
  {
    /// <summary>
    /// Type of the barrier
    /// </summary>
    public enum BarrierType
    {
      None,
      Wall,
      Furniture,
      Floor
    }
    
    public Rect Hitbox { get; set; }
    public BarrierType Type { get; set; }
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="hitbox">The hitbox</param>
    /// <param name="type">Type of the barrier</param>
    public Barrier(Rect hitbox, BarrierType type)
    {
      Hitbox = hitbox;
      Type = type;
    }
  }
}