namespace olbaid_mortel_7720.MVVM.Model.Object.Weapons
{
  public class SpawnObject : GameObject
  {
    #region Properties

    public int X;
    public int Y;

    #endregion Properties

    #region Constructor
    public SpawnObject(string name, bool visible, bool penetrable, int x, int y) : base(name, visible, penetrable)
    {
      X = x;
      Y = y;
    }
    #endregion Constructor

    #region Methods
    #endregion Methods
  }
}
