using olbaid_mortel_7720.MVVM.Model;
using System;
using System.Collections.Generic;
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
      DataContext = this;
      MyPlayer = player;
      InitializeComponent();
    }

    public void Canvas_KeyDown(object sender, KeyEventArgs e)
    {
      MyPlayer.Move(sender, e);
    }
    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
      Window window = Window.GetWindow(this);
      window.KeyDown += Canvas_KeyDown;
    }
  }
}
