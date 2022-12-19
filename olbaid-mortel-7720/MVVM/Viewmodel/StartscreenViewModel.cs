using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Utils;
using System.Windows;

namespace olbaid_mortel_7720.MVVM.Viewmodel
{
  public class StartscreenViewModel : BaseViewModel
  {
    #region Properties
    public bool FirstTime => GlobalVariables.FirstTime;
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
      GlobalVariables.FirstTime = false;
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
