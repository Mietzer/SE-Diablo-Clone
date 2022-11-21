using olbaid_mortel_7720.MVVM.Model.Object;
using olbaid_mortel_7720.MVVM.Model.Object.Weapons;
using olbaid_mortel_7720.Object;
using System.Collections.Generic;
using System.Linq;
using TiledCS;

namespace olbaid_mortel_7720.MVVM.Model
{
  public class Map
  {
    #region Properties
    public string PathMap { get; set; }
    public string PathTileset { get; set; }

    public int MapHeight;
    public int MapWidth;

    #endregion Properties

    public Map(string pathmap, string pathtileset)
    {
      this.PathMap = pathmap;
      this.PathTileset = pathtileset;
      this.MapHeight = this.GetHeight();
      this.MapWidth = this.GetWidth();
    }



    #region Methods
    public List<MapObject> Load()
    {
      List<MapObject> mapObjects = new List<MapObject>();


      //Import Tildmap
      var map = new TiledMap(this.PathMap);
      var tilesets = map.GetTiledTilesets(this.PathTileset); // DO NOT forget the / at the end
      var tileLayers = map.Layers.Where(x => x.type == TiledLayerType.TileLayer);
      var tileObject = map.Layers.Where(x => x.type == TiledLayerType.ObjectLayer);

      //Creat MapObjects for Rendering Map
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
            var rect = map.GetSourceRect(mapTileset, tileset, gid);

            mapObjects.Add(new MapObject(layer.name, new Graphics(tileset.Image.source, rect.height, rect.width, rect.x, rect.y, index), true, layer.name == "Floor" ? true : false));

          }
        }

      }



      return mapObjects;
    }

    public List<SpawnObject> LoadObjects()
    {
      List<SpawnObject> spawnObjects = new List<SpawnObject>();

      //Import Tildmap
      var map = new TiledMap(this.PathMap);
      var tilesets = map.GetTiledTilesets(this.PathTileset); // DO NOT forget the / at the 
      var tileObject = map.Layers.Where(x => x.type == TiledLayerType.ObjectLayer);

      //Creat ObjectList for Plasing Objects in Map
      foreach (var layer in tileObject)
      {
        foreach (var obj in layer.objects)
        {
          //To Do Impelmentierung vpn Objecten z.b. Spawn Points
          spawnObjects.Add(new SpawnObject(obj.name, true, true, obj.x, obj.y, obj.width, obj.height));
        }
      }

      return spawnObjects;
    }


    public int GetHeight()
    {
      var map = new TiledMap(this.PathMap);
      return map.Height;
    }
    public int GetWidth()
    {
      var map = new TiledMap(this.PathMap);
      return map.Width;
    }
    #endregion Methods

  }
}
