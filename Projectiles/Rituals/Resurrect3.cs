using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
	public class Resurrect3 : Resurrect
    {
        public override void Config()
        {
            dustType = 135;
            power = 3;
        }

        public override bool PreAI()
        {
            Lighting.AddLight(projectile.Center, 0f, 0.6f, 0.8f);
            return true;
        }
    }
}