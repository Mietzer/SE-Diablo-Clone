﻿using olbaid_mortel_7720.Object;
using System.Windows;

namespace olbaid_mortel_7720.MVVM.Model.Object
{
  /// <summary>
  ///  Basic Class for all types of Object 
  /// </summary>
  public class MapObject : GameObject
  {
    #region Properties
    public Graphics Graphic;
    public Rect? CollisionBox { get; set; } = null;
    public bool HasCollision() => CollisionBox != null;
    #endregion Properties

    #region Constructor
    public MapObject(string name, Graphics graphic, bool visible, bool penetrable) : base(name, visible, penetrable)
    {
      Graphic = graphic;
    }
    #endregion Constructor

    #region Methods
    public void AddCollisionBox(double x, double y, double width, double height)
    {
      CollisionBox = new Rect(x, y, width, height);
    }
    #endregion Methods

  }
}
