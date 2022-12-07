using olbaid_mortel_7720.MVVM.Model.Object;
using System.Windows;

namespace olbaid_mortel_7720.Engine
{
  /// <summary>
  /// Container for a barrier
  /// </summary>
  public class Barrier
  {
    #region Enumerations
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
    
    /// <summary>
    /// Special tag for the barrier
    /// </summary>
    public enum BarrierTag
    {
      None,
      Destroyable
    }
    #endregion Enumerations

    #region Properties
    public Rect Hitbox { get; set; }
    public BarrierType Type { get; set; }
    public BarrierTag Tag { get; set; }
    #endregion Properties

    #region Methods
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="hitbox">The hitbox</param>
    /// <param name="type">Type of the barrier</param>
    public Barrier(Rect hitbox, BarrierType type)
    {
      Hitbox = hitbox;
      Type = type;
      Tag = BarrierTag.None;
    }
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="hitbox">The hitbox</param>
    /// <param name="type">Type of the barrier</param>
    /// <param name="tag">Special tag</param>
    public Barrier(Rect hitbox, BarrierType type, BarrierTag tag)
    {
      Hitbox = hitbox;
      Type = type;
      Tag = tag;
    }
    #endregion Methods
  }
}