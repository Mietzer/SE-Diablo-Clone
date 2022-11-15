using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.View;
using System;
using System.Collections.ObjectModel;

namespace olbaid_mortel_7720.MVVM.Viewmodel
{
  public class LevelSelectionViewModel : NotifyObject
  {
    #region Properties
    public MainWindow mainWindow { get; private set; }
    public ObservableCollection<LevelModel> Levellist { get; set; } = new();
    #endregion Properties
    public LevelSelectionViewModel()
    {
      //Examples
      LevelModel level1 = new LevelModel(true, true, false, 1, "LEVEL 1 Name", TimeSpan.FromSeconds(54.0), "", true);
      LevelModel level2 = new LevelModel(true, false, false, 2, "Das ist mein LEVEL 2.", TimeSpan.FromSeconds(43.0), "", false);
      LevelModel level3 = new LevelModel(false, true, true, 3, "LEVEL 3", TimeSpan.FromSeconds(97.0), "pack://application:,,,/Images/Items/paralysispotion.png", false);
      LevelModel level4 = new LevelModel(true, true, true, 4, "LEVEL 4", TimeSpan.FromSeconds(197.0), "pack://application:,,,/Images/Items/paralysispotion.png", false);
      Levellist.Add(level1);
      Levellist.Add(level2);
      Levellist.Add(level3);

      InitCommands();
    }
    #region Methods
    private void InitCommands()
    {
      SelectLevelCommand = new RelayCommand(SelectLevel, CanSelectLevel);
    }

    public void SetWindow(MainWindow w)
      => mainWindow = w;
    #endregion Methods

    #region Commands

    public RelayCommand SelectLevelCommand { get; set; }

    public void SelectLevel(object sender)
    {
      mainWindow.SwitchView(new LevelView((int)sender));
    }

    public bool CanSelectLevel()
      => true;

    #endregion Commands

  }
}
