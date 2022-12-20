using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model.Object.Weapons;
using System.Windows.Media;

namespace olbaid_mortel_7720.Object.Weapons
{
  public class Rifle : Weapon
  {
    #region Properties

    #endregion Properties
    public Rifle() : base()
    {
      Damage = 300;
      reloadtime = 4;
      category = ImageCategory.WEAPONS_PLAYER_RIFLE;
      imageString = "rifle.png";
      this.Munition = new Munition(5, 10, new ImageBrush(ImageImporter.Import(ImageCategory.BULLETS, "rifle-bullet.png")), "ShotPlayer");
    }
    #region Methods

    #endregion Methods
  }
}
