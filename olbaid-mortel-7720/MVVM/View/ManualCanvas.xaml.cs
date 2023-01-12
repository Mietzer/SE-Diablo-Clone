using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Model.Object;
using olbaid_mortel_7720.MVVM.Viewmodel;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

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
      
      stackPanel.Children.Add(CreateKeyLabel("to move", "W", "A", "S", "D"));
      stackPanel.Children.Add(CreateKeyLabel("to change the weapon", "1", "2"));
      stackPanel.Children.Add(CreateMouseLabel("to aim and shoot"));
      stackPanel.Children.Add(CreateKeyLabel("to collect items", "E"));
      stackPanel.Children.Add(CreateKeyLabel("to pause the game", "ESC", "SPC"));
      
      Canvas.SetLeft(stackPanel, 100);
      Canvas.SetBottom(stackPanel, 100);
      ManualCanvasObject.Children.Add(stackPanel);
    }
    
    private Label CreateKeyLabel(string caption, params string[] keys)
    {
      Label label = new Label();
      StackPanel panel = new StackPanel();
      panel.Orientation = Orientation.Horizontal;
      ImageBrush imageBrush = new ImageBrush();
      imageBrush.ImageSource = ImageImporter.Import(ImageCategory.MANUAL, "key.png");
      imageBrush.Stretch = Stretch.Uniform;
      
      foreach (string key in keys)
      {
        Grid grid = new Grid();
        grid.Width = 50;
        grid.Height = 50;
        grid.Background = imageBrush;
        grid.Margin = new System.Windows.Thickness(0, 0 , 5, 0);
        TextBlock textBlock = new TextBlock();
        textBlock.Text = key;
        grid.Children.Add(textBlock);
        panel.Children.Add(grid);
      }
      panel.Children.Add(new Label() { Content = caption });
      label.Content = panel;
      return label;
    }
    
    private Label CreateMouseLabel(string caption)
    {
      Label label = new Label();
      StackPanel panel = new StackPanel();
      panel.Orientation = Orientation.Horizontal;
      Image image = new Image();
      image.Height = 50;
      image.Width = 50;
      image.Source = ImageImporter.Import(ImageCategory.MANUAL, "mouse.png");
      panel.Children.Add(image);
      panel.Children.Add(new Label() { Content = caption });
      label.Content = panel;
      return label;
    }
  }
}
