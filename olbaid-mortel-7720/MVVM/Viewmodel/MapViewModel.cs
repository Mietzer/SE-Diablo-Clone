using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Model.Object;
using olbaid_mortel_7720.MVVM.Model.Object.Weapons;
using System.Collections.Generic;
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
    private Map map;
    public Canvas Canvas;

    #endregion Properties

    public MapViewModel(Canvas canvas, Map map)
    {
      Rectangles = new List<Rectangle>();
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

        double x = -rednermap[i].Graphic.Imagex;
        double y = -rednermap[i].Graphic.Imagey;
        myImageBrush.Transform = new MatrixTransform(0.75d, 0.0d, 0.0d, 0.75d, x, y); //m11=default 1   m12 m21 m22=default 1 0.75 da 0.25 Skalierungfaktor rausrechnen x y 
        Rectangles[i].Fill = myImageBrush;

        Canvas.SetTop(Rectangles[i], ((rednermap[i].Graphic.Index - (rednermap[i].Graphic.Index % map.MapWidth)) / map.MapWidth) * 32);
        Canvas.SetLeft(Rectangles[i], (rednermap[i].Graphic.Index % map.MapWidth) * 32);
        Canvas.Children.Add(Rectangles[i]);
      }
    }

    public void CreatObjects()
    {
      List<SpawnObject> rednerobjects = map.LoadObjects();

      for (int i = 0; i < rednerobjects.Count; i++)
      {
        Canvas.SetTop(rednerobjects[i].Hitbox, (rednerobjects[i].Y));
        Canvas.SetLeft(rednerobjects[i].Hitbox, (rednerobjects[i].X));
        Canvas.Children.Add(rednerobjects[i].Hitbox);
      }
    }

    #endregion Methods


  }

}
