﻿using olbaid_mortel_7720.Helper;
using System;
using System.Text.Json.Serialization;
using System.Windows.Media.Imaging;

namespace olbaid_mortel_7720.MVVM.Model
{
  [Serializable]
  public class LevelModel : NotifyObject
  {
    #region Properties
    private bool star1;
    public bool Star1
    {
      get { return star1; }
      set
      {
        star1 = value;
        OnPropertyChanged(nameof(Star1));
      }
    }

    private bool star2;
    public bool Star2
    {
      get { return star2; }
      set
      {
        star2 = value;
        OnPropertyChanged(nameof(Star2));
      }
    }

    private bool star3;
    public bool Star3
    {
      get { return star3; }
      set
      {
        star3 = value;
        OnPropertyChanged(nameof(Star3));
      }
    }
    public int LevelID { get; set; }

    private string levelName;
    [JsonIgnore]
    public string LevelName
    {
      get { return levelName; }
      set
      {
        levelName = value;
        OnPropertyChanged(nameof(LevelName));
      }
    }

    private TimeSpan bestTime;
    public TimeSpan BestTime
    {
      get { return bestTime; }
      set
      {
        bestTime = value;
        OnPropertyChanged(nameof(BestTime));
      }
    }

    private BitmapImage previewPicture;
    [JsonIgnore]
    public BitmapImage PreviewPicture
    {
      get { return previewPicture; }
      set
      {
        previewPicture = value;
        OnPropertyChanged(nameof(PreviewPicture));
      }
    }

    private bool isUnlocked;
    public bool IsUnlocked
    {
      get { return isUnlocked; }
      set
      {
        isUnlocked = value;
        OnPropertyChanged(nameof(IsUnlocked));
      }
    }

    #endregion Properties

    #region Constructor
    public LevelModel(int levelID, string levelName, TimeSpan bestTime, BitmapImage previewPicture, bool isUnlocked, bool star1, bool star2, bool star3)
    {
      LevelID = levelID;
      LevelName = levelName;
      BestTime = bestTime;
      PreviewPicture = previewPicture;
      IsUnlocked = isUnlocked;
      Star1 = star1;
      Star2 = star2;
      Star3 = star3;
    }

    /// <summary>
    /// Constructor for new Levels
    /// </summary>
    /// <param name="levelID"></param>
    /// <param name="levelName"></param>
    /// <param name="bestTime"></param>
    /// <param name="previewPicture"></param>
    /// <param name="isUnlocked"></param>
    public LevelModel(int levelID, string levelName, BitmapImage previewPicture, bool isUnlocked)
    {
      LevelID = levelID;
      LevelName = levelName;
      PreviewPicture = previewPicture;
      IsUnlocked = isUnlocked;
      Star1 = Star2 = Star3 = false;

    }

    public LevelModel()
    { }
    #endregion Constructor

    #region Methods
    public void RefreshData(LevelModel newLevelData)
    {
      BestTime = newLevelData.BestTime;
      IsUnlocked = newLevelData.IsUnlocked;
      Star1 = newLevelData.Star1;
      Star2 = newLevelData.Star2;
      Star3 = newLevelData.Star3;
    }
    #endregion Methods
  }


}

