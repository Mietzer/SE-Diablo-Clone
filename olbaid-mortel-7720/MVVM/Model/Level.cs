using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.MVVM.Model.Enemies;
using olbaid_mortel_7720.MVVM.Utils;
using olbaid_mortel_7720.MVVM.Viewmodel;
using System;
using System.Collections.Generic;

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
      for (int i = 0; i < melees; i++)
      {
        spawnList.Add(new EnemyMelee(rnd.Next(0, GlobalVariables.MaxX), rnd.Next(0, GlobalVariables.MaxY - 50), mapModel));
      }
      for (int i = 0; i < rareMelees; i++)
      {
        spawnList.Add(new EnemyRareMelee(rnd.Next(0, GlobalVariables.MaxX), rnd.Next(0, GlobalVariables.MaxY - 50), mapModel));
      }
      for (int i = 0; i < ranged; i++)
      {
        spawnList.Add(new EnemyRanged(rnd.Next(0, GlobalVariables.MaxX), rnd.Next(0, GlobalVariables.MaxY - 50), mapModel));
      }
      for (int i = 0; i < rareRanged; i++)
      {
        spawnList.Add(new EnemyRareRanged(rnd.Next(0, GlobalVariables.MaxX), rnd.Next(0, GlobalVariables.MaxY - 50), mapModel));
      }
      EnemySpawnList = spawnList;
    }

    #endregion Methods
  }
}
