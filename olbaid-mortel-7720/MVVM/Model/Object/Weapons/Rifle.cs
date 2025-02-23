﻿using olbaid_mortel_7720.Helper;
using System.Windows.Media;

namespace olbaid_mortel_7720.MVVM.Model.Object.Weapons
{
  public class Rifle : Weapon
  {
    #region Constructor
    public Rifle() : base()
    {
      Damage = 30;
      reloadtime = 4;
      category = ImageCategory.WEAPONS_PLAYER_RIFLE;
      imageString = "rifle.png";
      this.Munition = new Munition(5, 10, new ImageBrush(ImageImporter.Import(ImageCategory.BULLETS, "rifle-bullet.png")), "ShotPlayer");
    }
    #endregion Constructor

    #region Methods

    #endregion Methods
  }
}
