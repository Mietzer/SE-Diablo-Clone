using olbaid_mortel_7720.Helper;

namespace olbaid_mortel_7720.Object
{
  /// <summary>
  ///  Class for all types of weapons for players as well as for opponents
  /// </summary>
  public abstract class Weapon
  {

    int Shootingspeed;
    int Reloadtime;
    Projectile Bullet;
    protected ImageCategory category;
    protected string imageString;

      //optional Munitons Munition 
    public string PathtoGraphics { get; set; }

    public Weapon() { }

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
