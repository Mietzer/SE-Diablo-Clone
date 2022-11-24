using olbaid_mortel_7720.Helper;

namespace olbaid_mortel_7720.Object.Weapons
{
    public class Rifle : Weapon
    {
      public Rifle()
      {
        category = ImageCategory.WEAPONS_PLAYER_HANDGUN;
        imageString = "rifle.png";
        Damage = 30;
      }
    }
}
