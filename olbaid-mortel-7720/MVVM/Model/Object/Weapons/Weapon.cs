namespace olbaid_mortel_7720.Object
{
  /// <summary>
  ///  Class for all types of weapons for players as well as for opponents
  /// </summary>
  abstract class Weapon
  {

    int Shootingspeed;
    int Reloadtime;
    Projectile Bullet;

    //optional Munitons Munition 
    public string PathtoGraphics { get; set; }

    public Weapon() { }

    public void Shot() { }

    public void Reload() { }
  }
}
