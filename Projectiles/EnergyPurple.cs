using Microsoft.Xna.Framework;

namespace Necromancy.Projectiles
{
    public class EnergyPurple : WandEnergy
    {
        // basic projectile

        protected override int Pierce
        {
            get { return 1; }
        }

        protected override int Heal
        {
            get { return 2; }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Purple Energy");
        }

        protected override Color GetColor()
        {
            return new Color(1f, 0f, 1f);
        }
    }
}