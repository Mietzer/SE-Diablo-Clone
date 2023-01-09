using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Model.Object;
using System;
using System.Collections.Generic;
using System.Windows.Controls;


namespace olbaid_mortel_7720.MVVM.Viewmodel
{
  public class DroppedObjectsViewModel : NotifyObject
  {
    #region Properties
    public List<GameObject> DroppedObjects;
    private List<GameObject> _droppedObjects;
    private Player MyPlayer { get; set; }

    private Canvas DropObjectCanvas;

    private string Tag;

    #endregion Properties

    #region Constructor
    public DroppedObjectsViewModel(List<GameObject> droppedObjects, Canvas dropObjectCanvas, Player player)
    {
      DroppedObjects = droppedObjects;
      this._droppedObjects = new List<GameObject>();
      this.DropObjectCanvas = dropObjectCanvas;
      this.DropObjectCanvas.Name = "DropObjectCanvas";
      this.MyPlayer = player;
      this.Tag = "Drop";
      InitTimer();
    }

    ~DroppedObjectsViewModel() { }
    #endregion Constructor

    #region Methods 
    public void InitTimer()
    {
      GameTimer timer = GameTimer.Instance;
      timer.Execute(SpawnItems, nameof(this.SpawnItems) + GetHashCode());
    }

    /// <summary>
    /// Method for Cleanups on Closing
    /// </summary>
    public void Dispose()
    {
      GameTimer timer = GameTimer.Instance;
      timer.RemoveByName(nameof(this.SpawnItems) + GetHashCode());

      DroppedObjects?.Clear();
      DroppedObjects = null;

      MyPlayer = null;

      GC.Collect();
    }
    
    /// <summary>
    /// Method for Spawning Items 
    /// </summary>
    private void SpawnItems(EventArgs e)
    {
      foreach (var dropObject in DroppedObjects)
      {
        if (!_droppedObjects.Contains(dropObject))
        {
          if (dropObject is DropObject)
          {
            (dropObject as DropObject)?.DropItems(DropObjectCanvas);
          }
          else if (dropObject is CollectableObject)
          {
            (dropObject as CollectableObject)?.Spawn(DropObjectCanvas);
          }
          _droppedObjects.Add(dropObject);
        }
      }
    }
    #endregion Methods
  }
}
