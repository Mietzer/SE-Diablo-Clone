using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace olbaid_mortel_7720.MVVM.View
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window, INotifyPropertyChanged
  {
    public UserControl CurrentView { get; private set; }

    public MainWindow()
    {
      CurrentView = new LevelSelectionView();

      InitializeComponent();
    }

    #region Commands
    private void Minimize(object sender, RoutedEventArgs e)
    {
      App.Current.MainWindow.WindowState = WindowState.Minimized;
    }
    private void Maximize(object sender, RoutedEventArgs e)
    {
      if (App.Current.MainWindow.WindowState == WindowState.Maximized)
        App.Current.MainWindow.WindowState = WindowState.Normal;
      else if (App.Current.MainWindow.WindowState == WindowState.Normal)
        App.Current.MainWindow.WindowState = WindowState.Maximized;
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

    #endregion Commands

    #region Methods
    public void SwitchView(UserControl newView)
    {
      CurrentView = newView;
      OnPropertyChanged(nameof(CurrentView));
    }
    #endregion Methods

    #region PropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string prop = null)
      => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    #endregion PropertyChanged

  }
}