using olbaid_mortel_7720.Helper;

namespace olbaid_mortel_7720.MVVM.Model.Object
{
  public abstract class GameObject
  {
    #region Properties
    public string Name;
    bool Visible;
    public bool Penetrable;
    protected ImageCategory category;
    protected string imageString;
    #endregion Properties

    public GameObject(string name, bool visible, bool penetrable)
    {
      Name = name;
      this.Visible = visible;
      Penetrable = penetrable;
    }

    public ImageCategory GetCategory()
    {
      return category;
    }
    public string GetImageString()
    {
      return imageString;
    }
  }
}
