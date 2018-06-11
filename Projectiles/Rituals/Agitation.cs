using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles.Rituals
{
	public abstract class Agitation : Ritual
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Agitation");
        }

        public override void Tick()
        {
            foreach (Player player in Necromancy.NearbyAllies(projectile.Center, null, 600f))
            {
                player.AddBuff(mod.BuffType<Buffs.Agitated>(), 2);
                player.GetModPlayer<NecromancyPlayer>().agitation = power;
            }
            foreach (NPC npc in Necromancy.NearbyNPCs(projectile.Center, 600f))
            {
                npc.AddBuff(mod.BuffType<Buffs.Agitated>(), 2);
            }
        }
    }
}