using Necromancy.Empowerments;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class MagmastarPulse : PiccoloPulse
	{
        // weird curve projectile
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magmastar Pulse");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).empowermentType = EmpType.MaxMana;
        }

        public override Color GetColor()
        {
            return projectile.ai[0] == 0f ? new Color(1f, 0f, 1f) : new Color(1f, 0.5f, 0f);
        }

        public override float GetPerpendicularOffset()
        {
            float offset = (float)Math.Cos(MathHelper.ToRadians(projectile.timeLeft * 3)) * (projectile.ai[0] == 0f ? 1f : -1f);
            return offset * (360 - projectile.timeLeft) / 1440f * projectile.ai[1];
        }
    }
}