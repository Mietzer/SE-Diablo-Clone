using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace olbaid_mortel_7720.MVVM.Viewmodel
{
  public class LevelSelectionViewModel : BaseViewModel
  {
    #region Properties
    public ObservableCollection<LevelModel> Levellist { get; set; } = new();

    private DataProvider dataProvider { get; set; } = new();
    #endregion Properties
    public LevelSelectionViewModel()
    {
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
      OpenStartscreenCommand = new RelayCommand(OpenStartscreen, CanOpenStartscreen);
    }

    private new void Dispose()
    {
      Levellist?.Clear();
      Levellist = null;

      GC.Collect();
    }
    /// <summary>
    /// Creates the first instance of all Levels
    /// </summary>
    private void InitLevels()
    {
      LevelModel level1 = new LevelModel(1, "LEVEL 1", ImageImporter.Import(ImageCategory.LEVEL_PREVIEW, "Level1.png"), true);
      LevelModel level2 = new LevelModel(2, "LEVEL 2", ImageImporter.Import(ImageCategory.LEVEL_PREVIEW, "Level2.png"), false);
      LevelModel level3 = new LevelModel(3, "LEVEL 3", ImageImporter.Import(ImageCategory.LEVEL_PREVIEW, "Level3.png"), false);

      Levellist.Add(level1);
      Levellist.Add(level2);
      Levellist.Add(level3);
    }

    #endregion Methods

    #region Commands

    public RelayCommand SelectLevelCommand { get; set; }

    public void SelectLevel(object sender)
    {
      Dispose();
      NavigationLocator.MainViewModel.SwitchView(new LevelWrapperViewModel((int)sender));
    }

    public bool CanSelectLevel()
      => true;
    public RelayCommand OpenStartscreenCommand { get; set; }

    public void OpenStartscreen(object sender)
    {
      Dispose();
      NavigationLocator.MainViewModel.SwitchView(new StartscreenViewModel());
    }

    public bool CanOpenStartscreen()
      => true;

    #endregion Commands



  }
}
