using olbaid_mortel_7720.Helper;

namespace olbaid_mortel_7720.Object
{
  public class Handgun : Weapon
  {
    #region Properties

    #endregion Properties
    public Handgun()
    {
      Damage = 20;
      reloadtime = 2;
      category = ImageCategory.WEAPONS_PLAYER_HANDGUN;
      imageString = "handgun.png";
    }

    public void Reload()
    {
      //TODO: Wait x Time 
      //player.IsShooting = false;
    }
  }
}
