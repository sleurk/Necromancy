using Microsoft.Xna.Framework;

namespace Necromancy.Projectiles
{
    public class EnergyOrange : WandEnergy
    {
        // basic projectile

        protected override int Pierce
        {
            get { return 1; }
        }

        protected override int Heal
        {
            get { return 1; }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orange Energy");
        }

        protected override Color GetColor()
        {
            return Color.Orange;
        }
    }
}