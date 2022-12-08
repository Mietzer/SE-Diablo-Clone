using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model.Object.Weapons;

namespace olbaid_mortel_7720.Object
{
  public class Handgun : Weapon
  {
    #region Properties

    #endregion Properties
    public Handgun(Munition munition) : base(munition)
    {
      Damage = 20;
      reloadtime = 2;
      category = ImageCategory.WEAPONS_PLAYER_HANDGUN;
      imageString = "handgun.png";
    }
    #region Methods

    #endregion Methods
  }
}
