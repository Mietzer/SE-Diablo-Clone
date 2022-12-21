using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Model.Object;
using olbaid_mortel_7720.MVVM.Viewmodel;
using System.Collections.Generic;
using System.Windows.Controls;

namespace olbaid_mortel_7720.MVVM.View
{
  /// <summary>
  /// Interaktionslogik für DropObjectCanvas.xaml
  /// </summary>
  public partial class ManualCanvas : UserControl
  {
    public ManualCanvas()
    {
      InitializeComponent();
      WriteManual();
    }

    public void WriteManual()
    {
      StackPanel stackPanel = new StackPanel();
      
      Label wasd = new Label();
      wasd.Content = "WASD: to move";
      stackPanel.Children.Add(wasd);
      
      Label num = new Label();
      num.Content = "1 / 2: to change the weapon";
      stackPanel.Children.Add(num);
      
      Label mouse = new Label();
      mouse.Content = "Mouse: to aim and shoot";
      stackPanel.Children.Add(mouse);
      
      Label e = new Label();
      e.Content = "E: to collect items";
      stackPanel.Children.Add(e);
      
      Label esc = new Label();
      esc.Content = "ESC / Space: to pause the game";
      stackPanel.Children.Add(esc);
      
      Canvas.SetLeft(stackPanel, 100);
      Canvas.SetBottom(stackPanel, 100);
      ManualCanvasObject.Children.Add(stackPanel);
    }
  }
}
