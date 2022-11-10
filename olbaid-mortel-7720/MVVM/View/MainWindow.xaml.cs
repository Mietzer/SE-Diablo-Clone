using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace olbaid_mortel_7720.MVVM.View
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public UserControl CurrentView { get; private set; }

    public MainWindow()
    {
      CurrentView = new MapView();


      InitializeComponent();

    }

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
  }
}