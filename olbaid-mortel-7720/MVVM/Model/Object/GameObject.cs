using olbaid_mortel_7720.Helper;

namespace olbaid_mortel_7720.MVVM.Model.Object
{
  public abstract class GameObject
  {
    #region Properties
    public string Name;
    bool visible;
    public bool Penetrable;
    protected ImageCategory category;
    protected string imageString;
    #endregion Properties

    #region Constructor
    public GameObject(string name, bool visible, bool penetrable)
    {
      Name = name;
      this.visible = visible;
      Penetrable = penetrable;
    }
    #endregion Constructor

    #region Methods
    public ImageCategory GetCategory()
    {
      return category;
    }
    public string GetImageString()
    {
      return imageString;
    }
    #endregion Methods
  }
}
