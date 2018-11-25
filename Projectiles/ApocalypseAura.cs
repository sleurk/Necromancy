using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class ApocalypseAura : ModProjectile
	{
        // large projectile around a player with apocalypse armor, for set bonus effects & visuals
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Apocalypse Aura");
        }

        public override void SetDefaults()
        {
            projectile.width = 408;
			projectile.height = 408;
            projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 1;
            projectile.hide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
        }

        public override void AI()
		{
            Player player = Main.player[projectile.owner];
            foreach (Player p in Necromancy.NearbyAllies(player.Center, player, 204f, false))
            {
                p.AddBuff(mod.BuffType<Buffs.Apocalypse>(), 2);
            }
            foreach (NPC npc in Necromancy.NearbyNPCs(player.Center, 204f))
            {
                npc.AddBuff(mod.BuffType<Buffs.Apocalypse>(), 2);
            }

            for (int i = 0; i < 3; i++)
            {
                Dust d = Dust.NewDustPerfect(Main.rand.NextVector2CircularEdge(202, 202) + projectile.Center, 57, Vector2.Zero);
                d.noGravity = true;
                d.scale = Main.rand.NextFloat(0.5f, 1.5f);
                d.velocity = (player.Center - d.position).RotatedBy(MathHelper.ToRadians(30f))* 0.05f * Main.rand.NextFloat() + player.velocity;
                d.noLight = true;
            }
        }
    }
}