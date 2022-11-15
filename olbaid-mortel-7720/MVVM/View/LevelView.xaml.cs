using olbaid_mortel_7720.MVVM.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace olbaid_mortel_7720.MVVM.View
{
  /// <summary>
  /// Interaktionslogik für LevelView.xaml
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


      Player p = new Player(0, 0, 0, 0, (int)w.Width, (int)gameHeight, 20, 20, 100, 5);
      PlayerView = new PlayerCanvas(p);
    }
    #endregion PlayerAdding

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
