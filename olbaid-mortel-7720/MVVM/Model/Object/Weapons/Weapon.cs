using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model.Object.Weapons;

namespace olbaid_mortel_7720.Object
{
  /// <summary>
  ///  Class for all types of weapons for players as well as for opponents
  /// </summary>
  public abstract class Weapon
  {
    #region Properties
    protected string shotname = "ShotPlayer";
    protected int reloadtime = 1;
    protected ImageCategory category;
    protected string imageString;
    public Munition Munition;
    protected int munitioncount;
    public int Damage { get; protected set; }

    //optional Munitons Munition 
    #endregion Properties

    #region Constructor
    public Weapon() { }
    #endregion Constructor

    #region Methods
    public ImageCategory GetCategory()
    {
      return category;
    }
    public string GetImageString()
    {
      return imageString;
    }
    public void UpgradeDamage(int damage)
    {
      Damage += damage;
    }


    #endregion Methods

  }
}
