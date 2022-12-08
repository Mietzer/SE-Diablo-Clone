// TODO: Namespaces
namespace olbaid_mortel_7720.Object.Weapons
{
    public class Rifle : Weapon
    {
        #region Properties

        #endregion Properties
        public Rifle(Munition munition) : base(munition)
        {
            Damage = 30;
            reloadtime = 4;
            category = ImageCategory.WEAPONS_PLAYER_HANDGUN;
            imageString = "rifle.png";
        }
        #region Methods

        #endregion Methods
    }
}
