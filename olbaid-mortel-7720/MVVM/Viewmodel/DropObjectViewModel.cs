using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Model.Object;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace olbaid_mortel_7720.MVVM.Viewmodel
{
  public class DropObjectViewModel : NotifyObject
  {
    #region Properties
    public List<DropObject> DropObjects;
    private List<DropObject> _dropObjects;
    private Player MyPlayer { get; set; }

    private Canvas DropObjectCanvas;

    private string Tag;

    #endregion Properties

    #region Constructor
    public DropObjectViewModel(List<DropObject> dropObjects, Canvas dropObjectCanvas, Player player)
    {
      DropObjects = dropObjects;
      this._dropObjects = new List<DropObject>();
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
      timer.Execute(CheckforHit, nameof(this.CheckforHit) + GetHashCode());
      timer.Execute(RemoveDropObject, nameof(this.RemoveDropObject) + GetHashCode());
      timer.Execute(SpawnItems, nameof(this.SpawnItems) + GetHashCode());
    }

    /// <summary>
    /// Method for Cleanups on Closing
    /// </summary>
    public void Dispose()
    {
      GameTimer timer = GameTimer.Instance;
      timer.RemoveByName(nameof(this.CheckforHit) + GetHashCode());
      timer.RemoveByName(nameof(this.RemoveDropObject) + GetHashCode());
      timer.RemoveByName(nameof(this.SpawnItems) + GetHashCode());

      DropObjects?.Clear();
      DropObjects = null;

      MyPlayer = null;

      GC.Collect();
    }
    /// <summary>
    /// Method for Check if Player Hit Object
    /// </summary>
    private void CheckforHit(EventArgs e)
    {
      foreach (DropObject dropObject in DropObjects)
      {
        if (dropObject.Hitbox.IntersectsWith(MyPlayer.Hitbox))
        {
          foreach (var item in dropObject.Items)
          {
            item.OnCollect(MyPlayer);
          }
        }
      }
    }
    /// <summary>
    /// Method for Removing Collector or Time out Items
    /// </summary>
    private void RemoveDropObject(EventArgs e)
    {
      List<DropObject> DeleteDropObjects = new List<DropObject>();
      foreach (DropObject dropObject in DropObjects)
      {
        foreach (var item in dropObject.Items)
        {
          if (!item.CanBeCollected())
          {
            DeleteDropObjects.Add(dropObject);
          }
        }
      }
      if (DeleteDropObjects.Count > 0)
      {
        foreach (DropObject deldropObject in DeleteDropObjects)
        {
          DropObjects.Remove(deldropObject);

          Point point = new Point(deldropObject.Hitbox.X + deldropObject.Hitbox.Width / 2, deldropObject.Hitbox.Y + deldropObject.Hitbox.Height / 2 / 2);
          HitTestResult result = VisualTreeHelper.HitTest(DropObjectCanvas, point);


          if (result != null)
          {
            DropObjectCanvas.Children.Remove(result.VisualHit as UIElement);
          }
        }
      }

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
          dropObject.DropItems(DropObjectCanvas);
          _dropObjects.Add(dropObject);
        }
      }
    }

    #endregion Methods
  }
}
