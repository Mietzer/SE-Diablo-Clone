using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace olbaid_mortel_7720.MVVM.Viewmodel
{
  public class LevelSelectionViewModel : NotifyObject
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
    #endregion Properties
    public LevelSelectionViewModel()
    {
      InitCommands();
    }
    #region Methods
    public void InitPlayer(Window w)
    {
      DependencyObject d = w;
      Stack<DependencyObject> stack = new();
      stack.Push(d);

      //Search Grid row 0 in Visualtree to get actual Size of field
      while ((d as Grid)?.Name != "Menubar" && stack.Count > 0)
      {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(d); i++)
          stack.Push(VisualTreeHelper.GetChild(d, i));
        d = stack.Pop();
      }

      double h = (d as Grid).ActualHeight;

      //Create Player and add view
      Player p = new Player(0, 0, 0, 0, (int)w.Width, (int)(w.ActualHeight - h), 128, 64, 100, 5);
      PlayerView = new PlayerCanvas(p);
    }

    private void InitCommands()
    {
      SelectLevel1Command = new RelayCommand(SelectLevel1, CanSelectLevel1);
      SelectLevel2Command = new RelayCommand(SelectLevel2, CanSelectLevel2);
      SelectLevel3Command = new RelayCommand(SelectLevel3, CanSelectLevel3);
    }
    #endregion Methods

    #region Commands
    public RelayCommand SelectLevel1Command { get; set; }
    public void SelectLevel1(object sender)
    {
      //TODO
    }
    public bool CanSelectLevel1()
      => true; 
    public RelayCommand SelectLevel2Command { get; set; }
    public void SelectLevel2(object sender)
    {
      //TODO
    }
    public bool CanSelectLevel2()
      => true; //TODO: Load Info, if Level 2 is unlocked
    public RelayCommand SelectLevel3Command { get; set; }
    public void SelectLevel3(object sender)
    {
      //TODO
    }
    public bool CanSelectLevel3()
      => false; //TODO: Load Info, if Level 3 is unlocked
    #endregion Commands

  }
}
