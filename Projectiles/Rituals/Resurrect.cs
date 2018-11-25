using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles.Rituals
{
    public abstract class Resurrect : Ritual
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Resurrect");
        }

        protected override int TileType
        {
            get { return mod.TileType("ResurrectAltar"); }
        }

        public override void Tick()
        {
            foreach (Player player in Necromancy.NearbyAllies(projectile.Center, null, 600f, false, false))
            {
                player.AddBuff(mod.BuffType<Buffs.Resurrect>(), 2);
                player.GetModPlayer<NecromancyPlayer>().resurrect = (int)Power + 1;

                if (player.dead)
                {
                    Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustType);
                    d.noGravity = true;
                    d.velocity = new Vector2(0f, -10f);
                }
            }
        }
    }
}