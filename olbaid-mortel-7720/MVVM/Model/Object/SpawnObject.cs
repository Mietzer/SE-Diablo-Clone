using System.Windows.Media;
using System.Windows.Shapes;

namespace olbaid_mortel_7720.MVVM.Model.Object.Weapons
{
  public class SpawnObject : Object
  {
    #region Properties
    public Rectangle Hitbox;
    public float X;
    public float Y;
    #endregion Properties
    public SpawnObject(string name, bool visible, bool penetrable, float x, float y, float width = 1, float height = 1) : base(name, visible, penetrable)
    {
      X = x;
      Y = y;
      Hitbox = CreatHitbox(height, width);
    }

    #region Methods
    Rectangle CreatHitbox(float height, float width)
    {
      Rectangle hitbox = new Rectangle();
      hitbox.Height = height;
      hitbox.Width = width;
      //TODO: To colour the spawn zones, the following must be removed from the finished game. 
      hitbox.Fill = Brushes.Black;
      return hitbox;
    }
    #endregion Methods
  }
}
