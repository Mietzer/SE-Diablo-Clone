using System;

namespace olbaid_mortel_7720.Helper
{
  public class InitialEventArgs : EventArgs
  {
    public bool IsInitial { get; set; }

    public InitialEventArgs(bool isInitial)
    {
      IsInitial = isInitial;
    }

    public InitialEventArgs()
    {
      IsInitial = true;
    }
  }
}