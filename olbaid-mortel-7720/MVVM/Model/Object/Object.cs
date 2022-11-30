namespace olbaid_mortel_7720.MVVM.Model.Object
{
  public abstract class Object
  {
    #region Properties
    public string Name;
    bool Visible;
    public bool Penetrable;
    #endregion Properties

    public Object(string name, bool visible, bool penetrable)
    {
      Name = name;
      this.Visible = visible;
      Penetrable = penetrable;
    }

    #region Methods

    #endregion Methods
  }
}
