using olbaid_mortel_7720.MVVM.Utils;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace olbaid_mortel_7720.MVVM.View
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window, INotifyPropertyChanged
  {
    public UserControl CurrentView { get; private set; }
    private int menuBarHeight = 0;

    public MainWindow()
    {
      CurrentView = new LevelSelectionView();
      InitializeComponent();
    }

    #region Events
    private void Minimize(object sender, RoutedEventArgs e)
    {
      App.Current.MainWindow.WindowState = WindowState.Minimized;
    }
    private void Maximize(object sender, RoutedEventArgs e)
    {
      if (GlobalVariables.InGame) //Dont allow, if ingame
        return;

      if (App.Current.MainWindow.WindowState == WindowState.Maximized)
        App.Current.MainWindow.WindowState = WindowState.Normal;
      else if (App.Current.MainWindow.WindowState == WindowState.Normal)
        App.Current.MainWindow.WindowState = WindowState.Maximized;

      GlobalVariables.MaxX = (int)ActualWidth;
      GlobalVariables.MaxY = (int)ActualHeight - menuBarHeight;

    }
    private void Close(object sender, RoutedEventArgs e)
    {
      App.Current.Shutdown();
    }
    private void MoveWindow(object sender, MouseButtonEventArgs e)
    {
      if (e.LeftButton == MouseButtonState.Pressed)
        DragMove();
    }
    private void LoadedWindow(object sender, RoutedEventArgs e)
    {
      //Get Height of Window Row 0 with the Visual Tree
      DependencyObject d = this;
      Stack<DependencyObject> stack = new();
      stack.Push(d);
      while (d is not Grid && stack.Count > 0)
      {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(d); i++)
          stack.Push(VisualTreeHelper.GetChild(d, i));
        d = stack.Pop();
      }
      DependencyObject row0 = VisualTreeHelper.GetChild(d, 0);
      menuBarHeight = (int)(row0 as Grid).ActualHeight;

      GlobalVariables.MinX = 0;
      GlobalVariables.MinY = 0;
      GlobalVariables.MaxX = (int)ActualWidth;
      GlobalVariables.MaxY = (int)ActualHeight - menuBarHeight;
      
      if (Debugger.IsAttached)
      {
        Debug.WriteLine("\n------------------\nStarted in DEBUG mode\n------------------\n"); 
      }
    }

    #endregion Events

    #region Methods
    public void SwitchView(UserControl newView)
    {
      CurrentView = newView;
      OnPropertyChanged(nameof(CurrentView));
      
      if (GlobalVariables.InGame)
      {
        BtnPanel.Children.Remove(BtnMaximize);
      }
      else
      {
        if (BtnPanel.Children.Contains(BtnMaximize) == false)
          BtnPanel.Children.Add(BtnMaximize);
      }
    }

    #endregion Methods

    #region PropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string prop = null)
      => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    #endregion PropertyChanged
  }
}
