﻿using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Model.Enemies;
using olbaid_mortel_7720.MVVM.Model.Object.Weapons;
using olbaid_mortel_7720.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using WpfAnimatedGif;


namespace olbaid_mortel_7720.MVVM.Viewmodel
{
  public class LevelWrapperViewModel : BaseViewModel
  {
    #region Properties
    private PlayerCanvas playerView;
    public PlayerCanvas PlayerView
    {
      get { return playerView; }
      set
      {
        playerView = value;
        OnPropertyChanged(nameof(PlayerView));
      }
    }

    private UserControl gui;
    public UserControl Gui
    {
      get { return gui; }
      set
      {
        gui = value;
        OnPropertyChanged(nameof(Gui));
      }
    }


    private EnemyCanvas enemyView;
    public EnemyCanvas EnemyView
    {
      get { return enemyView; }
      set
      {
        enemyView = value;
        OnPropertyChanged(nameof(EnemyView));
      }
    }

    private DropObjectCanvas dropobjects;
    public DropObjectCanvas DropObjcects
    {
      get { return dropobjects; }
      set
      {
        dropobjects = value;
        OnPropertyChanged(nameof(DropObjcects));
      }
    }

    private ManualCanvas manual;
    public ManualCanvas Manual
    {
      get { return manual; }
      set
      {
        manual = value;
        OnPropertyChanged(nameof(Manual));
      }
    }

    private int enemyPlaced;

    /// <summary>
    /// How many ticks the spawning of new enemies should wait 
    /// </summary>
    private uint timerTicksToWaitEnemySpawning = 60;
    private uint maxEnemies = 20;
    private List<Enemy> spawnList;

    private int usedLevelID;
    private Level usedLevel;
    private double bluescreenPercent { get; set; } = 0.0;
    private UserControl currentLevel;
    public UserControl CurrentLevel
    {
      get { return currentLevel; }
      set { currentLevel = value; }
    }

    private bool isRunning;
    public bool IsRunning
    {
      get { return isRunning; }
      set
      {
        isRunning = value;
        OnPropertyChanged(nameof(IsRunning));
      }
    }

    private Player player;
    public Player Player
    {
      get { return player; }
      set
      {
        player = value;
        OnPropertyChanged(nameof(Player));
      }
    }

    private PlayerWeaponView weaponImage;
    public PlayerWeaponView WeaponImage
    {
      get { return weaponImage; }
      set
      {
        weaponImage = value;
        OnPropertyChanged(nameof(WeaponImage));
      }
    }

    private string deathscreenText;
    public string DeathscreenText
    {
      get { return deathscreenText; }
      set
      {
        deathscreenText = value;
        OnPropertyChanged(nameof(DeathscreenText));
      }
    }

    public string DeathscreenInfo { get; private set; } = "Press SPACE or ENTER to go to Levelselection. Press R to restart. You can't search online" +
                                                          " later for this Error: HAL_GOODGAMEPLAY-Failed " +
                                                          " If you like to resolve the issue over the phone you can't call our support at 1-234-567-890";
    private bool playerAlive = true;
    public bool PlayerAlive
    {
      get { return playerAlive; }
      set
      {
        playerAlive = value;
        OnPropertyChanged(nameof(PlayerAlive));
      }
    }

    private bool playerHasWon = false;
    public bool PlayerHasWon
    {
      get { return playerHasWon; }
      set
      {
        playerHasWon = value;
        OnPropertyChanged(nameof(PlayerHasWon));
      }
    }

    private long timestampStart;
    private TimeSpan levelTimespan;
    public TimeSpan LevelTimespan
    {
      get { return levelTimespan; }
      set
      {
        levelTimespan = value;
        OnPropertyChanged(nameof(LevelTimespan));
      }
    }

    private SolidColorBrush textColor = new(Colors.AntiqueWhite);
    public SolidColorBrush TextColor
    {
      get { return textColor; }
      set
      {
        textColor = value;
        OnPropertyChanged(nameof(TextColor));
      }
    }
    private SolidColorBrush defaultStarColor = new(Colors.DarkGoldenrod);

    private SolidColorBrush firstStarColor = new(Colors.DarkGray);
    public SolidColorBrush FirstStarColor
    {
      get { return firstStarColor; }
      set
      {
        firstStarColor = value;
        if (value == new SolidColorBrush(Colors.DarkGray))
          SecondStarColor = ThirdStarColor = value;
        OnPropertyChanged(nameof(FirstStarColor));
      }
    }

    private SolidColorBrush secondStarColor = new(Colors.DarkGray);
    public SolidColorBrush SecondStarColor
    {
      get { return secondStarColor; }
      set
      {
        secondStarColor = value;
        if (value == defaultStarColor)
          FirstStarColor = value;
        if (value == new SolidColorBrush(Colors.DarkGray))
          ThirdStarColor = value;
        OnPropertyChanged(nameof(SecondStarColor));
      }
    }

    private SolidColorBrush thirdStarColor = new(Colors.DarkGray);
    public SolidColorBrush ThirdStarColor
    {
      get { return thirdStarColor; }
      set
      {
        thirdStarColor = value;
        if (value == defaultStarColor)
          SecondStarColor = FirstStarColor = value;

        OnPropertyChanged(nameof(ThirdStarColor));
      }
    }

    #endregion Properties

    #region Constructor
    public LevelWrapperViewModel(int selectedLevel)
    {
      usedLevelID = selectedLevel;
      Setup();
    }
    #endregion Constructor

    #region Methods
    public void Setup()
    {
      InitCommands();
      AddLevel();
      AddPlayer();
      AddDroppedObjectsView();
      InitManual();
      InitTimer();
    }

    private new void Dispose()
    {
      //Remove from timer
      GameTimer timer = GameTimer.Instance;
      timer.RemoveByName(nameof(this.AddEnemy) + GetHashCode());
      timer.RemoveByName(nameof(this.PlayerDied) + GetHashCode());

      //Dispose in SubViewModels
      if (EnemyView != null)
      {
        (EnemyView.DataContext as EnemyViewModel)?.Dispose();
        EnemyView.DataContext = null;
        EnemyView = null;
      }
      (PlayerView.DataContext as PlayerViewModel).Dispose();

      spawnList?.Clear();
      spawnList = null;

      PlayerView.DataContext = null;
      PlayerView = null;

      CurrentLevel.DataContext = null;
      CurrentLevel = null;

      Gui.Content = null;
      Gui = null;

      Manual = null;

      GC.Collect();
    }

    private void InitCommands()
    {
      ResumeGameCommand = new RelayCommand(ResumeGame, CanResumeGame);
      LeaveGameCommand = new RelayCommand(LeaveGame, CanLeaveGame);
    }

    private void InitManual()
    {
      DataProvider dataProvider = new DataProvider();
      string firstTime = dataProvider.LoadData<string>("Manual");
      if (firstTime == null || firstTime.Equals("true"))
      {
        dataProvider.SaveData("false", "Manual");

        Manual = new ManualCanvas();
        GameTimer.ExecuteWithInterval(250, args =>
        {
          DoubleAnimation animation = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(200), FillBehavior.Stop);
          animation.Completed += delegate
          {
            Manual.Content = null;
            Manual = null;
          };
          Manual.BeginAnimation(UIElement.OpacityProperty, animation);
        }, true);
      }
    }

    /// <summary>
    /// Add method(s) to timer
    /// </summary>
    private void InitTimer()
    {
      GameTimer timer = GameTimer.Instance;
      timer.Execute(AddEnemy, nameof(this.AddEnemy) + GetHashCode());

      timer.Start();
      IsRunning = GameTimer.Instance.IsRunning;
    }

    private void AddPlayer()
    {
      SpawnObject PlayerSpawnObject = usedLevel.Map.PlayerSpawnPoint();
      player = new Player(PlayerSpawnObject.X, PlayerSpawnObject.Y, 64, 32, (CurrentLevel as MapView).ViewModel);
      PlayerView = new PlayerCanvas(player, usedLevel.DroppedObjects);
      player.PlayerDied += PlayerDied;
      player.PlayerWon += PlayerWon;

      Gui = new UserControl();
      Canvas guiCanvas = new Canvas();
      Gui.Content = guiCanvas;
      int guiHeight = 40;

      PlayerHealthbarView playerHealthbar = new PlayerHealthbarView(player);
      playerHealthbar.Height = guiHeight;
      playerHealthbar.Width = guiHeight * 6;
      Canvas.SetTop(playerHealthbar, 20);
      Canvas.SetLeft(playerHealthbar, 20);
      guiCanvas.Children.Add(playerHealthbar);

      weaponImage = new PlayerWeaponView(player);
      weaponImage.Height = guiHeight;
      weaponImage.Width = guiHeight * 2;
      Canvas.SetTop(weaponImage, 20);
      Canvas.SetLeft(weaponImage, 20 + playerHealthbar.Width + 20);
      guiCanvas.Children.Add(weaponImage);

      if (System.Diagnostics.Debugger.IsAttached)
      {
        Rectangle hitbox = new Rectangle();
        hitbox.Height = (int)player.Hitbox.Height;
        hitbox.Width = (int)player.Hitbox.Width;
        hitbox.Stroke = Brushes.DodgerBlue;
        hitbox.StrokeThickness = 1;
        Binding xBind = new Binding("Hitbox.X");
        xBind.Source = player;
        hitbox.SetBinding(Canvas.LeftProperty, xBind);
        Binding yBind = new Binding("Hitbox.Y");
        yBind.Source = player;
        hitbox.SetBinding(Canvas.TopProperty, yBind);
        PlayerView.PlayerCanvasObject.Children.Add(hitbox);
      }
    }

    private void PlayerDied()
    {
      //Remove Event
      player.PlayerDied -= PlayerDied;
      PlayerAlive = false;

      GameTimer timer = GameTimer.Instance;
      timer.Execute(ChangeBluescreenText, nameof(this.PlayerDied) + GetHashCode());
    }

    private void PlayerWon()
    {
      //Remove Event
      player.PlayerWon -= PlayerWon;
      PlayerAlive = true;

      GameTimer.Instance.Stop(); //Pause game
      PlayerHasWon = true;

      //Save stats of level
      DataProvider dataProvider = new DataProvider();
      ObservableCollection<LevelModel> loadedLevels = dataProvider.LoadData<ObservableCollection<LevelModel>>("Leveldata");
      if (loadedLevels == null) return;

      LevelModel loadedCurrentLevel = loadedLevels.First(l => l.LevelID == usedLevelID);

      CheckLevelStats(loadedCurrentLevel);

      LevelModel loadedNextLevel = loadedLevels.FirstOrDefault(l => l.LevelID == usedLevelID + 1);
      if (loadedNextLevel != null)
        loadedNextLevel.IsUnlocked = true;

      dataProvider.SaveData(loadedLevels, "Leveldata");
    }

    private void AddEnemy(EventArgs spawn)
    {
      if (enemyPlaced == 0 || (enemyPlaced < maxEnemies && !CheckIfNotDead(spawnList)))
      {
        //Wait some time for spawning
        if (timerTicksToWaitEnemySpawning != 0)
        {
          timerTicksToWaitEnemySpawning--;
          return;
        }
        timerTicksToWaitEnemySpawning = 60;
        if (timestampStart == 0) //First Waves
          timestampStart = DateTime.UtcNow.Ticks;
        //Creating View to display Enemies
        spawnList = CreateSpawnList(10);
        EnemyView = new EnemyCanvas(spawnList, PlayerView.MyPlayer);
      }
      else if (enemyPlaced == maxEnemies && !CheckIfNotDead(spawnList))
      {
        spawnList = CreateSpawnList(1);
        EnemyView = new EnemyCanvas(spawnList, PlayerView.MyPlayer);
      }
      else
        return;


      foreach (Enemy e in spawnList)
      {
        Image enemyImage = new Image();
        enemyImage.Height = e.Height;
        enemyImage.Width = e.Width;
        ImageBehavior.SetAnimatedSource(enemyImage, e.Image);
        //Placing Enemies and Adding them to the Canvas
        e.Model = enemyImage;
        Canvas.SetTop(e.Model, e.YCoord);
        Canvas.SetLeft(e.Model, e.XCoord);
        EnemyView.EnemyCanvasObject.Children.Add(e.Model);

        if (System.Diagnostics.Debugger.IsAttached)
        {
          Rectangle hitbox = new Rectangle();
          hitbox.Height = (int)e.Hitbox.Height;
          hitbox.Width = (int)e.Hitbox.Width;
          hitbox.Stroke = Brushes.Red;
          hitbox.StrokeThickness = 1;
          Binding bindHitboxX = new Binding("Hitbox.X");
          bindHitboxX.Source = e;
          hitbox.SetBinding(Canvas.LeftProperty, bindHitboxX);
          Binding bindHitboxY = new Binding("Hitbox.Y");
          bindHitboxY.Source = e;
          hitbox.SetBinding(Canvas.TopProperty, bindHitboxY);
          EnemyView.EnemyCanvasObject.Children.Add(hitbox);

          TextBlock enemyHealth = new TextBlock();
          enemyHealth.Foreground = Brushes.Red;
          Binding bindEnemyHealth = new Binding(nameof(e.Health));
          bindEnemyHealth.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
          bindEnemyHealth.Mode = BindingMode.OneWay;
          bindEnemyHealth.Source = e;
          enemyHealth.SetBinding(TextBlock.TextProperty, bindEnemyHealth);
          Binding bindEnemyX = new Binding(nameof(e.XCoord));
          bindEnemyX.Source = e;
          enemyHealth.SetBinding(Canvas.LeftProperty, bindEnemyX);
          Binding bindEnemyY = new Binding(nameof(e.YCoord));
          bindEnemyY.Source = e;
          enemyHealth.SetBinding(Canvas.TopProperty, bindEnemyY);
          EnemyView.EnemyCanvasObject.Children.Add(enemyHealth);
        }

        if (e is EnemyBoss)
        {
          BossHealthbarView bossHealthbar = new BossHealthbarView();
          bossHealthbar.Width = 32 * 2;
          bossHealthbar.Height = bossHealthbar.Width / 12;
          bossHealthbar.Tag = "BossHealthbar";
          bossHealthbar.DataContext = e as EnemyBoss;
          Binding bindBossHealthbarX = new Binding("Hitbox.X");
          bindBossHealthbarX.Source = e;
          bossHealthbar.SetBinding(Canvas.LeftProperty, bindBossHealthbarX);
          Binding bindBossHealthbarY = new Binding("Hitbox.Y");
          bindBossHealthbarY.Source = e;
          bossHealthbar.SetBinding(Canvas.TopProperty, bindBossHealthbarY);
          EnemyView.EnemyCanvasObject.Children.Add(bossHealthbar);
        }

        enemyPlaced++;
      }
    }

    /// <summary>
    /// Loads the next enemies for this map
    /// </summary>
    /// <param name="spawnCount">size of next enemy wave</param>
    /// <returns></returns>
    private List<Enemy> CreateSpawnList(int spawnCount)
    {
      Enemy[] enemies = new Enemy[spawnCount];
      usedLevel.EnemySpawnList.CopyTo(0, enemies, 0, spawnCount);

      foreach (Enemy enemy in enemies)
        usedLevel.EnemySpawnList.Remove(enemy);

      return enemies.ToList();
    }

    private bool CheckIfNotDead(List<Enemy> enemyList)
      => enemyList.Any(e => e.Health > 0);

    private void AddLevel()
    {
      switch (usedLevelID)
      {
        case 1:
          AddLevel1Data();
          break;
        case 2:
          AddLevel2Data();
          break;
        case 3:
          AddLevel3Data();
          break;
      }
    }

    private void AddLevel1Data()
    {
      Level level1 = new Level(new Map("./Levels/Level1.tmx", "./Levels/Level1.tsx"));
      CurrentLevel = new MapView(level1.Map);
      level1.SpawnEnemies((CurrentLevel as MapView).ViewModel, maxEnemies);
      usedLevel = level1;
    }
    private void AddLevel2Data()
    {
      Level level2 = new Level(new Map("./Levels/Level2.tmx", "./Levels/Level2.tsx"));
      CurrentLevel = new MapView(level2.Map);
      level2.SpawnEnemies((CurrentLevel as MapView).ViewModel, maxEnemies);
      usedLevel = level2;
    }
    private void AddLevel3Data()
    {
      Level level1 = new Level(new Map("./Levels/Level3.tmx", "./Levels/Level3.tsx"));
      CurrentLevel = new MapView(level1.Map);
      level1.SpawnEnemies((CurrentLevel as MapView).ViewModel, maxEnemies);
      usedLevel = level1;
    }
    private void AddDroppedObjectsView()
    {
      DropObjcects = new DropObjectCanvas(usedLevel.DroppedObjects, Player);
    }

    private void CheckLevelStats(LevelModel levelModel)
    {
      TimeSpan levelTime = new(DateTime.UtcNow.Ticks - timestampStart);
      int seconds = (int)Math.Round(levelTime.TotalSeconds);
      levelTime = TimeSpan.FromSeconds(seconds);
      LevelTimespan = levelTime;

      if (levelTime < levelModel.BestTime || levelModel.BestTime == new TimeSpan(0))
        levelModel.BestTime = levelTime;

      TimeSpan goodTime = new(0, 0, 0);
      //Ratio of Hits and overall shots
      double goodShotRatio = 1.0;
      int goodRemainingHealth = 100;
      switch (levelModel.LevelID)
      {
        case 1:
          goodTime = new(0, 2, 20);
          goodShotRatio = 0.7;
          goodRemainingHealth = 80;
          break;
        case 2:
          goodTime = new(0, 1, 55);
          goodShotRatio = 0.72;
          goodRemainingHealth = 85;
          break;
        case 3:
          goodTime = new(0, 2, 15);
          goodShotRatio = 0.69;
          goodRemainingHealth = 77;
          break;
      }

      //Set 0 to 3 stars, depending on Stats
      if (levelTime < goodTime || Player.ShotHits / Player.OverallShots >= goodShotRatio || Player.HealthPoints > goodRemainingHealth)
      {
        levelModel.Star1 = true;
        FirstStarColor = defaultStarColor;
      }
      if ((levelTime < goodTime && Player.ShotHits / Player.OverallShots >= goodShotRatio) ||
          (Player.HealthPoints > goodRemainingHealth && levelTime < goodTime) ||
          (Player.HealthPoints > goodRemainingHealth && Player.ShotHits / Player.OverallShots >= goodShotRatio))
      {
        levelModel.Star1 = levelModel.Star2 = true;
        SecondStarColor = defaultStarColor;
      }
      if (levelTime < goodTime && Player.ShotHits / Player.OverallShots >= goodShotRatio && Player.HealthPoints > goodRemainingHealth)
      {
        levelModel.Star1 = levelModel.Star2 = levelModel.Star3 = true;
        ThirdStarColor = defaultStarColor;
      }
    }
    /// <summary>
    /// Method to Pause/ Resume Game, depending on current state
    /// </summary>
    public void TogglePause()
    {
      if (PlayerHasWon) return;
      GameTimer timer = GameTimer.Instance;

      if (!IsRunning)
        timer.Start();
      else
        timer.Stop();

      IsRunning = timer.IsRunning;
      OnPropertyChanged(nameof(timer.IsRunning));
    }

    /// <summary>
    /// Sets view back to Levelselection
    /// </summary>
    public void LeaveMatch()
    {
      if (!PlayerAlive || PlayerHasWon)
      {
        Dispose();
        GameTimer.Instance.CleanUp();
        NavigationLocator.MainViewModel.SwitchView(new LevelSelectionViewModel());
      }
    }

    public void RestartMatch()
    {
      if (!PlayerAlive)
      {
        LeaveMatch();
        NavigationLocator.MainViewModel.SwitchView(new LevelWrapperViewModel(usedLevelID));
      }
    }


    private void ChangeBluescreenText(EventArgs e)
    {
      bluescreenPercent += 0.05;
      DeathscreenText = $"Your Level run into a problem and needs to restart. We're just collecting some error" +
                                               $" info, and then you'll restart {(int)bluescreenPercent}% complete)";

      if (bluescreenPercent > 100)
        LeaveMatch();
    }
    #endregion Methods

    #region Commands
    public RelayCommand ResumeGameCommand { get; set; }

    public void ResumeGame(object sender)
    {
      TogglePause();
    }

    public bool CanResumeGame() => !IsRunning;

    public RelayCommand LeaveGameCommand { get; set; }
    public void LeaveGame(object sender)
    {
      PlayerAlive = false;
      LeaveMatch();
    }


    public bool CanLeaveGame() => !IsRunning;
    #endregion Commands
  }
}
