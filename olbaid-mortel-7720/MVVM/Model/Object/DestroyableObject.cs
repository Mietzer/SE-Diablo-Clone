using olbaid_mortel_7720.Engine;

namespace olbaid_mortel_7720.MVVM.Model.Object
{
  /// <summary>
  ///  Class for objects that can be destroyed
  /// </summary>
  public abstract class DestroyableObject : GameObject, IDestroyable
  {
    public bool CanBeDestroyed()
    {
      return true;
    }
    
    public void OnDestroy()
    {
      this.Remove();
    }
    
    public DestroyableObject(string name, bool visible) : base(name, visible, true)
    {
      this.Add();
    }
    
    private void Add()
    {
      // TODO: Add to canvas
    }
    
    private void Remove()
    {
      // TODO: remove from canvas
    }
  }
}
