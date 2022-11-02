using olbaid_mortel_7720.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
  /// Interaktionslogik für LevelSelectionView.xaml
  /// </summary>
  public partial class LevelSelectionView : UserControl, INotifyPropertyChanged
  {
    private PlayerCanvas playerView;
    public PlayerCanvas PlayerView
    {
      get { return playerView; }
      set { playerView = value;
        OnPropertyChanged(nameof(PlayerView));
      }
    }

    public LevelSelectionView()
    {
      DataContext = this;
      InitializeComponent();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
      Window w = Window.GetWindow(this);

      DependencyObject d = w;
      Stack<DependencyObject> stack = new();
      stack.Push(d);
      while (d is not Grid && stack.Count >0)
      {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(d); i++)
        {
          stack.Push(VisualTreeHelper.GetChild(d,i));
        }
        d = stack.Pop();
        
      }



      DependencyObject row0 = VisualTreeHelper.GetChild(d, 0);
      double h = (row0 as Grid).ActualHeight;


      Player p = new Player(0, 0, 0, 0, (int)w.Width, (int)(w.ActualHeight - h), 20, 20, 100, 5);
      PlayerView = new PlayerCanvas(p);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string prop = null)
      => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

  }
}
