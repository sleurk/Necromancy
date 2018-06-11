using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles.Rituals
{
	public abstract class MeteorShower : Ritual
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("MeteorShower");
        }

        public override void Tick()
        {
            foreach (NPC npc in Necromancy.NearbyNPCs(projectile.Center, 600f))
            {
                if (npc != null && Main.rand.NextFloat() < 0.005f * power)
                {
                    int projectileType = 0;
                    switch (power)
                    {
                        case 1:
                            projectileType = mod.ProjectileType<Meteor1>();
                            break;
                        case 2:
                            projectileType = mod.ProjectileType<Meteor2>();
                            break;
                        default:
                            projectileType = mod.ProjectileType<Meteor3>();
                            break;
                    }
                    Projectile p = Main.projectile[Projectile.NewProjectile(npc.Center + new Vector2(Main.rand.NextFloat(-6f, 6f), -600f), new Vector2(Main.rand.NextFloat(-2f, 2f), 20f), projectileType, power * 15, 0f, projectile.owner)];
                }
            }
        }
    }
}