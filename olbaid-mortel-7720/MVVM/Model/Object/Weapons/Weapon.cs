using olbaid_mortel_7720.Helper;

namespace olbaid_mortel_7720.Object
{
  /// <summary>
  ///  Class for all types of weapons for players as well as for opponents
  /// </summary>
  public abstract class Weapon
  {
    protected string shotname = "ShotPlayer";
    protected int reloadtime;
    //TODO: Image for Bullets
    protected ImageCategory category;
    protected string imageString;

    public int Damage { get; protected set; }

    //optional Munitons Munition 

    public Weapon()
    {
    }

    public void Shot() { }

    public void Reload() { }

    public ImageCategory GetCategory()
    {
      return category;
    }

    public string GetImageString()
    {
      return imageString;
    }
  }
}
