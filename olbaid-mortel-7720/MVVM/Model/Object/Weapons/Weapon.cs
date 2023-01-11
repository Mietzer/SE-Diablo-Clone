using olbaid_mortel_7720.Helper;

namespace olbaid_mortel_7720.MVVM.Model.Object.Weapons
{
  /// <summary>
  ///  Class for all types of weapons for players as well as for opponents
  /// </summary>
  public abstract class Weapon
  {
    #region Properties
    protected int reloadtime = 1;
    protected ImageCategory category;
    protected string imageString;
    public Munition Munition;
    public int Damage { get; protected set; }
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
