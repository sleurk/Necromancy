using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles.Minions
{
	public class GreenSwarmLeader : GreenSwarmFollower
	{
        // NYI - summons are weird
        // This is supposed to a small projectile that homes, and comes in a set
        // This one is the one in a set that uses a minion slot
        private bool justSummoned;

        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.minionSlots = 1;
            justSummoned = true;
        }

        public override void AI()
        {
            if (justSummoned)
            {
                justSummoned = false;

                for (int i = 0; i < 4; i++)
                {
                    Projectile proj = Projectile.NewProjectileDirect(projectile.Center, Main.rand.NextVector2CircularEdge(16f, 16f), mod.ProjectileType("GreenSwarmFollower"), projectile.damage, projectile.knockBack, projectile.owner, projectile.whoAmI);
                    proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom;
                }
            }
            base.AI();
        }
    }
}
