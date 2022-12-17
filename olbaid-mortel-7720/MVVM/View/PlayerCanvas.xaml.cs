using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Viewmodel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace olbaid_mortel_7720.MVVM.View
{
  /// <summary>
  /// Interaktionslogik für PlayerCanvas.xaml
  /// </summary>
  public partial class PlayerCanvas : UserControl
  {
    public Player MyPlayer { get; set; }
    public Rectangle CustomPointer { get; set; }

    public PlayerCanvas(Player player)
    {
      MyPlayer = player;
      InitializeComponent();
      PlayerViewModel vm = new(player, PlayerCanvasObject);
      DataContext = vm;
      CustomPointer = new Rectangle();
      CustomPointer.Width = 32;
      CustomPointer.Height = 32;
      CustomPointer.Fill = new ImageBrush(ImageImporter.Import(ImageCategory.GENERAL, "crosshair.png"));
      PlayerCanvasObject.Children.Add(CustomPointer);
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
      //Init Events
      Window window = Window.GetWindow(this);
      window.KeyDown += Canvas_StartMove;
      window.KeyUp += Canvas_StopMove;
      window.MouseLeftButtonDown += Canvas_Shoot;
      window.MouseLeftButtonUp += Canvas_MouseUp;
      window.MouseMove += Canvas_MouseMove;
      window.KeyDown += Canvas_WeaponSelection;
    }

    /// <summary>
    /// Remove Event from Window as it's not used anymore
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UserControl_Unloaded(object sender, RoutedEventArgs e)
    {
      Window window = NavigationLocator.MainViewModel as Window;
      window.KeyDown -= Canvas_StartMove;
      window.KeyUp -= Canvas_StopMove;
      window.MouseLeftButtonDown -= Canvas_Shoot;
      window.MouseLeftButtonUp -= Canvas_MouseUp;
      window.MouseMove -= Canvas_MouseMove;
    }

    private void Canvas_MouseMove(object sender, MouseEventArgs e)
    {
      Point p = e.GetPosition(PlayerCanvasObject);
      Canvas.SetTop(CustomPointer, p.Y - CustomPointer.Height / 2);
      Canvas.SetLeft(CustomPointer, p.X - CustomPointer.Width / 2);

      Window window = Window.GetWindow(this);
      if (window != null)
        window.Cursor = Cursors.None;
    }

    public async void Canvas_StartMove(object sender, KeyEventArgs e)
    {
      PlayerViewModel vm = DataContext as PlayerViewModel;
      if (e.Key == Key.W)
      {
        vm.MoveDown = false;
        vm.MoveUp = true;
      }
      else if (e.Key == Key.S)
      {
        vm.MoveUp = false;
        vm.MoveDown = true;
      }
      else if (e.Key == Key.A)
      {
        vm.MoveRight = false;
        vm.MoveLeft = true;
      }
      else if (e.Key == Key.D)
      {
        vm.MoveLeft = false;
        vm.MoveRight = true;
      }
    }

    private void Canvas_StopMove(object sender, KeyEventArgs e)
    {
      PlayerViewModel vm = DataContext as PlayerViewModel;

      if (e.Key == Key.W)
        vm.MoveUp = false;
      else if (e.Key == Key.S)
        vm.MoveDown = false;
      else if (e.Key == Key.A)
        vm.MoveLeft = false;
      else if (e.Key == Key.D)
        vm.MoveRight = false;
    }

    public async void Canvas_WeaponSelection(object sender, KeyEventArgs e)
    {
      PlayerViewModel vm = DataContext as PlayerViewModel;

      if (e.Key == Key.D1)
      {
        vm.PrimaryWeapon = true;
        vm.SecondaryWeapon = false;
      }
      else if (e.Key == Key.D2)
      {
        vm.PrimaryWeapon = false;
        vm.SecondaryWeapon = true;
      }
    }


    #region shooting
    private async void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
    {
      (DataContext as PlayerViewModel).StopShooting();
    }

    private async void Canvas_Shoot(object sender, MouseButtonEventArgs e)
    {
      Point p = e.GetPosition(this);
      if (p.X < 0 || p.Y < 0 || !GameTimer.Instance.IsRunning)
        return;
      (DataContext as PlayerViewModel).Shoot(p);
    }
    #endregion shooting


  }
}