using olbaid_mortel_7720.Helper;
using System.Windows;

namespace olbaid_mortel_7720.MVVM.Viewmodel
{
  public class StartscreenViewModel : BaseViewModel
  {
    #region Properties

    #endregion Properties

    #region Constructor
    public StartscreenViewModel()
    {
      InitCommands();
    }
    #endregion Constructor

    #region Methods
    private void InitCommands()
    {
      OpenLevelViewCommand = new(OpenLevelView, CanOpenLevelView);
      CloseApplicationCommand = new(CloseApplication, CanCloseApplication);
    }
    #endregion Methods

    #region Commands
    public RelayCommand OpenLevelViewCommand { get; set; }

    public void OpenLevelView(object sender)
    {
      NavigationLocator.MainViewModel.SwitchView(new LevelSelectionViewModel());
    }

    public bool CanOpenLevelView()
      => true;

    public RelayCommand CloseApplicationCommand { get; set; }
    public void CloseApplication(object sender)
    {
      Application.Current.Shutdown();
    }

    public bool CanCloseApplication()
      => true;
    #endregion Commands
  }
}
