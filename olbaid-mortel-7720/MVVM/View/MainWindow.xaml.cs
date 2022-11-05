using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
      CurrentView = new LevelSelectionView();

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
      if(e.LeftButton == MouseButtonState.Pressed)
        DragMove();
    }
  }
}