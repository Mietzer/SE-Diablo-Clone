using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using System.Windows;
using System.Windows.Media;

namespace olbaid_mortel_7720.Object
{
  public class Handgun : Weapon
  {
    #region Properties

    #endregion Properties
    public Handgun(Player player) : base(player)
    {
      damage = 20;
      reloadtime = 2;
      category = ImageCategory.WEAPONS_PLAYER_HANDGUN;
      imageString = "handgun.png";
    }


    public void Shoot(Point p)
    {
      if (player.IsShooting == false)
      {
        double playerShootX = player.XCoord + player.Width / 2
           , playerShootY = player.YCoord + player.Height / 4 * 3;

        // Direction the bullet is going
        Vector vector = new Vector(p.X - playerShootX, p.Y - playerShootY);
        vector.Normalize();
        Brush bulletImage = new ImageBrush(RessourceImporter.Import(ImageCategory.BULLETS, "bullet.png"));
        Bullet bullet = new Bullet(2, 4, vector, bulletImage, shotname, damage);

        //Add to Player
        player.IsShooting = true;
        player.Bullets.Add(bullet);
        //Shot on Players left
        if (vector.X < 0)
        {
          playerShootX -= bullet.Rectangle.Width;

          //Above
          if (vector.Y < 0)
          {
            playerShootY -= bullet.Rectangle.Height;
          }
        }

        //Shot on Players right and above
        else if (vector.X > 0 && vector.Y < 0)
        {
          playerShootY -= bullet.Rectangle.Height;
        }

        // Add to Canvas
        bullet.Show(playercanvas, playerShootX, playerShootY);

        player.UpdateViewDirection(GetPlayerView(vector.X, vector.Y));
      }
      else
      {
        return null;
      }

    }

    public void Reload()
    {
      //TODO: Wait x Time 
      owner.IsShooting = false;
    }
  }
}
