using Terraria.ModLoader;
using Terraria;
using System.IO;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria.Localization;
using Terraria.DataStructures;

namespace Necromancy
{
	public class Necromancy : Mod
	{
		public Necromancy()
		{
			Properties = new ModProperties()
			{
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true
			};
		}

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            Mod mod = ModLoader.GetMod("Necromancy");

            NecromancyMessageType msgType = (NecromancyMessageType)reader.ReadByte();
            switch (msgType)
            {
                case NecromancyMessageType.DrainLife:
                    {
                        Player player = Main.player[reader.ReadInt32()];
                        int life = reader.ReadInt32();
                        string source = reader.ReadString();
                        DrainLifeMulti(player, life);
                        if (Main.netMode == 2)
                        {
                            Console.WriteLine("Received packet from " + source);
                            Console.WriteLine("Server packet: Draining life from " + player.name + " (" + player.whoAmI + ")");
                            ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                            packet.Write((byte)NecromancyMessageType.DrainLife);
                            packet.Write(player.whoAmI);
                            packet.Write(life);
                            packet.Write(source + "*");
                            packet.Send();
                        }
                        break;
                    }

                case NecromancyMessageType.DrainMana:
                    {
                        Player player = Main.player[reader.ReadInt32()];
                        player.statMana = Math.Max(player.statMana - 10, 0);
                        if (Main.netMode == 2)
                        {
                            ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                            packet.Write((byte)NecromancyMessageType.DrainMana);
                            packet.Write(player.whoAmI);
                            packet.Send();
                        }
                        break;
                    }

                case NecromancyMessageType.WraithForm:
                    {
                        Player player = Main.player[reader.ReadInt32()];
                        int wraithTime = reader.ReadInt32();
                        Vector2 vel = reader.ReadVector2();
                        player.velocity = vel;
                        player.AddBuff(BuffID.Invisibility, 2, true);
                        for (int i = 0; i < 10; i++)
                        {
                            Dust d = Dust.NewDustPerfect(player.Center + vel, 54, Main.rand.NextVector2Circular(4f, 4f));
                            d.scale = Main.rand.NextFloat(1.5f, 2.5f);
                            d.noGravity = true;
                        }

                        if (wraithTime == 1 || player.GetModPlayer<NecromancyPlayer>().wraithInterrupt)
                        {
                            wraithTime = 0;
                            Main.PlaySound(SoundID.Item20, player.Center);
                            for (int i = 0; i < 20; i++)
                            {
                                Vector2 dv = Main.rand.NextVector2Circular(8f, 8f) + player.velocity;
                                Dust d = Dust.NewDustDirect(player.position, player.width, player.height, 54, dv.X, dv.Y);
                                d.scale = Main.rand.NextFloat(1f, 2f);
                                d.noGravity = true;
                            }
                        }

                        if (Main.netMode == 2)
                        {
                            ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                            packet.Write((byte)NecromancyMessageType.WraithForm);
                            packet.Write(player.whoAmI);
                            packet.Write(wraithTime);
                            packet.WriteVector2(vel);
                            packet.Send();
                        }
                        break;
                    }

                case NecromancyMessageType.HealPlayer:
                    {
                        Player player = Main.player[reader.ReadInt32()];
                        int life = reader.ReadInt32();
                        HealPlayerMulti(player, life);
                        if (Main.netMode == 2)
                        {
                            ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                            packet.Write((byte)NecromancyMessageType.HealPlayer);
                            packet.Write(player.whoAmI);
                            packet.Write(life);
                            packet.Send();
                        }
                        break;
                    }

                case NecromancyMessageType.ApplySymphonicBuffs:
                    {
                        Player player = Main.player[reader.ReadInt32()];
                        int buffType = reader.ReadInt32();
                        int buffTime = reader.ReadInt32();
                        player.AddBuff(buffType, buffTime, false);
                        if (Main.netMode == 2)
                        {
                            ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                            packet.Write((byte)NecromancyMessageType.ApplySymphonicBuffs);
                            packet.Write(player.whoAmI);
                            packet.Write(buffType);
                            packet.Write(buffTime);
                            packet.Send();
                        }
                        break;
                    }

                case NecromancyMessageType.PreHurt:
                    {
                        Player player = Main.player[reader.ReadInt32()];
                        Vector2 teleportTo = reader.ReadVector2();
                        player.Teleport(teleportTo, 1);
                        if (Main.netMode == 2)
                        {
                            ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                            packet.Write((byte)NecromancyMessageType.PreHurt);
                            packet.Write(player.whoAmI);
                            packet.WriteVector2(teleportTo);
                            packet.Send();
                        }
                        break;
                    }

                case NecromancyMessageType.Hurt:
                    {
                        Player player = Main.player[reader.ReadInt32()];
                        NecromancyPlayer mPlayer = player.GetModPlayer<NecromancyPlayer>();
                        mPlayer.wraithInterrupt = true;
                        if (Main.netMode == 2)
                        {
                            ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                            packet.Write((byte)NecromancyMessageType.Hurt);
                            packet.Write(player.whoAmI);
                            packet.Send();
                        }
                        break;
                    }

                case NecromancyMessageType.Enchant:
                    {
                        Vector2 pos = reader.ReadVector2();
                        int type = reader.ReadInt32();
                        Player player = Main.player[reader.ReadInt32()];
                        Item weapon = Main.item[reader.ReadInt32()];
                        Tiles.TEEnchantingAltar tileEntity = (Tiles.TEEnchantingAltar)TileEntity.ByID[reader.ReadInt32()];
                        Projectile projectile = Projectile.NewProjectileDirect(pos, Vector2.Zero, type, 0, 0f, player.whoAmI);
                        if (projectile.modProjectile is Projectiles.Rituals.Enchantment)
                        {
                            Projectiles.Rituals.Enchantment e = (Projectiles.Rituals.Enchantment)projectile.modProjectile;
                            e.targetItem = weapon;
                            e.targetPlayer = Main.player[weapon.owner];
                        }
                        projectile.Center = pos;
                        tileEntity.activeRitual = projectile;
                        Main.PlaySound(SoundID.Item46, pos);
                        if (Main.netMode == 2)
                        {
                            ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                            packet.Write((byte)NecromancyMessageType.Enchant);
                            packet.WriteVector2(pos);
                            packet.Write(type);
                            packet.Write(player.whoAmI);
                            packet.Write(weapon.whoAmI);
                            packet.Write(tileEntity.ID);
                            packet.Send();
                        }
                        break;
                    }

                case NecromancyMessageType.RitualCancel:
                    {
                        Tiles.TEEnchantingAltar tileEntity = (Tiles.TEEnchantingAltar)TileEntity.ByID[reader.ReadInt32()];

                        tileEntity.activeRitual.Kill();
                        tileEntity.activeRitual.netUpdate = true;
                        tileEntity.activeRitual = null;

                        if (Main.netMode == 2)
                        {
                            ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                            packet.Write((byte)NecromancyMessageType.RitualCancel);
                            packet.Write(tileEntity.ID);
                            packet.Send();
                        }
                        break;
                    }

                case NecromancyMessageType.SyncDebug:
                    {
                        String message = reader.ReadString();
                        Main.NewText("Packet received: " + message);
                        Console.WriteLine("Packet received: " + message);
                        break;
                    }

                default:
                    ErrorLogger.Log("Necromancy: Unknown Message type: " + msgType);
                    break;
            }
        }

        public static Player NearestPlayer(Vector2 pos, float maxDistance=-1, Player ally=null)
        {
            float shortestDistance = -1;
            Player closestPlayer = null;
            foreach (Player p in Main.player)
            {
                if (p != null && p.active && ally == null || IsAlly(p, ally))
                {
                    Vector2 toPlayer = p.Center - pos;
                    if ((maxDistance == -1 || toPlayer.Length() < maxDistance) && (shortestDistance == -1 || shortestDistance > toPlayer.Length()))
                    {
                        closestPlayer = p;
                        shortestDistance = toPlayer.Length();
                    }
                }
            }
            return closestPlayer;
        }

        // pos - center
        // ally - target player (to select allies of the player), null counts everyone
        // maxDistance - radius
        // countself - whether to include 'ally' in the array
        // alive - whether or not the players must be alive to be included
        public static Player[] NearbyAllies(Vector2 pos, Player ally = null, float maxDistance = -1, bool countSelf = false, bool alive = true)
        {
            List<Player> players = new List<Player>();
            foreach (Player p in Main.player)
            {
                if (p != null && p.active)
                {
                    if (p.active && (!p.dead || !alive) && (ally == null || (countSelf && (p == ally)) || IsAlly(p, ally)))
                    {
                        Vector2 toPlayer = p.Center - pos;
                        if (toPlayer.Length() < maxDistance)
                        {
                            players.Add(p);
                        }
                    }
                }

            }
            return players.ToArray();
        }

        // pos - center
        // maxDistance - radius
        // hostile - whether or not to only target hostile enemies
        public static NPC[] NearbyNPCs(Vector2 pos, float maxDistance = -1, bool hostile = false)
        {
            List<NPC> npcs = new List<NPC>();
            foreach (NPC npc in Main.npc)
            {
                if (npc != null && npc.active && npc.type != NPCID.TargetDummy && (!hostile || !npc.friendly))
                {
                    Vector2 toPlayer = npc.Center - pos;
                    if (toPlayer.Length() < maxDistance)
                    {
                        npcs.Add(npc);
                    }
                }
            }
            return npcs.ToArray();
        }

        public static Player LowestAlly(Vector2 pos, Player ally, float maxDistance = -1)
        {
            float lowestHealth = -1;
            Player lowestPlayer = null;
            foreach (Player p in Main.player)
            {
                if (p != null && p != ally && p.active && IsAlly(p, ally))
                {
                    Vector2 toPlayer = p.Center - pos;
                    if (p.statLife < p.statLifeMax2 && (maxDistance == -1 || toPlayer.Length() < maxDistance) && (lowestHealth == -1 || lowestHealth > p.statLife))
                    {
                        lowestPlayer = p;
                        lowestHealth = p.statLife;
                    }
                }

            }
            return lowestPlayer;
        }

        // If p1 is another player with the same team as p2 (or if p1 has no team)
        public static bool IsAlly(Player p1, Player p2)
        {
            return p1 != p2 && (p1.team == 0 || p1.team == p2.team);
        }

        public static NPC NearestNPC(Vector2 pos, float distance = -1f, bool lightning = false)
        {
            float shortestDistance = -1;
            NPC closestNPC = null;
            for (int i = 0; i < 200; i++)
            {
                NPC npc = Main.npc[i];
                Vector2 toNPC = npc.Center - pos;
                if (npc.active && !npc.immortal && !npc.dontTakeDamage 
                    && npc.type != NPCID.TargetDummy
                    && !npc.friendly && npc.type != NPCID.TargetDummy 
                    && (!lightning || !npc.GetGlobalNPC<NPCs.NecromancyNPC>().lightningHit) 
                    && (shortestDistance == -1 || shortestDistance > toNPC.Length())
                    && (toNPC.Length() < distance || distance == -1f))
                {
                    closestNPC = npc;
                    shortestDistance = toNPC.Length();
                }
            }
            return closestNPC;
        }

        public static Item NearestWeapon(Vector2 pos, float distance = -1f)
        {
            float shortestDistance = -1;
            Item closestItem = null;
            for (int i = 0; i < Main.item.Length; i++)
            {
                Item item = Main.item[i];
                if (item != null && item.active)
                {
                    Vector2 toItem = item.Center - pos;
                    if (item.damage > 0 
                        && (shortestDistance == -1 || toItem.Length() < shortestDistance)
                        && (distance == -1f) || toItem.Length() < distance)
                    {
                        closestItem = item;
                        shortestDistance = toItem.Length();
                    }
                }
            }
            return closestItem;
        }

        public static int GetCost(Player player, Item item, bool forTooltip = false)
        {
            int lc = item.GetGlobalItem<Items.NecromancyGlobalItem>().lifeCost;
            if (player.GetModPlayer<NecromancyPlayer>().demonHelm && item.GetGlobalItem<Items.NecromancyGlobalItem>().magic)
            {
                lc /= 2;
            }
            if (forTooltip) lc *= item.GetGlobalItem<Items.NecromancyGlobalItem>().numShoot;
            return lc;
        }

        public static int GetHealPower(Player player, Item item)
        {
            int heal = item.GetGlobalItem<Items.NecromancyGlobalItem>().healPower;
            if (player.GetModPlayer<NecromancyPlayer>().radiantAcc)
            {
                heal += item.damage / 10 - 2;
            }
            heal = Math.Max(heal, 1);
            return heal;
        }

        public static int GetHealPower(Projectile proj)
        {
            int heal = proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>().healPower;
            if (Main.player[proj.owner].GetModPlayer<NecromancyPlayer>().radiantAcc)
            {
                heal += proj.damage / 10 - 2;
            }
            heal = Math.Max(heal, 1);
            return heal;
        }

        public static void DoLifeCost(Player player, Item item)
        {
            if (Main.netMode == 2) Console.WriteLine("Server: DoLifeCost");
            if (item.GetGlobalItem<Items.NecromancyGlobalItem>().baseLifeCost > 0)
            {
                int lc = GetCost(player, item);

                lc = Math.Max(lc, 1);
                if (player.GetModPlayer<NecromancyPlayer>().manaAcc && !item.GetGlobalItem<Items.NecromancyGlobalItem>().ranged)
                {
                    DoLifeCostMana(player, lc);
                }
                else
                {
                    DrainLife(player, lc);
                }
            }
        }

        public static void DoLifeCostMana(Player player, int lifeCost)
        {
            for (int i = lifeCost; i > 0; i--)
            {
                if (player.statMana > 10)
                {
                    if (Main.netMode == 0) player.statMana -= 10;

                    if (Main.netMode == 1)
                    {
                        ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                        packet.Write((byte)NecromancyMessageType.DrainMana);
                        packet.Write(player.whoAmI);
                        packet.Send();
                    }
                }
                else
                {
                    player.statMana = 0;
                    DrainLife(player, i);
                    return;
                }
            }
        }

        public static int CountSummons(Player player)
        {
            if (player.GetModPlayer<NecromancyPlayer>().summonAcc) return 5;
            int summons = 0;
            foreach (Projectile proj in Main.projectile)
            {
                if (proj != null && proj.active && proj.owner == player.whoAmI && proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>().summonCost > 0)
                {
                    summons++;
                }
            }
            return summons;
        }

        public static void DrainLife(Player player, int life)
        {
            if (Main.netMode == 0) DrainLifeMulti(player, life);
            
            if (Main.netMode == 1)
            {
                ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                packet.Write((byte)NecromancyMessageType.DrainLife);
                packet.Write(player.whoAmI);
                packet.Write(life);
                packet.Write("Necromancy.DrainLife");
                packet.Send();
            }
        }

        public static void DrainLifeMulti(Player player, int life)
        {
            player.statLife -= life;

            if (player.statLife <= 0)
            {
                PlayerDeathReason damageSource;
                switch (Main.rand.Next(3))
                {
                    case 0:
                        damageSource = PlayerDeathReason.ByCustomReason(player.name + " ran out of blood.");
                        break;
                    case 1:
                        damageSource = PlayerDeathReason.ByCustomReason(player.name + " couldn't handle the power.");
                        break;
                    default:
                        damageSource = PlayerDeathReason.ByCustomReason(player.name + " didn't watch their health bar.");
                        break;
                }
                player.KillMe(damageSource, life, -player.direction);
            }
        }

        public static void HealPlayer(Player player, int life, string sender = "no sender")
        {
            if (Main.netMode == 0) HealPlayerMulti(player, life);

            if (Main.netMode == 1)
            {
                ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                packet.Write((byte)NecromancyMessageType.HealPlayer);
                packet.Write(player.whoAmI);
                packet.Write(life);
                packet.Send();
            }
        }

        public static void HealPlayerMulti(Player player, int life)
        {
            player.statLife = Math.Min(player.statLife + life, player.statLifeMax2);
            player.HealEffect(life, true);
        }
    }

    enum NecromancyMessageType : byte
    {
        DrainLife,
        DrainMana,
        WraithForm,
        HealPlayer,
        ApplySymphonicBuffs,
        PreHurt,
        Hurt,
        Enchant,
        RitualCancel,
        SyncDebug
    }
}
