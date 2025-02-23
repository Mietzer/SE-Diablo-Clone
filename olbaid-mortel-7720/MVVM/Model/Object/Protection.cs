﻿using olbaid_mortel_7720.Helper;

namespace olbaid_mortel_7720.MVVM.Model.Object
{
  /// <summary>
  /// Class Armor for all Object that give the Player Armor
  /// </summary>
  public class Protection : CollectableObject
  {

    #region Constructor
    public Protection(int lifetime, int x, int y) : base("Protection", true, lifetime, x, y)
    {
      category = ImageCategory.ITEMS;
      imageString = "protection.png";
    }
    #endregion Constructor

    #region Methods
    public override void WhenCollected(Player player)
    {
      player.BeProtected();
    }
    #endregion Methods
  }
}
