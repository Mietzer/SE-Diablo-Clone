using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Model.Object;
using System;
using System.Collections.Generic;
using System.Windows.Controls;


namespace olbaid_mortel_7720.MVVM.Viewmodel
{
  public class DropObjectViewModel : NotifyObject
  {
    #region Properties
    public List<GameObject> DropObjects;
    private List<GameObject> _dropObjects;
    private Player MyPlayer { get; set; }

    private Canvas DropObjectCanvas;

    private string Tag;

    #endregion Properties

    #region Constructor
    public DropObjectViewModel(List<GameObject> dropObjects, Canvas dropObjectCanvas, Player player)
    {
      DropObjects = dropObjects;
      this._dropObjects = new List<GameObject>();
      this.DropObjectCanvas = dropObjectCanvas;
      this.DropObjectCanvas.Name = "DropObjectCanvas";
      this.MyPlayer = player;
      this.Tag = "Drop";
      InitTimer();
    }

    ~DropObjectViewModel() { }
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

      DropObjects?.Clear();
      DropObjects = null;

      MyPlayer = null;

      GC.Collect();
    }
    
    /// <summary>
    /// Method for Spwaning Items 
    /// </summary>
    private void SpawnItems(EventArgs e)
    {
      foreach (var dropObject in DropObjects)
      {
        if (!_dropObjects.Contains(dropObject))
        {
          if (dropObject is DropObject)
          {
            (dropObject as DropObject)?.DropItems(DropObjectCanvas);
          }
          else if (dropObject is CollectableObject)
          {
            (dropObject as CollectableObject)?.Spawn(DropObjectCanvas);
          }
          _dropObjects.Add(dropObject);
        }
      }
    }
    #endregion Methods
  }
}
