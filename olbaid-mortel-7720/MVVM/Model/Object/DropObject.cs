
namespace olbaid_mortel_7720.MVVM.Model.Object
{
  /// <summary>
  ///  Class DropObject Dopts Items on Destruction
  /// </summary>
  public class DropObject : DestroyableObject
  {
    public CollectableObject[] Items { get; private set; }

    public void DropItems()
    {
      foreach (CollectableObject item in Items)
      {
        item.Spawn();
      }
    }
    
    public DropObject(string name, bool visible) : base(name, visible)
    {
      
    }
  }
}
