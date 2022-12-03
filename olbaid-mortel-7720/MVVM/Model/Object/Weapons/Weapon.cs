using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using System.Windows.Controls;

namespace olbaid_mortel_7720.Object
{
  /// <summary>
  ///  Class for all types of weapons for players as well as for opponents
  /// </summary>
  public abstract class Weapon
  {
    protected string shotname = "ShotPlayer";
    protected Player player;
    protected Canvas playercanvas;
    protected int reloadtime;
    protected int damage;
    Bullet Bullet;
    protected ImageCategory category;
    protected string imageString;
    
    private int damage;

    public int Damage { get; protected set; }

    //optional Munitons Munition 

    public Weapon(Player player, Canvas playercanvas)
    {
      this.player = player;
      this.playercanvas = playercanvas;
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
