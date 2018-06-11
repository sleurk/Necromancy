using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
    public class Enchantment2 : Enchantment
    {
        public override void Config()
        {
            dustType = 27;
            power = 2;
        }

        public override bool PreAI()
        {
            Lighting.AddLight(projectile.Center, 0.5f, 0f, 0.7f);
            return true;
        }
    }
}