using olbaid_mortel_7720.MVVM.Model;

namespace olbaid_mortel_7720.Engine
{
  /// <summary>
  /// Implementing a destoyable object
  /// </summary>
  public interface IDestroyable
  {
    /// <summary>
    /// Callback when the object is destroyed
    /// </summary>
    void OnDestroy();
  }
}