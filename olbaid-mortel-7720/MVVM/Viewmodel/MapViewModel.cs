using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Model.Object;
using olbaid_mortel_7720.MVVM.Model.Object.Weapons;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace olbaid_mortel_7720.MVVM.Viewmodel
{
  public class MapViewModel : NotifyObject
  {
    #region Properties

    public List<Rectangle> Rectangles;
    public List<Rect> Barriers;
    private Map map;
    public Canvas Canvas;

    #endregion Properties

    public MapViewModel(Canvas canvas, Map map)
    {
      Rectangles = new List<Rectangle>();
      Barriers = new List<Rect>();
      this.map = map;
      Canvas = canvas;
      RenderMap();
      CreatObjects();
    }

    #region Methods
    public void RenderMap()
    {
      List<MapObject> rednermap = map.Load();
      BitmapImage tilesetImage = ImageImporter.Import(ImageCategory.TILESETS, "Level1.png");

      //Randering the Map 
      for (int i = 0; i < rednermap.Count; i++)
      {
        Rectangles.Add(new Rectangle());

        double Wert = 42.74;

        Rectangles[i].Width = Wert * ((double)(rednermap[i].Graphic.Imagex + rednermap[i].Graphic.Imagewidth) / 32);
        Rectangles[i].Height = Wert * ((double)(rednermap[i].Graphic.Imagey + rednermap[i].Graphic.Imageheight) / 32);

        ImageBrush myImageBrush = new ImageBrush();
        myImageBrush.ImageSource = tilesetImage;
        myImageBrush.ViewboxUnits = BrushMappingMode.Absolute;
        myImageBrush.Stretch = Stretch.None;
        myImageBrush.TileMode = TileMode.None;
        myImageBrush.AlignmentX = AlignmentX.Left;
        myImageBrush.AlignmentY = AlignmentY.Top;

        double xImageOffset = -rednermap[i].Graphic.Imagex;
        double yImageOffset = -rednermap[i].Graphic.Imagey;
        myImageBrush.Transform = new MatrixTransform(0.75d, 0.0d, 0.0d, 0.75d, xImageOffset, yImageOffset); //m11=default 1   m12 m21 m22=default 1 0.75 da 0.25 Skalierungfaktor rausrechnen x y 
        Rectangles[i].Fill = myImageBrush;

        double yTileValue = ((rednermap[i].Graphic.Index - (rednermap[i].Graphic.Index % map.MapWidth)) / map.MapWidth) * 32;
        double xTileValue = (rednermap[i].Graphic.Index % map.MapWidth) * 32;
        Canvas.SetTop(Rectangles[i], yTileValue);
        Canvas.SetLeft(Rectangles[i], xTileValue);
        Canvas.Children.Add(Rectangles[i]);
        
        if (rednermap[i].Name == MapLayerType.INNER_WALL || rednermap[i].Name == MapLayerType.OUTER_WALL)
        {
          MapObject wall = rednermap[i];
          if (wall.HasCollision())
          {
            Rect wallCollision = wall.CollisionBox ?? Rect.Empty;
            if (System.Diagnostics.Debugger.IsAttached)
            {
              Rectangle rect = new Rectangle();
              rect.Height = wallCollision.Height;
              rect.Width = wallCollision.Width;
              rect.Stroke = Brushes.Orange;
              rect.StrokeThickness = 1;
              Canvas.SetTop(rect, wallCollision.Y);
              Canvas.SetLeft(rect, wallCollision.X);
              Canvas.Children.Add(rect);
            }
            Barriers.Add(wallCollision);
          }
        }
      }
    }

    public void CreatObjects()
    {
      List<SpawnObject> rednerobjects = map.LoadObjects();

      for (int i = 0; i < rednerobjects.Count; i++)
      {
      }
    }

    #endregion Methods
  }
}
