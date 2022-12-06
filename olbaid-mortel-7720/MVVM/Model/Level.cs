using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.MVVM.Model.Enemies;
using olbaid_mortel_7720.MVVM.Model.Object.Weapons;
using olbaid_mortel_7720.MVVM.Utils;
using olbaid_mortel_7720.MVVM.Viewmodel;
using System;
using System.Collections.Generic;
using olbaid_mortel_7720.MVVM.Model.Object;

namespace olbaid_mortel_7720.MVVM.Model
{
  public class Level
  {
    #region Properties

    //TODO: ADD Progperties for Level

    public Map Map;
    
    private List<Enemy> _enemySpawnList;
    public List<Enemy> EnemySpawnList
    {
      get { return _enemySpawnList; }
      private set { _enemySpawnList = value; }
    }

    #endregion Properties

    public Level(Map map)
    {
      Map = map;
    }

    #region Methods
    //TODO: Add WIN/LOSE Game 
    public void SpawnEnemies(MapViewModel mapModel, uint melees, uint rareMelees, uint ranged, uint rareRanged, uint bossStage)
    {
      Random rnd = new Random();
      List<Enemy> spawnList = new List<Enemy>();
      List<SpawnObject> objectList = this.Map.LoadObjects();
      List<SpawnObject> spawnPoints = new List<SpawnObject>();
      int index;

      foreach(SpawnObject x in objectList)
      {
        if(x.Name == "Enemy Spawn")
        {
          spawnPoints.Add(x);
        }
      }

      spawnPoints = FixCoordinates(spawnPoints);

      for (int i = 0; i < melees; i++)
      {
        index = rnd.Next(0, spawnPoints.Count - 1);
        spawnList.Add(new EnemyMelee(Convert.ToInt32(spawnPoints[index].X), Convert.ToInt32(spawnPoints[index].Y), mapModel));
      }
      for (int i = 0; i < rareMelees; i++)
      {
        index = rnd.Next(0, spawnPoints.Count - 1);
        spawnList.Add(new EnemyRareMelee(Convert.ToInt32(spawnPoints[index].X), Convert.ToInt32(spawnPoints[index].Y), mapModel));
      }
      for (int i = 0; i < ranged; i++)
      {
        index = rnd.Next(0, spawnPoints.Count - 1);
        spawnList.Add(new EnemyRanged(Convert.ToInt32(spawnPoints[index].X), Convert.ToInt32(spawnPoints[index].Y), mapModel));
      }
      for (int i = 0; i < rareRanged; i++)
      {
        index = rnd.Next(0, spawnPoints.Count - 1);
        spawnList.Add(new EnemyRareRanged(Convert.ToInt32(spawnPoints[index].X), Convert.ToInt32(spawnPoints[index].Y), mapModel));
      }
      EnemySpawnList = spawnList;
    }

    private static List<SpawnObject> FixCoordinates(List<SpawnObject> list)
    {
      int count = 0;
      foreach(SpawnObject obj in list)
      {
        if(count == 1)
        {
          obj.Y = obj.Y - 20;
        }
        else if(count == 2)
        {
          obj.Y = obj.Y - 100;
        }
        else if(count == 3)
        {
          obj.Y = obj.Y - 40;
          obj.X = obj.X + 50;
        }
        else if(count == 4)
        {
          obj.X = obj.X - 150;
        }
        else if(count == 6)
        {
          obj.X = obj.X - 150;
        }
        count++;
      }

      return list;
    }

    #endregion Methods
  }
}
