using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Model.Enemies;
using olbaid_mortel_7720.MVVM.Utils;
using olbaid_mortel_7720.MVVM.View;
using olbaid_mortel_7720.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
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
      Player p = new Player(100, 100, 64, 32, 100, 5);
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
      Random rnd = new Random();
      List<Enemy> spawnList = new List<Enemy>();
      int maxEnemy = 10;
      for (int i = 0; i < maxEnemy; i++)
      {
        //Creating Enemies and Adding them to a List
        //EnemyMelee e = new EnemyMelee(rnd.Next(0, GlobalVariables.MaxX), rnd.Next(0, GlobalVariables.MaxY - 50), 64, 32, 3, 100, 2);
        EnemyRanged r = new EnemyRanged(rnd.Next(0, GlobalVariables.MaxX), rnd.Next(0, GlobalVariables.MaxY - 50), 64, 32, 3, 100, 2);
        //spawnList.Add(e);
        spawnList.Add(r);
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
        
        if (System.Diagnostics.Debugger.IsAttached)
        {
          Rectangle hitbox = new Rectangle();
          hitbox.Height = (int)e.Hitbox.Height;
          hitbox.Width = (int)e.Hitbox.Width;
          hitbox.Stroke = Brushes.Red;
          hitbox.StrokeThickness = 1;
          Binding xBind = new Binding("Hitbox.X");
          xBind.Source = e;
          hitbox.SetBinding(Canvas.LeftProperty, xBind);
          Binding yBind = new Binding("Hitbox.Y");
          yBind.Source = e;
          hitbox.SetBinding(Canvas.TopProperty, yBind);
          EnemyView.EnemyCanvasObject.Children.Add(hitbox);
        }
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
