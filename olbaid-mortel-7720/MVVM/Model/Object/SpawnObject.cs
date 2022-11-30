using System.Windows.Media;
using System.Windows.Shapes;

namespace olbaid_mortel_7720.MVVM.Model.Object.Weapons
{
  public class SpawnObject : Object
  {
    #region Properties
    
    public float X;
    public float Y;
    
    #endregion Properties
    
    public SpawnObject(string name, bool visible, bool penetrable, float x, float y) : base(name, visible, penetrable)
    {
      X = x;
      Y = y;
    }

    #region Methods
    #endregion Methods
  }
}
