using Microsoft.Xna.Framework;
using Terraria;

namespace Necromancy.Projectiles
{
    public class EnergyRainbow : WandEnergy
    {
        // basic projectile

        protected override int Pierce
        {
            get { return 1; }
        }

        protected override int Heal
        {
            get { return 6; }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rainbow Energy");
        }

        protected override Color GetColor()
        {
            return Main.hslToRgb(projectile.timeLeft % 60 / 60f, 1f, 0.5f);
        }
    }
}