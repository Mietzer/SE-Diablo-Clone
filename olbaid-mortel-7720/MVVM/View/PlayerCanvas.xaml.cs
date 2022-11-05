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
    private int MinX;
    private int MinY;
    private int MaxX;
    private int MaxY;

    private string ShotName = "ShotPlayer";
    public PlayerCanvas(Player player)
    {
    
      MyPlayer = player;
      MinX = player.XCoordMin;
      MinY = player.YCoordMin;
      MaxX = player.XCoordMax;
      MaxY = player.YCoordMax;

      InitializeComponent();
      PlayerViewModel vm = new(player, PlayerCanvasObject);
      DataContext = vm;
    }

    public void Canvas_KeyDown(object sender, KeyEventArgs e)
    {
      MyPlayer.Move(sender, e);
    }
    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
      Window window = Window.GetWindow(this);
      window.KeyDown += Canvas_KeyDown;
      window.MouseLeftButtonDown += Canvas_Shoot;

      DispatcherTimer deleteTimer = new();
      deleteTimer.Tick += new EventHandler(CheckForDelete);
      deleteTimer.Interval = new TimeSpan(0, 0, 15);
      deleteTimer.Start();

  
    }

    #region shooting

    private void Canvas_Shoot(object sender, MouseButtonEventArgs e)
    {
      Point p = e.GetPosition(this);
      if (p.X < 0 || p.Y < 0)
        return;
      (DataContext as PlayerViewModel).Shoot(p);
      //double spawnPointX = MyPlayer.XCoord + MyPlayer.Width / 2, spawnPointY = MyPlayer.YCoord + MyPlayer.Height / 2;

      //Vector vector = new Vector(p.X - spawnPointX, p.Y - spawnPointY);
      //vector.Normalize();
      //Bullet s = new Bullet(25, 25, vector, new SolidColorBrush(Colors.MediumPurple), ShotName);
      //PlayerCanvasObject.Children.Add(s.Rectangle);
      //MyPlayer.Bullets.Add(s);

      ////Shot on Players left
      //if (vector.X < 0)
      //{
      //  spawnPointX -= s.Rectangle.Width;
      //  //Above
      //  if (vector.Y < 0)
      //    spawnPointY -= s.Rectangle.Height;
      //}
      ////Shot on Players right and above
      //else if (vector.X > 0 && vector.Y < 0)
      //  spawnPointY -= s.Rectangle.Height;

      //Canvas.SetLeft(s.Rectangle, spawnPointX + vector.X * 50);
      //Canvas.SetTop(s.Rectangle, spawnPointY + vector.Y * 50);
    }

    private void MoveShots(object sendern, EventArgs e)
    {
      //int velocity = 30;

      //foreach (Rectangle item in PlayerCanvasObject.Children)
      //{
      //  if(item.Name == ShotName)
      //  {
      //    Bullet s = MyPlayer.Bullets.Where(s => s.Rectangle == item).FirstOrDefault();
      //    Canvas.SetLeft(item, Canvas.GetLeft(item) +  s.Direction.X * velocity);
      //    Canvas.SetTop(item, Canvas.GetTop(item) + s.Direction.Y * velocity);

      //    if(Canvas.GetLeft(item) < MinX - item.Width|| Canvas.GetLeft(item) > MaxX
      //     || Canvas.GetTop(item) < MinY - item.Height || Canvas.GetTop(item) > MaxY)
      //    {
      //      PlayerCanvasObject.Children.Remove(item);
      //      MyPlayer.Bullets.Remove(s);
      //      break;
      //    }
      //  }
      //}
    }
  
    /// <summary>
    /// Delets every time called the Shot, which exists the longest time
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CheckForDelete(object sender, EventArgs e)
    {
      //double now = (int)DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
      //foreach (Rectangle item in PlayerCanvasObject.Children)
      //{
      //  if (item.Name == "ShotPlayer" && item.RadiusX <= now + 5000)
      //  {
      //    PlayerCanvasObject.Children.Remove(item);
      //    Bullet s = MyPlayer.Bullets.First();
      //    MyPlayer.Bullets.Remove(s);
      //    break;
      //  }
      //}
    }

    #endregion shooting

  }
}