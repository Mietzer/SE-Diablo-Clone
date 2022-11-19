using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace olbaid_mortel_7720.MVVM.View
{
    /// <summary>
    /// Interaktionslogik für LevelView.xaml
    /// Wrapperclass for gameelements
    /// </summary>
    public partial class LevelView : UserControl, INotifyPropertyChanged
    {
        public LevelView(int selectedLevel = 0)
        {
            //TODO: Maybe own Viewmodel, depending on usability,
            //espacially in how to add new Objects to the Level view

            //Currently not used: Will be called, if any Level is selected to Initialize general Stuff


            //TODO: Add Healthbar

            DataContext = this;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            AddPlayer();
            AddEnemy();
            AddLevel();
        }

        #region PlayerAdding
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
        private void AddPlayer()
        {
            //Get Height of Window Row 0 with the Visual Tree
            Window w = Window.GetWindow(this);
            DependencyObject d = w;
            Stack<DependencyObject> stack = new();
            stack.Push(d);
            while (d is not Grid && stack.Count > 0)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(d); i++)
                    stack.Push(VisualTreeHelper.GetChild(d, i));
                d = stack.Pop();
            }

            DependencyObject row0 = VisualTreeHelper.GetChild(d, 0);
            //Window Row 0 not part of game
            double gameHeight = w.ActualHeight - (row0 as Grid).ActualHeight;


            Player p = new Player(0, 0, 0, 0, (int)w.Width, (int)gameHeight, 128, 64, 100, 5);
            PlayerView = new PlayerCanvas(p);
        }
        #endregion PlayerAdding

        #region EnemySpawning

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

        private void AddEnemy()
        {
            Random rnd = new Random();
            Window w = Window.GetWindow(this);
            List<Enemy> spawnList = new List<Enemy>();
            int maxEnemy = 10;
            for (int i = 0; i < maxEnemy; i++)
            {
                //Creating Enemies and Adding them to a List
                EnemyMelee e = new EnemyMelee(rnd.Next(0, (int)w.Width), rnd.Next(0, (int)w.ActualHeight - 50), 0, 0, (int)w.Width, (int)(w.ActualHeight), 20, 20, 4, 100, 5);
                spawnList.Add(e);
            }
            //Creating View to display Enemies
            EnemyView = new EnemyCanvas(spawnList, PlayerView.MyPlayer);

            foreach (Enemy e in spawnList)
            {
                //Placing Enemies and Adding them to the Canvas
                Canvas.SetTop(e.Model, e.YCoord);
                Canvas.SetLeft(e.Model, e.XCoord);
                EnemyView.EnemyCanvasObject.Children.Add(e.Model);
            }
        }

        #endregion

        #region Level
        private UserControl currentLevel;
        public UserControl CurrentLevel
        {
            get { return currentLevel; }
            set { currentLevel = value; }
        }

        private void AddLevel()
        {
            //TODO: Depending on some Variable, using of Level 1,2 or 3
            // Current just for Testcases:
            currentLevel = this;
        }
        #endregion Level

        #region ProperyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string prop = null)
          => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        #endregion ProperyChanged


    }
}
