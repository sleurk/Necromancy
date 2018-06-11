using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
    public class SoulHarvest1 : SoulHarvest
    {
        public override void Config()
        {
            power = 1;
            dustType = 63;
        }

        public override bool PreAI()
        {
            Lighting.AddLight(projectile.Center, 0.4f, 0.4f, 0.4f);
            return true;
        }
    }
}