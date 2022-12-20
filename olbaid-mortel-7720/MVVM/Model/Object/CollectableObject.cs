using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
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
    
    public Rect Hitbox { get; private set; }
    public int X { get; private set; }
    public int Y { get; private set; }
    public Canvas Canvas { get; private set; }
    public delegate void OnRemove(CollectableObject collectableObject);
    public event OnRemove OnRemoveEvent;
    
    public bool CanBeCollected()
    {
      return remainingLifetime > 0;
    }

    public CollectableObject(string name, bool visible, int lifetime, int x, int y) : base(name, visible, false)
    {
      this.overallLifetime = this.remainingLifetime = lifetime;
      this.X = x;
      this.Y = y;
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
      this.Remove();
      this.WhenCollected(player);
    }

    public abstract void WhenCollected(Player player);

    public void Spawn(Canvas canvas)
    {
      Rectangle rectangle = new Rectangle();
      rectangle.Tag = GetHashCode();

      rectangle.Width = 20;
      rectangle.Height = 20;
      
      if (Debugger.IsAttached) rectangle.Stroke = Brushes.Blue;

      ImageBrush myImageBrush = new ImageBrush();
      myImageBrush.ImageSource = ImageImporter.Import(this.category, this.imageString);
      myImageBrush.ViewboxUnits = BrushMappingMode.RelativeToBoundingBox;
      myImageBrush.Stretch = Stretch.Uniform;
      myImageBrush.TileMode = TileMode.None;
      myImageBrush.AlignmentX = AlignmentX.Left;
      myImageBrush.AlignmentY = AlignmentY.Top;

      rectangle.Fill = myImageBrush;

      double xPosition = X - rectangle.Width / 2;
      double yPosition = Y - rectangle.Height / 2;
      
      Canvas.SetTop(rectangle, yPosition);
      Canvas.SetLeft(rectangle, xPosition);
      this.Hitbox = new Rect(xPosition, yPosition, rectangle.Width, rectangle.Height);
      canvas.Children.Add(rectangle);
      this.Canvas = canvas;
    }

    private void Remove()
    {
      GameTimer.Instance.RemoveByName(Name + GetHashCode());
      if (Canvas != null)
      {
        UIElement element = Canvas.Children.Cast<UIElement>().FirstOrDefault(e => e is Rectangle && (int)e.GetValue(Canvas.TagProperty) == GetHashCode());
        if (element != null)
        {
          Canvas.Children.Remove(element);
        }
      }
      OnRemoveEvent?.Invoke(this);
    }
  }
}
