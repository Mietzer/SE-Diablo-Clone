using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Model.Object;
using System.Collections.Generic;
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
    public List<Barrier> Barriers;
    private Map map;
    public Canvas Canvas;

    #endregion Properties

    public MapViewModel(Canvas canvas, Map map)
    {
      Rectangles = new List<Rectangle>();
      Barriers = new List<Barrier>();
      this.map = map;
      Canvas = canvas;
      RenderMap();
    }

    #region Methods
    public void RenderMap()
    {
      List<MapObject> rednermap = map.Load();
      Dictionary<string, BitmapImage> tilesets = new Dictionary<string, BitmapImage>();
      tilesets.Add("Level1", ImageImporter.Import(ImageCategory.TILESETS, "Level1.png"));
      tilesets.Add("Furniture", ImageImporter.Import(ImageCategory.TILESETS, "Furniture.png"));

      //Randering the Map 
      for (int i = 0; i < rednermap.Count; i++)
      {
        Rectangles.Add(new Rectangle());

        double renderValue = 42.74;

        Rectangles[i].Width = renderValue * ((double)(rednermap[i].Graphic.Imagex + rednermap[i].Graphic.Imagewidth) / 32);
        Rectangles[i].Height = renderValue * ((double)(rednermap[i].Graphic.Imagey + rednermap[i].Graphic.Imageheight) / 32);

        string ImageName = rednermap[i].Graphic.PathtoGraphics.Substring(19, rednermap[i].Graphic.PathtoGraphics.Length - 19).Replace(".png", "");
        BitmapImage tilesetImage = tilesets[ImageName];
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

        MapObject mapObject = rednermap[i];
        if (mapObject.HasCollision() && !mapObject.Name.Equals(MapLayerType.FLOOR) && !mapObject.Name.Equals(MapLayerType.STAIR))
        {
          Rect barrierCollision = mapObject.CollisionBox ?? Rect.Empty;
          if (System.Diagnostics.Debugger.IsAttached)
          {
            Rectangle rect = new Rectangle();
            rect.Height = barrierCollision.Height;
            rect.Width = barrierCollision.Width;
            rect.Stroke = Brushes.Orange;
            rect.StrokeThickness = 1;
            Canvas.SetTop(rect, barrierCollision.Y);
            Canvas.SetLeft(rect, barrierCollision.X);
            Canvas.Children.Add(rect);
          }
          Barriers.Add(new Barrier(barrierCollision, MapLayerType.GetBarrierType(mapObject.Name)));
        }
      }
    }


  }

  #endregion Methods
}
