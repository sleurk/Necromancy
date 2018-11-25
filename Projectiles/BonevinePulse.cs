using Necromancy.Empowerments;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class BonevinePulse : PiccoloPulse
	{
        // weird curvy projectile
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bonevine Pulse");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).empowermentType = EmpType.MaxLife;
        }

        public override Color GetColor()
        {
            return projectile.ai[0] == 0f ? new Color(0f, 1f, 0f) : new Color(1f, 1f, 1f);
        }

        public override float GetPerpendicularOffset()
        {
            float offset = (float)Math.Sin(MathHelper.ToRadians(projectile.timeLeft * 3)) * (projectile.ai[0] == 0f ? 1f : -1f);
            return offset * (360 - projectile.timeLeft) / 1440f * projectile.ai[1];
        }
    }
}