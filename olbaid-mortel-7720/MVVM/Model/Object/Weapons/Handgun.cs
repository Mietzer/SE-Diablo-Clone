using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model.Object.Weapons;
using System.Windows.Media;

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
      this.Munition = new Munition(3, 6, new ImageBrush(ImageImporter.Import(ImageCategory.BULLETS, "bullet.png")), "ShotPlayer");
    }
    #region Methods

    #endregion Methods
  }
}
