using olbaid_mortel_7720.GameplayClasses;
using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Utils;
using olbaid_mortel_7720.MVVM.View;
using olbaid_mortel_7720.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using WpfAnimatedGif;

namespace olbaid_mortel_7720.MVVM.Viewmodel
{
  public class LevelWrapperViewModel : NotifyObject
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

    private PlayerHealthbarView playerHealthbar;

    public PlayerHealthbarView PlayerHealthbar
    {
      get { return playerHealthbar; }
      set
      {
        playerHealthbar = value;
        OnPropertyChanged(nameof(PlayerHealthbar));
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

    private UserControl currentLevel;
    public UserControl CurrentLevel
    {
      get { return currentLevel; }
      set { currentLevel = value; }
    }
    #endregion Properties

    #region Constructor
    public LevelWrapperViewModel()
    {
      Setup();
    }
    #endregion Constructor

    #region Methods
    public void Setup()
    {
      AddLevel();
      AddPlayer();
      AddEnemy();
    }

    private void AddPlayer()
    {
      Player p = new Player(0, 0, 64, 32, 100, 5);
      PlayerView = new PlayerCanvas(p);
      PlayerHealthbar = new PlayerHealthbarView(p);
      PlayerHealthbar.Height = 20;
      PlayerHealthbar.Width = PlayerHealthbar.Height * 6;
    }

    private void AddEnemy()
    {
      Random rnd = new Random();
      List<Enemy> spawnList = new List<Enemy>();
      int maxEnemy = 10;
      for (int i = 0; i < maxEnemy; i++)
      {
        //Creating Enemies and Adding them to a List
        EnemyMelee e = new EnemyMelee(rnd.Next(0, GlobalVariables.MaxX), rnd.Next(0, GlobalVariables.MaxY - 50), 64, 32, 3, 100, 2);
        spawnList.Add(e);
      }
      //Creating View to display Enemies
      EnemyView = new EnemyCanvas(spawnList, PlayerView.MyPlayer);

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
      }
    }

    private void AddLevel()
    {
      //TODO: Depending on some Variable, using of Level 1,2 or 3
      //currentLevel = this;
      Level level1 = new Level(new Map("./Levels/Level1.tmx", "./Levels/Level1.tsx"));
      CurrentLevel = new MapView(level1.Map);
    }
    #endregion Methods

    #region Commands

    #endregion Commands
  }
}
