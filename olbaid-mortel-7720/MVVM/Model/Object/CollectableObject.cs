using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.Helper;
using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace olbaid_mortel_7720.MVVM.Model.Object
{
  /// <summary>
  /// Class for all types of objects that are collectable
  /// </summary>
  public abstract class CollectableObject : GameObject, ICollectable
  {
    private int overallLifetime;
    private int remainingLifetime;
    public bool CanBeCollected()
    {
      return remainingLifetime > 0;
    }

    public CollectableObject(string name, bool visible, int lifetime) : base(name, visible, false)
    {
      this.overallLifetime = this.remainingLifetime = lifetime;
      GameTimer.Instance.Execute(OnGameTick, Name + GetHashCode());
    }

    private void OnGameTick(EventArgs e)
    {
      this.remainingLifetime--;
      if (this.remainingLifetime <= 0)
      {
        this.Remove();
      }
    }

    public void OnCollect(Player player)
    {
      Debug.WriteLine(this.Name + " collected");
      this.Remove();
      this.WhenCollected(player);
    }

    public abstract void WhenCollected(Player player);

    public void Spawn(Canvas canvas, int x, int y)
    {
      Debug.WriteLine(this.Name + " spawned");
      if (canvas.Name == "DropObjectCanvas")
      {
        Rectangle rectangle = new Rectangle();

        rectangle.Width = 40;
        rectangle.Height = 40;
        rectangle.Stroke = Brushes.Blue;

        ImageBrush myImageBrush = new ImageBrush();
        myImageBrush.ImageSource = ImageImporter.Import(this.category, this.imageString);
        myImageBrush.ViewboxUnits = BrushMappingMode.Absolute;
        myImageBrush.Stretch = Stretch.None;
        myImageBrush.TileMode = TileMode.None;
        myImageBrush.AlignmentX = AlignmentX.Left;
        myImageBrush.AlignmentY = AlignmentY.Top;

        rectangle.Fill = myImageBrush;

        Canvas.SetTop(rectangle, y + 20);
        Canvas.SetLeft(rectangle, x);
        canvas.Children.Add(rectangle);
      }
    }

    private void Remove()
    {
      GameTimer.Instance.RemoveByName(Name + GetHashCode());
      // TODO: remove from canvas
    }
  }
}
