using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.View;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
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

    private int usedLevelID;
    private Level usedLevel;
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


    #endregion Properties

    #region Constructor
    public LevelWrapperViewModel(int selectedLevel)
    {
      usedLevelID = selectedLevel;
      IsRunning = GameTimer.Instance.IsRunning;
      Setup();
    }
    #endregion Constructor

    #region Methods
    public void Setup()
    {
      InitCommands();
      AddLevel();
      AddPlayer();
      AddEnemy();
    }
    private void InitCommands()
    {
      ResumeGameCommand = new RelayCommand(ResumeGame, CanResumeGame);
      LeaveGameCommand = new RelayCommand(LeaveGame, CanLeaveGame);
    }

    private void AddPlayer()
    {
      Player p = new Player(200, 150, 64, 32, 100, 5, (CurrentLevel as MapView).Vm);
      PlayerView = new PlayerCanvas(p);

      Gui = new UserControl();
      Canvas guiCanvas = new Canvas();
      Gui.Content = guiCanvas;

      PlayerHealthbarView playerHealthbar = new PlayerHealthbarView(p);
      playerHealthbar.Height = 40;
      playerHealthbar.Width = playerHealthbar.Height * 6;
      Canvas.SetTop(playerHealthbar, 20);
      Canvas.SetLeft(playerHealthbar, 20);
      guiCanvas.Children.Add(playerHealthbar);

      PlayerWeaponView weaponImage = new PlayerWeaponView(p);
      weaponImage.Height = 40;
      weaponImage.Width = weaponImage.Height * 2;
      Canvas.SetTop(weaponImage, 20);
      Canvas.SetLeft(weaponImage, 20 + playerHealthbar.Width + 20);
      guiCanvas.Children.Add(weaponImage);

      if (System.Diagnostics.Debugger.IsAttached)
      {
        Rectangle hitbox = new Rectangle();
        hitbox.Height = (int)p.Hitbox.Height;
        hitbox.Width = (int)p.Hitbox.Width;
        hitbox.Stroke = Brushes.DodgerBlue;
        hitbox.StrokeThickness = 1;
        Binding xBind = new Binding("Hitbox.X");
        xBind.Source = p;
        hitbox.SetBinding(Canvas.LeftProperty, xBind);
        Binding yBind = new Binding("Hitbox.Y");
        yBind.Source = p;
        hitbox.SetBinding(Canvas.TopProperty, yBind);
        PlayerView.PlayerCanvasObject.Children.Add(hitbox);
      }
    }

    private void AddEnemy()
    {
      //Creating View to display Enemies
      EnemyView = new EnemyCanvas(usedLevel.EnemySpawnList, PlayerView.MyPlayer);

      foreach (Enemy e in usedLevel.EnemySpawnList)
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
      }
    }

    private void AddLevel()
    {
      // TODO: Depending on some Variable, using of Level 1,2 or 3
      switch (usedLevelID)
      {
        case 1:
          AddLevel1Data();
          break;
        default:
          break;
      }
    }

    private void AddLevel1Data()
    {
      // TODO: Add spawnlists with random choice out of a list of possible lists
      Level level1 = new Level(new Map("./Levels/Level1.tmx", "./Levels/Level1.tsx"));
      CurrentLevel = new MapView(level1.Map);
      level1.SpawnEnemies((CurrentLevel as MapView).Vm, 2, 2, 3, 1, 0);
      usedLevel = level1;
    }

    /// <summary>
    /// Method to Pause/ Resume Game, depending on current state
    /// </summary>
    public void PauseLevel()
    {
      GameTimer timer = GameTimer.Instance;

      if (IsRunning)
        timer.Stop();
      else
        timer.Start();

      IsRunning = !IsRunning;

      OnPropertyChanged(nameof(timer.IsRunning));
    }

    /// <summary>
    /// Sets view back to Levelselection
    /// </summary>
    private void LeaveMatch()
    {
      //TODO: Clearup, handle win/loose (saving data of win and unlock new level)
      NavigationLocator.MainViewModel.SwitchView(new LevelSelectionViewModel());
    }
    #endregion Methods

    #region Commands
    public RelayCommand ResumeGameCommand { get; set; }

    public void ResumeGame(object sender)
    {
      PauseLevel();
    }

    public bool CanResumeGame() => !IsRunning;

    public RelayCommand LeaveGameCommand { get; set; }
    public void LeaveGame(object sender)
    {
      //TODO: Ask user if he really wants to leave
      LeaveMatch();
    }

    public bool CanLeaveGame() => !IsRunning;
    #endregion Commands
  }
}
