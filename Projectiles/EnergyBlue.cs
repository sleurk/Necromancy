using Microsoft.Xna.Framework;

namespace Necromancy.Projectiles
{
	public class EnergyBlue : WandEnergy
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
            DisplayName.SetDefault("Blue Energy");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
        }

        protected override Color GetColor()
        {
            return Color.Blue;
        }
    }
}