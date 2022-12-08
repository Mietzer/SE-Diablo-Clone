using olbaid_mortel_7720.MVVM.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Windows;

namespace olbaid_mortel_7720.Engine
{
  /// <summary>
  /// Implements a Pathfinding Algorithm similar to A*.
  /// <see cref="https://www.uni-kassel.de/eecs/index.php?eID=dumpFile&t=f&f=1581&token=8ba34c1c49d2f21b0d18c36a249dff0075988586"/>
  /// </summary>
  public class Pathfinder
  {
    private const int STANDARD_TILE_SIZE = 32;
    private static List<Rect>? obstacles;

    public static Pathfinder Initialize(List<Barrier> obstacles)
    {
      return new Pathfinder(obstacles);
    }

    private Pathfinder(List<Barrier> obstacles)
    {
      if (obstacles != null)
        Pathfinder.obstacles = obstacles.ConvertAll(barrier => barrier.Hitbox);
    }

    public Vector2 FindPath(Point start, Point end)
    {
      int MAX_ITERATIONS = 1000;
      
      PfNode startNode = new PfNode((int)start.X, (int)start.Y);
      PfNode targetNode = new PfNode((int)end.X, (int)end.Y);
      
      //return new Vector2((float)(end.X - start.X), (float)(end.Y - start.Y));
      
      startNode.SetDistance(targetNode.X, targetNode.Y);
      
      List<PfNode> activeNodes = new();
      activeNodes.Add(startNode);
      List<PfNode> visitedNodes = new();

      int iterations = 0;
      while(activeNodes.Any() && iterations <= MAX_ITERATIONS)
      {
        PfNode checkTile = activeNodes.OrderBy(x => x.CostDistance).First();

        if(checkTile.X == targetNode.X && checkTile.Y == targetNode.Y)
        {
          PfNode nextNode = activeNodes[1];
          return new Vector2(nextNode.X - startNode.X, nextNode.Y - startNode.Y);
        }

        visitedNodes.Add(checkTile);
        activeNodes.Remove(checkTile);

        List<PfNode> walkableNeighbours = GetWalkableNeighbours(checkTile, targetNode);

        foreach(PfNode neighbour in walkableNeighbours)
        {
          if (visitedNodes.Any(node => node.X == neighbour.X && node.Y == neighbour.Y))
            continue;

          if(activeNodes.Any(node => node.X == neighbour.X && node.Y == neighbour.Y))
          {
            PfNode existingNode = activeNodes.First(x => x.X == neighbour.X && x.Y == neighbour.Y);
            if(existingNode.CostDistance > checkTile.CostDistance)
            {
              activeNodes.Remove(existingNode);
              activeNodes.Add(neighbour);
            }
          }
          else
          {
            activeNodes.Add(neighbour);
          }
        }
        iterations++;
      }

      if (activeNodes.Count > 1)
        return new Vector2(activeNodes[1].X - startNode.X, activeNodes[1].Y - startNode.Y);
      return new Vector2(0, 0);
    }

    private List<PfNode> GetWalkableNeighbours(PfNode node, PfNode target)
    {
      const int costsBetweenNeighbours = 1;
      List<PfNode> possibleNeighbours = new List<PfNode>(new[]
      {
        new PfNode(node.X + STANDARD_TILE_SIZE, node.Y, node, node.Cost + costsBetweenNeighbours),
        new PfNode(node.X - STANDARD_TILE_SIZE, node.Y, node, node.Cost + costsBetweenNeighbours),
        new PfNode(node.X, node.Y + STANDARD_TILE_SIZE, node, node.Cost + costsBetweenNeighbours),
        new PfNode(node.X, node.Y - STANDARD_TILE_SIZE, node, node.Cost + costsBetweenNeighbours)
      });
      possibleNeighbours.ForEach(neighbour => neighbour.SetDistance(target.X, target.Y));

      if (obstacles == null) return possibleNeighbours;
      return possibleNeighbours
        .Where(neighbour => neighbour.X >= GlobalVariables.MinX && neighbour.X <= GlobalVariables.MaxX)
        .Where(neighbour => neighbour.Y >= GlobalVariables.MinY && neighbour.Y <= GlobalVariables.MaxY)
        .Where(neighbour => !obstacles.Any(obstacle => obstacle.IntersectsWith(new Rect(neighbour.X, neighbour.Y, STANDARD_TILE_SIZE, STANDARD_TILE_SIZE))))
        .ToList();
    }
  }
}