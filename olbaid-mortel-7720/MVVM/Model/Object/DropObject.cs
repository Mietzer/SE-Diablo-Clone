using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace olbaid_mortel_7720.MVVM.Model.Object
{
  /// <summary>
  ///  Class DropObject drops Items on Destruction or on Interaction
  /// </summary>
  public class DropObject : GameObject
  {
    #region Properties
    private int x;
    private int y;

    //private Rect hitbox;
    public Rect Hitbox { get; set; }

    private List<CollectableObject> items = new List<CollectableObject>();

    public ReadOnlyCollection<CollectableObject> Items { get => items.AsReadOnly(); }
    #endregion Properties

    #region Constructor
    public void DropItems(Canvas canvas)
    {
      foreach (CollectableObject item in Items)
      {
        item.Spawn(canvas);
      }
    }
    #endregion Constructor

    #region Methods
    public DropObject(int x, int y, string name, bool visible) : base(name, visible, true)
    {
      this.x = x;
      this.y = y;
      Hitbox = new Rect(x, y, 40, 40);
    }

    public void AddAsLoot(CollectableObject item)
    {
      items.Add(item);
    }
    #endregion Methods

  }
}
