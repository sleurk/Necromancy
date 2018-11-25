using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace Necromancy.Projectiles
{
	public class GluttonyRing : ModProjectile
	{
        // large projectile, black-hole enemy attraction effect
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gluttony Ring");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 200;
			projectile.height = 200;
			projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.hide = true;
			projectile.penetrate = -1;
            projectile.netImportant = true;
			projectile.timeLeft = 300;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
        }

		public override void AI()
		{
            foreach (NPC npc in Necromancy.NearbyNPCs(projectile.Center, 200f, true))
            {
                Necromancy.DoCustomKnockback(npc, (projectile.Center - npc.Center) / 128f);
            }
            for (int i = 0; i < 6; i++)
            {
                Dust d = Dust.NewDustPerfect(projectile.Center + Main.rand.NextVector2CircularEdge(100, 100), 64);
                d.noGravity = true;
            }
            if (Main.player[projectile.owner].dead)
            {
                projectile.Kill();
            }
        }
    }
}