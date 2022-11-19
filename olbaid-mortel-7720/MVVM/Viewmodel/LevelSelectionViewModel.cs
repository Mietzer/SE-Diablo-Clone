using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
      Player p = new Player(0, 0, 0, 0, (int)w.Width, (int)(w.ActualHeight - h), 60, 60, 100, 5);
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
      // This will get the current WORKING directory (i.e. \bin\Debug)
      string workingDirectory = Environment.CurrentDirectory;
      // or: Directory.GetCurrentDirectory() gives the same result

      // This will get the current PROJECT directory
      string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

      Level level1 = new Level(new Map($"{projectDirectory}/Levels/Level1.tmx", $"{projectDirectory}/Levels/Level1.tsx"));

      MapView mapView = new MapView(level1.Map);
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
