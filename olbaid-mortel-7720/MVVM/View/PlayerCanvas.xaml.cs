using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Viewmodel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
using System.Windows.Threading;
using System.Xaml.Schema;

namespace olbaid_mortel_7720.MVVM.View
{
  /// <summary>
  /// Interaktionslogik für PlayerCanvas.xaml
  /// </summary>
  public partial class PlayerCanvas : UserControl
  {
    public Player MyPlayer { get; set; }

    public PlayerCanvas(Player player)
    {
      MyPlayer = player;
      InitializeComponent();
      PlayerViewModel vm = new(player, PlayerCanvasObject);
      DataContext = vm;
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
      //Init Events
      Window window = Window.GetWindow(this);
      window.KeyDown += Canvas_StartMove;
      window.MouseLeftButtonDown += Canvas_Shoot;
      window.KeyUp += Canvas_StopMove;
    }

    public async void Canvas_StartMove(object sender, KeyEventArgs e)
    {
      PlayerViewModel vm = DataContext as PlayerViewModel;
      if (e.Key == Key.W)
      {
        vm.moveDown = false;
        vm.moveUp = true;
      }
      else if (e.Key == Key.S)
      {
        vm.moveUp = false;
        vm.moveDown = true;
      }
      else if (e.Key == Key.A)
      {
       vm.moveRight = false;
        vm.moveLeft = true;
      }
      else if (e.Key == Key.D)
      {
        vm.moveLeft = false;
        vm.moveRight = true;
      }
    }

    private void Canvas_StopMove(object sender, KeyEventArgs e)
    {
      PlayerViewModel vm = DataContext as PlayerViewModel;

      if (e.Key == Key.W)
        vm.moveUp = false;
      else if (e.Key == Key.S)
        vm.moveDown = false;
      else if (e.Key == Key.A)
        vm.moveLeft = false;
      else if (e.Key == Key.D)
        vm.moveRight = false;
    }

    #region shooting
    private async void Canvas_Shoot(object sender, MouseButtonEventArgs e)
    {
      Point p = e.GetPosition(this);
      if (p.X < 0 || p.Y < 0)
        return;
      (DataContext as PlayerViewModel).Shoot(p);
    }
    #endregion shooting
  }
}