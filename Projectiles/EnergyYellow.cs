using Microsoft.Xna.Framework;

namespace Necromancy.Projectiles
{
    public class EnergyYellow : WandEnergy
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
            DisplayName.SetDefault("Yellow Energy");
        }

        protected override Color GetColor()
        {
            return Color.Yellow;
        }
    }
}