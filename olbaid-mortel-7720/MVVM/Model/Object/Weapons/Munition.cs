using System.Windows.Media;

namespace olbaid_mortel_7720.MVVM.Model.Object.Weapons
{
  /// <summary>
  ///  Class for all types of Munition for Weapons
  /// </summary>
  public class Munition
  {
    #region Properties
    public int Height;
    public int Width;
    public Brush BulletImage;
    public string Name;
    #endregion Properties

    #region Constructor
    public Munition(int height, int width, Brush bulletimage, string name)
    {
      Height = height;
      Width = width;
      BulletImage = bulletimage;
      Name = name;
    }
    #endregion Constructor

    #region Methods

    #endregion Methods
  }
}
