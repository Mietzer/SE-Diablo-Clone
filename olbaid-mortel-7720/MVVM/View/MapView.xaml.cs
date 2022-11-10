using System.Linq;
using System.Windows.Controls;
using TiledCS;

namespace olbaid_mortel_7720.MVVM.View
{
  /// <summary>
  /// Interaktionslogik für MapView.xaml
  /// </summary>
  //public string[] Test { get; set; }
  public partial class MapView : UserControl
  {
    public MapView()
    {
      //Test Import Tild 
      var map = new TiledMap("C:\\Users\\Konsti\\OneDrive - Technische Hochschule Nürnberg Georg Simon Ohm\\Documents\\Studium\\Semester 3\\SE Praktikum\\git\\olbaid-mortel-7720\\Levels\\Level1.tmx");
      var tilesets = map.GetTiledTilesets("C:\\Users\\Konsti\\OneDrive - Technische Hochschule Nürnberg Georg Simon Ohm\\Documents\\Studium\\Semester 3\\SE Praktikum\\git\\olbaid-mortel-7720\\Levels\\Level1.tsx"); // DO NOT forget the / at the end
      var tileLayers = map.Layers.Where(x => x.type == TiledLayerType.TileLayer);

      foreach (var layer in tileLayers)
      {
        for (var y = 0; y < layer.height; y++)
        {
          for (var x = 0; x < layer.width; x++)
          {
            var index = (y * layer.width) + x; // Assuming the default render order is used which is from right to bottom
            var gid = layer.data[index]; // The tileset tile index
            var tileX = (x * map.TileWidth);
            var tileY = (y * map.TileHeight);

            // Gid 0 is used to tell there is no tile set
            if (gid == 0)
            {
              continue;
            }

            // Helper method to fetch the right TieldMapTileset instance. 
            // This is a connection object Tiled uses for linking the correct tileset to the gid value using the firstgid property.
            var mapTileset = map.GetTiledMapTileset(gid);

            // Retrieve the actual tileset based on the firstgid property of the connection object we retrieved just now
            var tileset = tilesets[mapTileset.firstgid];

            // Use the connection object as well as the tileset to figure out the source rectangle.
            var rect = map.GetSourceRect(mapTileset, tileset, gid);

            // Render sprite at position tileX, tileY using the rect

          }
        }
      }

      InitializeComponent();
    }
  }
}
