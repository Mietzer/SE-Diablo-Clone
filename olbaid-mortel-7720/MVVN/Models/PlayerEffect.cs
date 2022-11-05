namespace olbaid_mortel_7720.MVVN.Models
{
  /// <summary>
  ///  List containing all the possible effects a player can have
  /// </summary>
  public enum PlayerEffect
  {
    // Player is not affected by anything
    None,
    
    // Player is affected by the slow effect
    Poisoned,
    
    // Player is burning e.g. because of lava
    Burning,
    
    // Player is healing e.g. because of being affected by a healthpack
    Healing,
    
    // Player is affected by a protection
    Protected
  }
}