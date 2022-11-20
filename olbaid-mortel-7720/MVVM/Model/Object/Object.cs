using olbaid_mortel_7720.Object;

namespace olbaid_mortel_7720.MVVM.Model.Object
{
  /// <summary>
  ///  Basic Class for all types of Object 
  /// </summary>
  public class MapObject
  {
    public string Name;
    public Graphics Graphic;

    bool Visible;

    //hitbox 
    public bool Penetrable { get; set; }



    public MapObject(string name, Graphics graphic, bool visible, bool penetrable)
    {
      Name = name;
      Graphic = graphic;
      Visible = visible;
      Penetrable = penetrable;
    }
  }
}
