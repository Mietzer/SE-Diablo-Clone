using System;

namespace olbaid_mortel_7720.Engine
{
  public class PfNode
  {
    #region Propeerties
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Cost { get; set; }
    public int Distance { get; private set; }
    public int CostDistance => Cost + Distance;
    public PfNode? Parent { get; set; }
    #endregion Propeerties
    
    #region Methods
    public PfNode(int x, int y) : this(x, y, null, 0) { }
    
    public PfNode(int x, int y, PfNode parent, int cost)
    {
      X = x;
      Y = y;
      Cost = cost;
      Parent = parent;
    }
    
    public void SetDistance(int targetX, int targetY)
    {
      this.Distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
    }
    #endregion Methods
  }
}