namespace olbaid_mortel_7720.Helper
{
  public interface IMainViewModel
  {
    /// <summary>
    /// Sets the Current Viewmodel of the Mainview to the new one
    /// </summary>
    /// <param name="newViewModel">Viewmodel of new View</param>
    void SwitchView(BaseViewModel newViewModel);
  }
}
