using olbaid_mortel_7720.Helper;

// TODO: Namespaces
namespace olbaid_mortel_7720.Object.Weapons
{
    public class Rifle : Weapon
    {
      public Rifle()
      {
        category = ImageCategory.WEAPONS_PLAYER_RIFLE;
        imageString = "rifle.png";
        Damage = 30;
      }
    }
}
