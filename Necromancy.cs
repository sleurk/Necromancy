using Terraria.ModLoader;
using Terraria;
using System.IO;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria.Localization;
using Terraria.DataStructures;
using Terraria.UI;
using ReLogic.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Shaders;
using Necromancy.Items;
using Necromancy.NPCs;
using Necromancy.Projectiles;
using Necromancy.Empowerments;
using Necromancy.Items.Armor;

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

        public override void Load()
        {
            Config.Load();
        }

        // packet handling - messages sent to other clients to resolve multiplayer issues
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
                        DrainLife(player, life);
                        if (Main.netMode == 2)
                        {
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
                            d.shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
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
                                d.shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
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
                        bool impure = reader.ReadBoolean();
                        HealPlayer(player, life, impure);

                        if (Main.netMode == 2)
                        {
                            ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                            packet.Write((byte)NecromancyMessageType.HealPlayer);
                            packet.Write(player.whoAmI);
                            packet.Write(life);
                            packet.Write(impure);
                            packet.Send();
                        }
                        break;
                    }

                case NecromancyMessageType.ApplySymphonicEmps:
                    {
                        Player player = Main.player[reader.ReadInt32()];
                        EmpType empType = (EmpType)reader.ReadByte();
                        int buffTime = reader.ReadInt32();
                        bool symphAcc = reader.ReadBoolean();
                        
                        foreach (Player p in NearbyAllies(player.Center, player, 1200f, true))
                        {
                            NecromancyPlayer mP = p.GetModPlayer<NecromancyPlayer>();
                            mP.AddEmpowerment(empType, buffTime);
                            if (symphAcc) mP.RefreshEmpowerments();
                        }
                        
                        if (Main.netMode == 2)
                        {
                            ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                            packet.Write((byte)NecromancyMessageType.ApplySymphonicEmps);
                            packet.Write(player.whoAmI);
                            packet.Write((byte)empType);
                            packet.Write(buffTime);
                            packet.Write(symphAcc);
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
                        mPlayer.wraithTime = Math.Max(mPlayer.wraithTime - 1, 0);
                        if (mPlayer.wraithTime > 0)
                            mPlayer.wraithInterrupt = true;
                        mPlayer.wraithTime = 0;
                        if (Main.netMode == 2)
                        {
                            ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                            packet.Write((byte)NecromancyMessageType.Hurt);
                            packet.Write(player.whoAmI);
                            packet.Send();
                        }
                        break;
                    }

                case NecromancyMessageType.KillProj:
                    {
                        Projectile proj = Main.projectile[reader.ReadInt32()];
                        proj.Kill();
                        if (Main.netMode == 2)
                        {
                            ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                            packet.Write((byte)NecromancyMessageType.KillProj);
                            packet.Write(proj.whoAmI);
                            packet.Send();
                        }
                        break;
                    }

                case NecromancyMessageType.KillPlayerDrain:
                    {
                        Player player = Main.player[reader.ReadInt32()];
                        string deathMessage = reader.ReadString();
                        int life = reader.ReadInt32();
                        player.statLife = 1;
                        player.Hurt(PlayerDeathReason.ByCustomReason(deathMessage), life, -player.direction);
                        if (Main.netMode == 2)
                        {
                            ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                            packet.Write((byte)NecromancyMessageType.KillPlayerDrain);
                            packet.Write(player.whoAmI);
                            packet.Write(deathMessage);
                            packet.Write(life);
                            packet.Send();
                        }
                        break;
                    }

                case NecromancyMessageType.RemoveEmp:
                    {
                        Player player = Main.player[reader.ReadInt32()];
                        EmpType empType = (EmpType)reader.ReadByte();
                        NecromancyPlayer mP = player.GetModPlayer<NecromancyPlayer>();
                        mP.activeEmpowerments.Remove(empType);
                        if (Main.netMode == 2)
                        {
                            ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                            packet.Write((byte)NecromancyMessageType.RemoveEmp);
                            packet.Write(player.whoAmI);
                            packet.Write((byte)empType);
                            packet.Send();
                        }
                        break;
                    }

                case NecromancyMessageType.ProcVampireLocket:
                    {
                        Player player = Main.player[reader.ReadInt32()];
                        NecromancyPlayer mP = player.GetModPlayer<NecromancyPlayer>();
                        mP.vampireTime = 60;
                        player.AddBuff(mod.BuffType("VampiricExhaustion"), 10800);
                        if (Main.netMode == 2)
                        {
                            ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                            packet.Write((byte)NecromancyMessageType.ProcVampireLocket);
                            packet.Write(player.whoAmI);
                            packet.Send();
                        }
                        break;
                    }

                case NecromancyMessageType.SyncEmp:
                    {
                        EmpType empType = (EmpType)reader.ReadByte();
                        int time = reader.ReadInt32();
                        byte owner = reader.ReadByte();
                        int maxTime = reader.ReadInt32();
                        bool flag = reader.ReadBoolean();
                        int power = reader.ReadInt32();
                        NecromancyPlayer mP = Main.player[owner].GetModPlayer<NecromancyPlayer>();
                        mP.activeEmpowerments.Add(empType, Empowerment.NewEmp(empType, time, owner, maxTime, flag, power));
                        break;
                    }

                default:
                    ErrorLogger.Log("Necromancy: Unknown Message type: " + msgType);
                    break;
            }
        }

        // adds alternate recipes for all potions at the blood alchemy table
        public override void PostAddRecipes()
        {
            Mod mod = ModLoader.GetMod("Necromancy");
            foreach (Recipe recipe in Main.recipe)
            {
                int index = Array.IndexOf(recipe.requiredTile, TileID.Bottles);
                if (index > -1)
                {
                    BloodAlchemyRecipe newRecipe = new BloodAlchemyRecipe(mod)
                    {
                        requiredItem = recipe.requiredItem
                    };
                    newRecipe.AddTile(mod, "BloodAlchemyStation");
                    newRecipe.createItem = recipe.createItem;
                    newRecipe.AddRecipe();
                }
            }
        }

        // nearest player within [maxDistance] pixels of [pos], ally to [ally] if [ally] is not null
        public static Player NearestPlayer(Vector2 pos, float maxDistance = -1, Player ally = null)
        {
            float shortestDistanceSq = -1;
            Player closestPlayer = null;
            foreach (Player p in Main.player)
            {
                if (p != null && p.active && ally == null || IsAlly(p, ally))
                {
                    Vector2 toPlayer = p.Center - pos;
                    if ((maxDistance == -1 || toPlayer.LengthSquared() < maxDistance * maxDistance) && (shortestDistanceSq == -1 || shortestDistanceSq > toPlayer.LengthSquared()))
                    {
                        closestPlayer = p;
                        shortestDistanceSq = toPlayer.LengthSquared();
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
                        if (toPlayer.LengthSquared() < maxDistance * maxDistance)
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
        public static NPC[] NearbyNPCs(Vector2 pos, float maxDistance = -1, bool hostile = false, bool lineOfSight = true)
        {
            List<NPC> npcs = new List<NPC>();
            foreach (NPC npc in Main.npc)
            {
                if (npc != null && npc.active && npc.type != NPCID.TargetDummy && (!hostile || !npc.friendly))
                {
                    Vector2 toPlayer = npc.Center - pos;
                    if (toPlayer.LengthSquared() < maxDistance * maxDistance
                        && (!lineOfSight || Collision.CanHit(pos, 1, 1, npc.Center, 1, 1)))
                    {
                        npcs.Add(npc);
                    }
                }
            }
            return npcs.ToArray();
        }

        // lowest player within [maxDistance] pixels of [pos] that is [ally]'s ally
        public static Player LowestAlly(Vector2 pos, Player ally, float maxDistance = -1)
        {
            float lowestHealth = -1;
            Player lowestPlayer = null;
            foreach (Player p in Main.player)
            {
                if (p != null && p != ally && p.active && IsAlly(p, ally))
                {
                    Vector2 toPlayer = p.Center - pos;
                    if (p.statLife < p.statLifeMax2 && (maxDistance == -1 || toPlayer.LengthSquared() < maxDistance * maxDistance) && (lowestHealth == -1 || lowestHealth > p.statLife))
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

        // nearest NPC to [pos] within a max distance of [distance]
        // [lightning] checks if the npc was already hit by a lightning attack this frame so as not to boucne back to the same one
        // [lineOfSight] checks if there is a clear line of sight from [pos] to the npc
        public static NPC NearestNPC(Vector2 pos, float distance = -1f, bool lightning = false, bool lineOfSight = true)
        {
            float shortestDistanceSq = -1;
            NPC closestNPC = null;
            for (int i = 0; i < 200; i++)
            {
                NPC npc = Main.npc[i];
                Vector2 toNPC = npc.Center - pos;
                if (npc.active && !npc.immortal && !npc.dontTakeDamage
                    && npc.type != NPCID.TargetDummy
                    && !npc.friendly && npc.type != NPCID.TargetDummy
                    && (!lightning || !npc.GetGlobalNPC<NecromancyNPC>().lightningHit)
                    && (shortestDistanceSq == -1 || shortestDistanceSq > toNPC.LengthSquared())
                    && (toNPC.LengthSquared() < distance * distance || distance == -1f)
                    && (!lineOfSight || Collision.CanHit(pos, 1, 1, npc.Center, 1, 1)))
                {
                    closestNPC = npc;
                    shortestDistanceSq = toNPC.LengthSquared();
                }
            }
            return closestNPC;
        }

        public static Projectile[] NearbyProjectiles(Vector2 pos, int type = -1, float distance = -1f, bool lineOfSight = true, bool lightningFlag = false)
        {
            List<Projectile> projList = new List<Projectile>();
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active
                 && (type == -1 || proj.type == type)
                 && (distance == -1f || Vector2.DistanceSquared(proj.Center, pos) < distance * distance)
                 && (!lineOfSight || Collision.CanHit(pos, 1, 1, proj.Center, 1, 1))
                 && (!lightningFlag || !proj.GetGlobalProjectile<NecromancyGlobalProjectile>().lightningShootFlag))
                {
                    if (projList.Count == 0)
                    {
                        projList.Add(proj);
                        continue;
                    }
                    for (int j = 0; j < projList.Count; j++)
                    {
                        if (Vector2.DistanceSquared(proj.Center, pos) < Vector2.DistanceSquared(projList[j].Center, pos))
                        {
                            projList.Add(proj);
                            break;
                        }
                    }
                }
            }
            return projList.ToArray();
        }

        // finds if the given item is a weapon, will be used for enchantment ritual
        public static bool IsWeapon(Item item)
        {
            return (item != null && item.active && item.damage > 0);
        }

        // finds the life cost of the given player using the given item
        public static int GetCost(Player player, Item item)
        {
            if (item.GetGlobalItem<NecromancyGlobalItem>().ranged) return item.GetGlobalItem<NecromancyGlobalItem>().lifeCost;
            int lc = item.GetGlobalItem<NecromancyGlobalItem>().lifeCost + player.GetModPlayer<NecromancyPlayer>().lifeCostModifier;
            if (player.GetModPlayer<NecromancyPlayer>().demonHelm && item.GetGlobalItem<NecromancyGlobalItem>().magic)
            {
                lc /= 2;
            }
            if (player.GetModPlayer<NecromancyPlayer>().magicCostAcc && item.GetGlobalItem<NecromancyGlobalItem>().magic)
            {
                lc = (int)(lc * (float)player.statLife / player.statLifeMax2);
            }
            return Math.Max(lc, 1);
        }

        // finds the necrotic radiant healing power of the item when used by the player
        public static int GetHealPower(Player player, Item item)
        {
            int heal = item.GetGlobalItem<NecromancyGlobalItem>().healPower;
            if (player.GetModPlayer<NecromancyPlayer>().radiantAcc)
            {
                heal += item.damage / 5 - 2;
            }
            heal = Math.Max(heal, 1);
            return heal;
        }

        // finds the necrotic radiant healing power of the projectile
        public static int GetHealPower(Projectile proj)
        {
            int heal = proj.GetGlobalProjectile<NecromancyGlobalProjectile>().healPower;
            if (Main.player[proj.owner].GetModPlayer<NecromancyPlayer>().radiantAcc)
            {
                heal += proj.damage / 10 - 2;
            }
            heal = Math.Max(heal, 1);
            return heal;
        }

        // applies the life cost of the item to the player
        public static void DoLifeCost(Player player, Item item)
        {
            if (Main.netMode == 2) Console.WriteLine("Server: DoLifeCost");
            if (item.GetGlobalItem<NecromancyGlobalItem>().lifeCost > 0
             && (player.altFunctionUse == 2 || !item.GetGlobalItem<NecromancyGlobalItem>().rClickCostOnly))
            {
                int lc = GetCost(player, item);

                if (player.GetModPlayer<NecromancyPlayer>().manaAcc && !item.GetGlobalItem<NecromancyGlobalItem>().ranged)
                {
                    DoLifeCostMana(player, lc);
                }
                else
                {
                    BroadcastDrainLife(player, lc);
                }
            }
        }

        // applies the mana cost to the player if using Astral Contract equivalent to [lifeCost] health
        public static void DoLifeCostMana(Player player, int lifeCost)
        {
            for (int i = lifeCost; i > 0; i--)
            {
                if (player.statMana > 15)
                {
                    if (Main.netMode == 0) player.statMana -= 15;

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
                    BroadcastDrainLife(player, i);
                    return;
                }
            }
            player.manaRegenDelay = (int)Math.Ceiling((0.7f * ((1 - player.statMana / 1f / player.statManaMax2) * 240 + 45)));
        }

        // counts the number of active necrotic summons to multiply cost
        public static int CountSummons(Player player)
        {
            if (player.GetModPlayer<NecromancyPlayer>().summonAcc) return 3;
            int summons = 0;
            foreach (Projectile proj in Main.projectile)
            {
                if (proj != null && proj.active && proj.owner == player.whoAmI && proj.GetGlobalProjectile<NecromancyGlobalProjectile>().summonCost > 0)
                {
                    summons++;
                }
            }
            return summons;
        }

        // drains life of a player and sends a packet to other clients to do the same
        public static void BroadcastDrainLife(Player player, int life)
        {
            if (Main.netMode == 0) DrainLife(player, life);

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

        // drains life of a player on this client
        // progress will change the number to a percent representing it, for health catalysts
        public static void DrainLife(Player player, int life, float progress = -1f, Color progressColor = default(Color))
        {
            NecromancyPlayer mP = player.GetModPlayer<NecromancyPlayer>();
            if (mP.vampireTime > 0 && life > 0)
            {
                BroadcastHealPlayer(player, life);
                return;
            }

            if (progress == -1f)
            {
                Color color = mP.RecentHeals > life ? new Color(1f, 0f, 0.5f) : new Color(1f, 0f, 0f);
                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), color, life, true, true);
            }
            else if (progress > -1f)
            {
                Color color = progressColor;
                Vector3 hsl = Main.rgbToHsl(color);
                hsl.Y = progress;
                if (progress < 1f) hsl.Y *= 0.7f;
                color = Main.hslToRgb(hsl.X, hsl.Y, hsl.Z);
                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), color, (int)Math.Round(progress * 100f) + "%", progress == 1f, false);
            }

            mP.RecentHeals = Math.Max(0, mP.RecentHeals - life);

            player.statLife -= life;

            if (player.statLife <= 0 && player.whoAmI == Main.myPlayer)
            {
                string damageSource;
                switch (Main.rand.Next(3))
                {
                    case 0:
                        damageSource = player.name + " ran out of blood.";
                        break;
                    case 1:
                        damageSource = player.name + " couldn't handle the power.";
                        break;
                    default:
                        damageSource = player.name + " didn't watch their health bar.";
                        break;
                }
                if (Main.netMode == 0)
                {
                    player.statLife = 1;
                    player.Hurt(PlayerDeathReason.ByCustomReason(damageSource), life, -player.direction);
                }
                else
                {
                    ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                    packet.Write((byte)NecromancyMessageType.KillPlayerDrain);
                    packet.Write(player.whoAmI);
                    packet.Write(damageSource);
                    packet.Write(life);
                    packet.Send();
                }
            }
        }

        // heals the player and broadcasts to other clients to do the same
        // impure determines if the health is from life-steal (impure life takes more damage if there is no pure life in front of it)
        // sender is for debug
        public static void BroadcastHealPlayer(Player player, int life, bool impure = false, string sender = "no sender")
        {
            if (life > 0)
            {
                if (Main.netMode == 0) HealPlayer(player, life, impure);

                if (Main.netMode == 1)
                {
                    ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                    packet.Write((byte)NecromancyMessageType.HealPlayer);
                    packet.Write(player.whoAmI);
                    packet.Write(life);
                    packet.Write(impure);
                    packet.Send();
                }
            }
        }

        // heals the player on this client
        // impure determines if the health is from life-steal (impure life takes more damage if there is no pure life in front of it)
        public static void HealPlayer(Player player, int life, bool impure)
        {
            if (life <= 0)
                return;
            NecromancyPlayer mP = player.GetModPlayer<NecromancyPlayer>();
            if (player.statLife + life > player.statLifeMax2) life -= player.statLife + life - player.statLifeMax2;
            if (impure)
            {
                Color color = new Color(1f, 0f, 1f);
                mP.RecentHeals += life;
                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), color, life, false, true);
            }
            else
            {
                player.HealEffect(life);
            }
            player.statLife = Math.Min(player.statLife + life, player.statLifeMax2);
        }

        // applies knockback in a custom direction to the target
        public static void DoCustomKnockback(NPC target, Vector2 addVel)
        {
            if (target.type != NPCID.TargetDummy) target.velocity += addVel * target.knockBackResist;
        }

        // for drawing empowerments on a player and for drawing pink hearts over the health bar for impure health
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            for (int i = 0; i < layers.Count; i++)
            {
                if (layers[i].Name.Contains("Resource Bars"))
                {
                    layers.Insert(i + 1, new LegacyGameInterfaceLayer(
                        "Necromancy: Impure Health Bar",
                        delegate
                        {
                            DrawImpureHearts(Main.spriteBatch);
                            return true;
                        },
                        InterfaceScaleType.UI)
                    );
                    layers.Insert(i + 2, new LegacyGameInterfaceLayer(
                        "Necromancy: Symphonic Empowerments",
                        delegate
                        {
                            DrawSymphEmpowerments(Main.spriteBatch);
                            return true;
                        },
                        InterfaceScaleType.UI)
                    );
                }
            }
        }

        // draws ui for current empowerments
        private void DrawSymphEmpowerments(SpriteBatch spriteBatch)
        {
            foreach (Player player in Main.player)
            {
                if ((player.whoAmI == Main.myPlayer || Config.OtherEmpowerments) && player != null && player.active && !player.dead)
                {
                    NecromancyPlayer mP = player.GetModPlayer<NecromancyPlayer>();
                    float[] xOffsets = mP.GetXOffsets();
                    Vector2[] offsets = mP.GetOffsets();

                    int index = 0;
                    foreach (EmpType type in mP.activeEmpowerments.Keys)
                    {
                        Vector2 screenPos = new Vector2((int)(player.Center.X), (int)player.Center.Y) + offsets[index] - Main.screenPosition;

                        if (Config.MoveEmpowerments && player.whoAmI == Main.myPlayer)
                        {
                            screenPos.Y = Main.screenHeight - 40f;
                        }

                        Texture2D tex = mP.activeEmpowerments[type].Texture;
                        float scale = 1f;

                        Vector2 texCenter = screenPos;
                        Color color = new Color(0.9f, 0.9f, 0.9f);
                        Rectangle? sourceRectangle = new Rectangle?(new Rectangle(30 * mP.activeEmpowerments[type].Frame.X, 30 * mP.activeEmpowerments[type].Frame.Y, 30, 30));
                        spriteBatch.Draw(tex, texCenter, sourceRectangle, color, 0f, new Vector2(15f, 15f), scale, SpriteEffects.None, 0f);
                        
                        texCenter += new Vector2(0f, 30f);
                        spriteBatch.DrawString(Main.fontMouseText, mP.activeEmpowerments[type].Text, texCenter, mP.activeEmpowerments[type].power == 200 ? mP.activeEmpowerments[type].TextColor : color,
                            0f, Main.fontMouseText.MeasureString(mP.activeEmpowerments[type].Text) / 2f, scale, SpriteEffects.None, 0f);

                        tex = ModLoader.GetTexture("Necromancy/Empowerments/ProgressBarFrame");
                        texCenter += new Vector2(0f, -60f);
                        sourceRectangle = new Rectangle?(new Rectangle(0, 0, 38, 18));
                        spriteBatch.Draw(tex, texCenter, sourceRectangle, color, 0f, new Vector2(19f, 9f), scale, SpriteEffects.None, 0f);

                        tex = ModLoader.GetTexture("Necromancy/Empowerments/ProgressBar");
                        sourceRectangle = new Rectangle?(new Rectangle(0, 0, (int)Math.Ceiling((mP.activeEmpowerments[type].time / 1f / mP.activeEmpowerments[type].maxTime) * 26), 6));
                        spriteBatch.Draw(tex, texCenter, sourceRectangle, mP.activeEmpowerments[type].Color, 0f, new Vector2(13f, 3f), scale, SpriteEffects.None, 0f);
                        index++;
                    }
                }
            }
        }

        // draws purple hearts for impure health
        private void DrawImpureHearts(SpriteBatch spriteBatch)
        {
            Player player = Main.LocalPlayer;
            if (player.dead) return;
            int redHearts = Math.Min(player.statLifeMax / 20, 20);
            if (redHearts == 0) return;
            int heartSize = player.statLifeMax2 / redHearts;

            float pureHearts = player.statLife / 1f / heartSize;
            float impureHearts = Math.Min(20f, player.GetModPlayer<NecromancyPlayer>().RecentHeals / 1f / heartSize);
            for (int i = 0; i < impureHearts; i++)
            {
                int screenAnchorX = Main.screenWidth - 800;
                float scale = 1f;
                float alpha = 1f;

                if (player.statLife < (i + 1) * 20f) // if this heart is not full
                {
                    float heartPortion = player.statLife / 20f - i;
                    alpha = heartPortion;
                    scale = heartPortion + 3 / 4f;
                }
                
                alpha = 1 - Math.Min(Math.Min(1f, impureHearts - i), pureHearts - i);
                Vector2 corner = new Vector2((500 + 26 * i + screenAnchorX + Main.heartTexture.Width / 2), 32f + (Main.heartTexture.Height - Main.heartTexture.Height * scale) / 2f + (Main.heartTexture.Height / 2));
                if (i > 9) corner += new Vector2(-260f, 26f);
                Main.spriteBatch.Draw(ModLoader.GetTexture("Necromancy/ImpureHeart"), corner, new Rectangle?(new Rectangle(0, 0, Main.heartTexture.Width, Main.heartTexture.Height)), new Color(1f, 1f, 1f, alpha), 0f, new Vector2((Main.heartTexture.Width / 2), (Main.heartTexture.Height / 2)), scale, SpriteEffects.None, 0f);
            }
        }
    }

    enum NecromancyMessageType : byte
    {
        DrainLife,
        DrainMana,
        WraithForm,
        HealPlayer,
        ApplySymphonicEmps,
        PreHurt,
        Hurt,
        KillProj,
        KillPlayerDrain,
        RemoveEmp,
        ProcVampireLocket,
        SyncEmp
    }
}
