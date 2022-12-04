using olbaid_mortel_7720.MVVM.Model.Object;
using System.Windows;

namespace olbaid_mortel_7720.Engine
{
  /// <summary>
  /// Container for a barrier
  /// </summary>
  public class Barrier
  {
    public enum BarrierType
    {
      None,
      Wall,
      Furniture,
      Floor
    }
    
    public Rect Hitbox { get; set; }
    public BarrierType Type { get; set; }
    
    public Barrier(Rect hitbox, BarrierType type)
    {
      Hitbox = hitbox;
      Type = type;
    }
  }
}