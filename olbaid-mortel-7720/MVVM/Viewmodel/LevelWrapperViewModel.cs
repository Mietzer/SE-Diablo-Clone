using System;
using System.Collections.Generic;
using System.Linq;

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

        private int enemyPlaced;

        /// <summary>
        /// How many ticks the spawning of new enemies should wait 
        /// </summary>
        private uint timerTicksToWaitEnemySpawning = 60;
        private uint maxEnemies = 50;
        private List<Enemy> spawnList;

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
            Setup();
        }
        #endregion Constructor

        #region Methods
        public void Setup()
        {
            InitCommands();
            AddLevel();
            AddPlayer();
            InitTimer();
        }

        private new void Dispose()
        {
            //Remove from timer
            GameTimer timer = GameTimer.Instance;
            timer.RemoveByName(nameof(this.AddEnemy) + GetHashCode());

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

            GC.Collect();
        }

        private void InitCommands()
        {
            ResumeGameCommand = new RelayCommand(ResumeGame, CanResumeGame);
            LeaveGameCommand = new RelayCommand(LeaveGame, CanLeaveGame);
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
            Player p = new Player(200, 150, 64, 32, 100, 5, (CurrentLevel as MapView).ViewModel);
            PlayerView = new PlayerCanvas(p);

            Gui = new UserControl();
            Canvas guiCanvas = new Canvas();
            Gui.Content = guiCanvas;
            int guiHeight = 40;

            PlayerHealthbarView playerHealthbar = new PlayerHealthbarView(p);
            playerHealthbar.Height = guiHeight;
            playerHealthbar.Width = guiHeight * 6;
            Canvas.SetTop(playerHealthbar, 20);
            Canvas.SetLeft(playerHealthbar, 20);
            guiCanvas.Children.Add(playerHealthbar);

            PlayerWeaponView weaponImage = new PlayerWeaponView(p);
            weaponImage.Height = guiHeight;
            weaponImage.Width = guiHeight * 2;
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
                //Creating View to display Enemies
                spawnList = CreateSpawnList(10);
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
            level1.SpawnEnemies((CurrentLevel as MapView).ViewModel, maxEnemies);
            usedLevel = level1;
        }

        /// <summary>
        /// Method to Pause/ Resume Game, depending on current state
        /// </summary>
        public void TogglePause()
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
            //TODO: handle win/loose (saving data of win and unlock new level)
            Dispose();
            GameTimer.Instance.CleanUp();
            NavigationLocator.MainViewModel.SwitchView(new LevelSelectionViewModel());
        }

        /// <summary>
        /// Method to Win Game
        /// </summary>
        private void Win()
        {
            //TODO: Wenn Bosse Besigt WON

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
            //TODO: Ask user if he really wants to leave
            LeaveMatch();
        }

        public bool CanLeaveGame() => !IsRunning;
        #endregion Commands
    }
}
