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

      for (int i = 0; i < melees; i++)
      {
        index = rnd.Next(0, spawnPoints.Count - 1);
        if(index == 1)
        {
          spawnList.Add(new EnemyMelee(Convert.ToInt32(spawnPoints[index].X), Convert.ToInt32(spawnPoints[index].Y - 20), mapModel));
        }
        else if(index == 2)
        {
          spawnList.Add(new EnemyMelee(Convert.ToInt32(spawnPoints[index].X), Convert.ToInt32(spawnPoints[index].Y - 100), mapModel));
        }
        else if(index == 3)
        {
          spawnList.Add(new EnemyMelee(Convert.ToInt32(spawnPoints[index].X + 100), Convert.ToInt32(spawnPoints[index].Y - 40), mapModel));
        }
        else if(index == 4)
        {
          spawnList.Add(new EnemyMelee(Convert.ToInt32(spawnPoints[index].X - 150), Convert.ToInt32(spawnPoints[index].Y), mapModel));
        }
        else if(index == 6)
        {
          spawnList.Add(new EnemyMelee(Convert.ToInt32(spawnPoints[index].X - 150), Convert.ToInt32(spawnPoints[index].Y), mapModel));
        }
        else
          spawnList.Add(new EnemyMelee(Convert.ToInt32(spawnPoints[index].X), Convert.ToInt32(spawnPoints[index].Y), mapModel));
      }
      for (int i = 0; i < rareMelees; i++)
      {
        index = rnd.Next(0, spawnPoints.Count - 1);
        if (index == 1)
        {
          spawnList.Add(new EnemyRareMelee(Convert.ToInt32(spawnPoints[index].X), Convert.ToInt32(spawnPoints[index].Y - 20), mapModel));
        }
        else if (index == 2)
        {
          spawnList.Add(new EnemyRareMelee(Convert.ToInt32(spawnPoints[index].X), Convert.ToInt32(spawnPoints[index].Y - 100), mapModel));
        }
        else if (index == 3)
        {
          spawnList.Add(new EnemyRareMelee(Convert.ToInt32(spawnPoints[index].X + 100), Convert.ToInt32(spawnPoints[index].Y - 40), mapModel));
        }
        else if (index == 4)
        {
          spawnList.Add(new EnemyRareMelee(Convert.ToInt32(spawnPoints[index].X - 150), Convert.ToInt32(spawnPoints[index].Y), mapModel));
        }
        else if (index == 6)
        {
          spawnList.Add(new EnemyRareMelee(Convert.ToInt32(spawnPoints[index].X - 150), Convert.ToInt32(spawnPoints[index].Y), mapModel));
        }
        else
          spawnList.Add(new EnemyRareMelee(Convert.ToInt32(spawnPoints[index].X), Convert.ToInt32(spawnPoints[index].Y), mapModel));
      }

      for (int i = 0; i < ranged; i++)
      {
        index = rnd.Next(0, spawnPoints.Count - 1);
        if (index == 1)
        {
          spawnList.Add(new EnemyRanged(Convert.ToInt32(spawnPoints[index].X), Convert.ToInt32(spawnPoints[index].Y - 20), mapModel));
        }
        else if (index == 2)
        {
          spawnList.Add(new EnemyRanged(Convert.ToInt32(spawnPoints[index].X), Convert.ToInt32(spawnPoints[index].Y - 100), mapModel));
        }
        else if (index == 3)
        {
          spawnList.Add(new EnemyRanged(Convert.ToInt32(spawnPoints[index].X + 100), Convert.ToInt32(spawnPoints[index].Y - 40), mapModel));
        }
        else if (index == 4)
        {
          spawnList.Add(new EnemyRanged(Convert.ToInt32(spawnPoints[index].X - 150), Convert.ToInt32(spawnPoints[index].Y), mapModel));
        }
        else if (index == 6)
        {
          spawnList.Add(new EnemyRanged(Convert.ToInt32(spawnPoints[index].X - 150), Convert.ToInt32(spawnPoints[index].Y), mapModel));
        }
        else
          spawnList.Add(new EnemyRanged(Convert.ToInt32(spawnPoints[index].X), Convert.ToInt32(spawnPoints[index].Y), mapModel));
      }

      for (int i = 0; i < rareRanged; i++)
      {
        index = rnd.Next(0, spawnPoints.Count - 1);
        if (index == 1)
        {
          spawnList.Add(new EnemyRareRanged(Convert.ToInt32(spawnPoints[index].X), Convert.ToInt32(spawnPoints[index].Y - 20), mapModel));
        }
        else if (index == 2)
        {
          spawnList.Add(new EnemyRareRanged(Convert.ToInt32(spawnPoints[index].X), Convert.ToInt32(spawnPoints[index].Y - 100), mapModel));
        }
        else if (index == 3)
        {
          spawnList.Add(new EnemyRareRanged(Convert.ToInt32(spawnPoints[index].X)+ 100, Convert.ToInt32(spawnPoints[index].Y - 40), mapModel));
        }
        else if (index == 4)
        {
          spawnList.Add(new EnemyRareRanged(Convert.ToInt32(spawnPoints[index].X - 150), Convert.ToInt32(spawnPoints[index].Y), mapModel));
        }
        else if (index == 6)
        {
          spawnList.Add(new EnemyRareRanged(Convert.ToInt32(spawnPoints[index].X - 150), Convert.ToInt32(spawnPoints[index].Y), mapModel));
        }
        else
          spawnList.Add(new EnemyRareRanged(Convert.ToInt32(spawnPoints[index].X), Convert.ToInt32(spawnPoints[index].Y), mapModel));
      }
      EnemySpawnList = spawnList;
    }

    #endregion Methods
  }
}
