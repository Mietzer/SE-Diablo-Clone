using System.Linq;
using TiledCS;

namespace olbaid_mortel_7720.MVVM.Model
{
  public class Map
  {
    #region Properties
    string PathMap { get; set; }
    string PathTileset { get; set; }

    #endregion Properties

    public Map(string pathmap, string pathtileset)
    {
      this.PathMap = pathmap;
      this.PathTileset = pathtileset;

    }



    #region Methods
    public TiledCS.TiledSourceRect[,] Load()
    {
      TiledCS.TiledSourceRect[,] Rect = new TiledCS.TiledSourceRect[5, 100];
      int Layerzahler = 0;
      //Import Tildmap
      var map = new TiledMap(this.PathMap);
      var tilesets = map.GetTiledTilesets(this.PathTileset); // DO NOT forget the / at the end
      var tileLayers = map.Layers.Where(x => x.type == TiledLayerType.TileLayer);

      foreach (var layer in tileLayers)
      {
        for (var y = 0; y < layer.height; y++) //10
        {
          for (var x = 0; x < layer.width; x++) //10
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
            //var rect = ;
            Rect[Layerzahler, index] = map.GetSourceRect(mapTileset, tileset, gid);
            // Render sprite at position tileX, tileY using the rect

          }
        }
        Layerzahler++;
      }

      return Rect;
    }
    #endregion Methods

  }
}
