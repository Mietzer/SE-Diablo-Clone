using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Utils;
using olbaid_mortel_7720.MVVM.View;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace olbaid_mortel_7720.MVVM.Viewmodel
{
  public class LevelSelectionViewModel : NotifyObject
  {
    #region Properties
    public MainWindow mainWindow { get; private set; }
    public ObservableCollection<LevelModel> Levellist { get; set; } = new();

    private DataProvider dataProvider { get; set; } = new();
    #endregion Properties
    public LevelSelectionViewModel()
    {
      App.Current.Exit += SaveEvent;

      InitLevels();
      Levellist.OrderBy(x => x.LevelID);
      ObservableCollection<LevelModel> loadedLevels = dataProvider.LoadData<ObservableCollection<LevelModel>>("Leveldata");
      if (loadedLevels != null)
        foreach (var item in Levellist)
        {
          //Refresh levelstats with saved stats
          LevelModel relatedLevel = loadedLevels.FirstOrDefault(x => x.LevelID == item.LevelID);
          item.RefreshData(relatedLevel);
        }

      //Save Data for next start
      dataProvider.SaveData(Levellist, "Leveldata");

      InitCommands();
    }
    #region Methods
    private void InitCommands()
    {
      SelectLevelCommand = new RelayCommand(SelectLevel, CanSelectLevel);
    }

    /// <summary>
    /// Creates the first instance of all Levels
    /// </summary>
    private void InitLevels()
    {
      //TODO: Right Category for LevelModelImages
      LevelModel level1 = new LevelModel(1, "LEVEL 1 Name", RessourceImporter.Import(ImageCategory.ITEMS, "paralysispotion.png"), true);
      LevelModel level2 = new LevelModel(2, "Das ist mein LEVEL 2.", RessourceImporter.Import(ImageCategory.ITEMS, "healthpack.png"), false);
      LevelModel level3 = new LevelModel(3, "LEVEL 3", RessourceImporter.Import(ImageCategory.ITEMS, "paralysispotion.png"), false);

      Levellist.Add(level1);
      Levellist.Add(level2);
      Levellist.Add(level3);

    }
    public void SetWindow(MainWindow w)
      => mainWindow = w;
    #endregion Methods

    #region Commands

    public RelayCommand SelectLevelCommand { get; set; }

    public void SelectLevel(object sender)
    {
      mainWindow.SwitchView(new LevelWrapperView((int)sender));
      GlobalVariables.InGame = true;
    }

    public bool CanSelectLevel()
      => true;

    #endregion Commands

    #region Events
    public void SaveEvent(object sender, EventArgs e)
    {
      dataProvider.SaveData(Levellist, "Leveldata");
    }
    #endregion Events

  }
}
