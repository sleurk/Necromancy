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

        protected override int TileType
        {
            get { return mod.TileType("MeteorShowerAltar"); }
        }

        public override void Tick()
        {
            int enemies = 0;
            foreach (NPC npc in Necromancy.NearbyNPCs(projectile.Center, 600f))
            {
                if (npc != null && Main.rand.NextFloat() < 0.005f * (int)Power)
                {
                    enemies++;
                    int projectileType = 0;
                    switch (Power)
                    {
                        case RitualPower.RitualBone:
                            projectileType = mod.ProjectileType<Meteor1>();
                            break;
                        case RitualPower.RitualShadow:
                            projectileType = mod.ProjectileType<Meteor2>();
                            break;
                        case RitualPower.RitualSoul:
                            projectileType = mod.ProjectileType<Meteor3>();
                            break;
                    }
                    Projectile p = Main.projectile[Projectile.NewProjectile(npc.Center + new Vector2(Main.rand.NextFloat(-6f, 6f), -600f), new Vector2(Main.rand.NextFloat(-2f, 2f), 20f), projectileType, (int)Power * 15, 0f, projectile.owner)];
                    if (enemies > 10) return;
                }
            }
        }
    }
}