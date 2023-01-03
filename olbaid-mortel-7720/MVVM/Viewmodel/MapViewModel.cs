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
  /// <summary>
  /// MapViewModel for creating the map for the view
  /// </summary>
  public class MapViewModel : NotifyObject
  {
    #region Properties
    public List<Barrier> Barriers;
    private Map map;
    public Canvas Canvas;
    #endregion Properties

    #region Constructor
    public MapViewModel(Canvas canvas, Map map)
    {
      Barriers = new List<Barrier>();
      this.map = map;
      Canvas = canvas;
      RenderMap();
    }
    #endregion Constructor

    #region Methods
    public void RenderMap()
    {
      List<MapObject> rednermap = map.Load();
      Dictionary<string, BitmapImage> tilesets = new Dictionary<string, BitmapImage>();
      tilesets.Add("Level1", ImageImporter.Import(ImageCategory.TILESETS, "Level1.png"));
      tilesets.Add("Furniture", ImageImporter.Import(ImageCategory.TILESETS, "Furniture.png"));
      tilesets.Add("Level2", ImageImporter.Import(ImageCategory.TILESETS, "Level2.png"));
      tilesets.Add("Level3", ImageImporter.Import(ImageCategory.TILESETS, "Level3.png"));

      //Randering the Map 
      for (int i = 0; i < rednermap.Count; i++)
      {
        Rectangle rectangle = new Rectangle();

        double renderValue = 42.74; //Empirically determined will for scale size 32 = 42.74 in WPF

        rectangle.Width = renderValue * ((double)(rednermap[i].Graphic.Imagex + rednermap[i].Graphic.Imagewidth) / 32);   //Adjustment of the rectangle according to the poison of the image section if not image section is not displayed. 
        rectangle.Height = renderValue * ((double)(rednermap[i].Graphic.Imagey + rednermap[i].Graphic.Imageheight) / 32);

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
        myImageBrush.Transform = new MatrixTransform(0.75d, 0.0d, 0.0d, 0.75d, xImageOffset, yImageOffset); // 0.75 Scaling to remove WPF scaling to get actual image size. To find correct x and y coordinates.
        rectangle.Fill = myImageBrush;


        double yTileValue = ((rednermap[i].Graphic.Index - (rednermap[i].Graphic.Index % map.MapWidth)) / map.MapWidth) * 32;  //Calculates plating in the canvas
        double xTileValue = (rednermap[i].Graphic.Index % map.MapWidth) * 32;
        Canvas.SetTop(rectangle, yTileValue);
        Canvas.SetLeft(rectangle, xTileValue);
        Canvas.Children.Add(rectangle);

        MapObject mapObject = rednermap[i];
        if (mapObject.HasCollision() && !mapObject.Name.Equals(MapLayerType.FLOOR) && !mapObject.Name.Equals(MapLayerType.STAIR) && !mapObject.Name.Equals(MapLayerType.TREE) && !mapObject.Name.Equals(MapLayerType.LAMP))
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
          Barriers.Add(new Barrier(barrierCollision, MapLayerType.GetBarrierType(mapObject.Name), mapObject.Penetrable ? Barrier.BarrierTag.Destroyable : Barrier.BarrierTag.None));
        }
      }
    }
  }
  #endregion Methods
}
