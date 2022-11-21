using olbaid_mortel_7720.Object;

namespace olbaid_mortel_7720.MVVM.Model.Object
{
  /// <summary>
  ///  Basic Class for all types of Object 
  /// </summary>
  public class MapObject : Object
  {
    #region Properties
    public Graphics Graphic;

    #endregion Properties

    public MapObject(string name, Graphics graphic, bool visible, bool penetrable) : base(name, visible, penetrable)
    {
      Graphic = graphic;
    }

    #region Methods

    #endregion Methods

  }
}
