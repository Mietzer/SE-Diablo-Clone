using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Model.Object;
using System.Collections.Generic;
using System.Windows.Controls;

namespace olbaid_mortel_7720.MVVM.Viewmodel
{
  public class DropObjectViewModel : NotifyObject
  {
    #region Properties
    public List<GameObject> DropObjects;
    private Player MyPlayer { get; set; }

    private Canvas DropObjectCanvas;

    private string Tag;

    #endregion Properties

    #region Constructor
    public DropObjectViewModel(List<GameObject> dropObjects, Canvas dropObjectCanvas, Player player)
    {
      DropObjects = dropObjects;
      this.DropObjectCanvas = dropObjectCanvas;
      this.DropObjectCanvas.Name = "DropObjectCanvas";
      this.MyPlayer = player;
      this.Tag = "Drop";
      SpawnItems();
    }

    ~DropObjectViewModel() { }
    #endregion Constructor

    #region Methods 

    private void SpawnItems()
    {
      foreach (var dropObject in DropObjects)
      {
        if (dropObject is DropObject)
        {
          (dropObject as DropObject)?.DropItems(DropObjectCanvas);
        }
        else if (dropObject is CollectableObject)
        {
          (dropObject as CollectableObject)?.Spawn(DropObjectCanvas);
        }
      }
    }

    #endregion Methods
  }
}
