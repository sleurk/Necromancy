using Microsoft.Xna.Framework;

namespace Necromancy.Projectiles
{
    public class EnergyCyan : WandEnergy
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
            DisplayName.SetDefault("Cyan Energy");
        }

        protected override Color GetColor()
        {
            return new Color(0, .8f, .85f);
        }
    }
}