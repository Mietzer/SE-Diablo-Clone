using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Windows;

namespace olbaid_mortel_7720.Engine
{
  /// <summary>
  /// Implements a custom Pathfinding Algorithm.
  /// </summary>
  public class Pathfinder
  {
    private const int STANDARD_TILE_SIZE = 32;
    private const int MAX_ITERATIONS = 50;
    private const int MAX_DEPTH = 500;
    private const int MAX_PLAYER_DISTANCE = 350;
    private const int TOLERANCE = 16;
    private static List<Rect>? obstacles;

    /// <summary>
    /// Get an instance of the Pathfinder.
    /// </summary>
    /// <param name="obstacles">List of barriers</param>
    /// <returns>an instance</returns>
    public static Pathfinder Initialize(List<Barrier> obstacles)
    {
      return new Pathfinder(obstacles);
    }

    /// <summary>
    /// Private constructor.
    /// Don't use it.
    /// </summary>
    /// <param name="obstacles"></param>
    private Pathfinder(List<Barrier> obstacles)
    {
      if (obstacles != null)
        Pathfinder.obstacles = obstacles.ConvertAll(barrier => barrier.Hitbox);
    }

    /// <summary>
    /// Path finding method.
    /// </summary>
    /// <param name="start">Entity position</param>
    /// <param name="end">Target position</param>
    /// <param name="directionBefore">Direction the entity has now</param>
    /// <returns>the new distance vector with correct direction</returns>
    public Vector2 FindPath(Point start, Point end, Direction directionBefore)
    {
      double distance = Math.Sqrt((end.X - start.X) * (end.X - start.X) + (end.Y - start.Y) * (end.Y - start.Y));
      if (distance > MAX_PLAYER_DISTANCE) return DirectionAsVector(directionBefore);

      int iterations = 0;
      Point current = end;
      while (distance > STANDARD_TILE_SIZE * 1.5 && iterations <= MAX_ITERATIONS)
      {
        // Get the middle point between the start and the current point
        current = new Point((start.X + current.X) / 2, (start.Y + current.Y) / 2);

        if (obstacles != null && obstacles.Any(obstacle => obstacle.IntersectsWith(new Rect(current.X, current.Y, STANDARD_TILE_SIZE, STANDARD_TILE_SIZE))))
        {
          current = GetWalkableNeighbour(current, end, DirectionAsVector(directionBefore));
        }

        // calculate new distance
        double xDistance = current.X - start.X;
        double yDistance = current.Y - start.Y;
        distance = Math.Sqrt(xDistance * xDistance + yDistance * yDistance);
        iterations++;
      }

      // get the actual distance vector between the start and the current point extended to the target
      Vector2 distanceVector = new Vector2((float)(current.X - start.X), (float)(current.Y - start.Y));
      Vector2.Normalize(distanceVector);
      distanceVector *= (int)Math.Sqrt((end.X - start.X) * (end.X - start.X) + (end.Y - start.Y) * (end.Y - start.Y));
      return distanceVector;
    }

    /// <summary>
    /// Returns a vector with the given direction.
    /// </summary>
    /// <param name="directionBefore">direction enum</param>
    /// <returns>a vector</returns>
    private Vector2 DirectionAsVector(Direction directionBefore)
    {
      Vector2 direction = new Vector2();
      switch (directionBefore)
      {
        case Direction.Up:
          direction = new Vector2(0, -10);
          break;
        case Direction.Down:
          direction = new Vector2(0, 10);
          break;
        case Direction.Left:
          direction = new Vector2(-10, 0);
          break;
        case Direction.Right:
          direction = new Vector2(10, 0);
          break;
      }
      return direction;
    }

    /// <summary>
    /// Tests the neighbours of the given point and returns the first walkable one.
    /// </summary>
    /// <param name="node">the original point to test</param>
    /// <param name="target">the target point</param>
    /// <param name="before">the direction vector the entity had before</param>
    /// <returns>the new target point</returns>
    private Point GetWalkableNeighbour(Point node, Point target, Vector2 before)
    {
      Vector2 vector = new Vector2((float)(target.X - node.X), (float)(target.Y - node.Y));

      // test points and vertical vectors in both directions
      Point p1;
      Point p2;
      Vector2 v1;
      Vector2 v2;
      int distance = STANDARD_TILE_SIZE + TOLERANCE;
      if (Math.Abs(vector.Y) > Math.Abs(vector.X))
      {
        p1 = new Point(node.X + distance, node.Y);
        p2 = new Point(node.X - distance, node.Y);
        v1 = new Vector2(distance, 0);
        v2 = new Vector2(-distance, 0);
      }
      else
      {
        p1 = new Point(node.X, node.Y + distance);
        p2 = new Point(node.X, node.Y - distance);
        v1 = new Vector2(0, distance);
        v2 = new Vector2(0, -distance);
      }

      int dir1 = TraverseNextWalkableNeighbour(p1, v1, 0);
      int dir2 = TraverseNextWalkableNeighbour(p2, v2, 0);

      if (dir1 == -1 && dir2 == -1)
        return node;

      if (dir1 == dir2)
      {
        Vector2.Normalize(before);

        if (Vector2.Dot(before, v1) > Vector2.Dot(before, v2))
          return p1;
        else
          return p2;
      }
      if (dir1 > dir2 || dir1 == -1)
        return p2;
      return p1;
    }

    /// <summary>
    /// Tests a node if walkable and returns how many iterations needed.
    /// </summary>
    /// <param name="node">a point</param>
    /// <param name="direction">the direction to test further</param>
    /// <param name="iter">iterations needed until now</param>
    /// <returns>the iterations needed to find the next walkable point</returns>
    private int TraverseNextWalkableNeighbour(Point node, Vector2 direction, int iter)
    {
      if (iter > MAX_DEPTH) return -1;

      if (obstacles != null && obstacles.Any(obstacle => obstacle.IntersectsWith(new Rect(node.X, node.Y, STANDARD_TILE_SIZE, STANDARD_TILE_SIZE * 2))))
      {
        node = new Point(node.X + direction.X, node.Y + direction.Y);
        return TraverseNextWalkableNeighbour(node, direction, iter + 1);
      }

      return iter;
    }
  }
}