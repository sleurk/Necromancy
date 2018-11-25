using Microsoft.Xna.Framework;

namespace Necromancy.Projectiles
{
    public class EnergyRed : WandEnergy
    {
        // basic projectile

        protected override int Pierce
        {
            get { return 1; }
        }

        protected override int Heal
        {
            get { return 3; }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red Energy");
        }

        protected override Color GetColor()
        {
            return Color.Red;
        }
    }
}