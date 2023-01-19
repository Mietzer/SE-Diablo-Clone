namespace olbaid_mortel_7720.Helper
{
  /// <summary>
  /// Class separating the images in different categories
  /// </summary>
  public class ImageCategory
  {
    public string Value { get; set; }

    private ImageCategory(string value)
    {
      Value = value;
    }

    internal static readonly ImageCategory GENERAL = new ImageCategory("");
    internal static readonly ImageCategory BOSSBAR = new ImageCategory("Bossbar/");
    internal static readonly ImageCategory BUTTONS = new ImageCategory("Buttons/");
    internal static readonly ImageCategory MANUAL = new ImageCategory("Manual/");
    internal static readonly ImageCategory LEVEL_PREVIEW = new ImageCategory("LevelPreview/");
    internal static readonly ImageCategory PLAYER = new ImageCategory("Entities/Player/");
    internal static readonly ImageCategory MELEE = new ImageCategory("Entities/Melee/");
    internal static readonly ImageCategory RANGED = new ImageCategory("Entities/Ranged/");
    internal static readonly ImageCategory BOSS = new ImageCategory("Entities/Boss/");
    internal static readonly ImageCategory BULLETS = new ImageCategory("Entities/Bullets/");
    internal static readonly ImageCategory HEALTHBAR = new ImageCategory("Healthbar/");
    internal static readonly ImageCategory HEALTHBAR_ICONS = new ImageCategory("Healthbar/Icons/");
    internal static readonly ImageCategory ITEMS = new ImageCategory("Items/");
    internal static readonly ImageCategory TILESETS = new ImageCategory("Tilesets/");
    internal static readonly ImageCategory WEAPONS_PLAYER_HANDGUN = new ImageCategory("Weapons/Player/Handgun/");
    internal static readonly ImageCategory WEAPONS_PLAYER_RIFLE = new ImageCategory("Weapons/Player/Rifle/");
  }
}