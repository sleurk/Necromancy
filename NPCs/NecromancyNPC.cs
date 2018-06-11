using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace Necromancy.NPCs
{
	public class NecromancyNPC : GlobalNPC
	{
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public bool pestilence = false;
        public Player pestilencePlayer = null;
        public bool goreyBoneHit = false;
        public bool lightningHit = false;
        public int agitated = 0;
        public float extraDmg = 1f;
        public bool wounded = false;
        public int brick = 0;
        public int soulHarvest = 0;
        public Vector2 oldCenter = Vector2.Zero;
        public Vector2 currentCenter = Vector2.Zero;

        // Used for npc deaths - npc loot is in Items/NPCDrops.cs
        public override void NPCLoot(NPC npc)
        {
            if (npc.GetGlobalNPC<NecromancyNPC>().soulHarvest > 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Soul" + soulHarvest));
            }
        }

        public override void ResetEffects(NPC npc)
        {
            oldCenter = currentCenter;
            currentCenter = npc.Center;
            agitated = 0;
            lightningHit = false;
            wounded = false;
            extraDmg = 1f;
            if (brick > 0 && brick < 30 && Math.Abs((npc.velocity - npc.oldVelocity).X) > 2f)
            {
                brick = 0;
                npc.StrikeNPC((int)(Math.Abs((npc.velocity - npc.oldVelocity).X) * 200), 0f, -npc.direction, true);
            }
            brick = Math.Max(brick - 1, 0);
            soulHarvest = 0;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (pestilence)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 60;
            }
            if (wounded)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 15;
            }
        }

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (agitated > 0)
            {
                extraDmg -= 0.1f * agitated;
                knockback = 0f;
            }
            damage = (int)(damage * extraDmg);
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (agitated > 0)
            {
                extraDmg -= 0.1f * agitated;
                knockback = 0f;
            }
            damage = (int)(damage * extraDmg);
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (pestilence)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 54, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.dust[dust].scale *= 0.25f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.3f, 0f, 0);
            }
            if (wounded)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 46, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.dust[dust].scale *= 0.25f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.3f, 0f, 0);
            }
        }

        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (player.GetModPlayer<NecromancyPlayer>().agitation > 0)
            {
                spawnRate = (int)(spawnRate / (player.GetModPlayer<NecromancyPlayer>().agitation * 4f + 4f));
                maxSpawns = (int)(maxSpawns * (player.GetModPlayer<NecromancyPlayer>().agitation * 4f + 4f));
            }
        }
    }
}
