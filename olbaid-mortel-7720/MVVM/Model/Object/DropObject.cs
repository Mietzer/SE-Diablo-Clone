
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace olbaid_mortel_7720.MVVM.Model.Object
{
  /// <summary>
  ///  Class DropObject Dopts Items on Destruction
  /// </summary>
  public class DropObject : DestroyableObject
  {
    private int x;
    private int y;
    
    private List<CollectableObject> items = new List<CollectableObject>();
    
    public ReadOnlyCollection<CollectableObject> Items { get => items.AsReadOnly(); }

    public void DropItems(Canvas canvas)
    {
      foreach (CollectableObject item in Items)
      {
        item.Spawn(canvas, x, y);
      }
    }
    
    public DropObject(string name, bool visible) : base(name, visible)
    {
      
    }
    
    public void AddAsLoot(CollectableObject item)
    {
      items.Add(item);
    }
  }
}
