using olbaid_mortel_7720.MVVM.Model.Object;
using olbaid_mortel_7720.MVVM.Model.Object.Weapons;
using olbaid_mortel_7720.Object;
using System;
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
      var tilesets = map.GetTiledTilesets(this.PathTileset);
      var tileLayers = map.Layers.Where(x => x.type == TiledLayerType.TileLayer);

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

            MapObject mapObject = new MapObject(layer.name, new Graphics(tileset.Image.source, rect.height, rect.width, rect.x, rect.y, index), true, layer.name == MapLayerType.FLOOR || layer.name == MapLayerType.TREE || layer.name == MapLayerType.LAMP ? true : false);

            TiledObject[] objects = map.GetTiledTile(mapTileset, tileset, gid).objects;
            if (objects.Length > 0)
            {
              TiledObject collisonobject = objects[0];
              mapObject.AddCollisionBox(collisonobject.x + (index % MapWidth) * 32, collisonobject.y + (index - (index % MapWidth)) / MapWidth * 32, collisonobject.width, collisonobject.height);
            }

            mapObjects.Add(mapObject);
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
      var tileObject = map.Layers.Where(x => x.type == TiledLayerType.ObjectLayer);

      //Creat ObjectList for Plasing Objects in Map
      foreach (var layer in tileObject)
      {
        foreach (var obj in layer.objects)
        {
          spawnObjects.Add(new SpawnObject(obj.name, true, true, Convert.ToInt32(obj.x), Convert.ToInt32(obj.y)));
        }
      }

      return spawnObjects;
    }

    public SpawnObject PlayerSpawnPoint()
    {
      List<SpawnObject> spawnObjects = LoadObjects();

      foreach (var obj in spawnObjects)
      {
        if (obj.Name == "Player Spawn")
          return obj;
      }
      return null;
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
