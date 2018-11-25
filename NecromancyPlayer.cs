using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework.Graphics;
using Necromancy.Items;
using Necromancy.Projectiles;
using Necromancy.NPCs;
using Necromancy.Empowerments;
using Terraria.UI;

namespace Necromancy
{
	public class NecromancyPlayer : ModPlayer
    {
        public int lifeCostModifier;

        public int painSummonPower;

        public int allCritBonus;
        public float allDamageMultiplier;
        public float allMeleeDamageMultiplier;
        public float allRangedDamageMultiplier;
        public float allMagicDamageMultiplier;
        public float allSummonDamageMultiplier;

        public float necroticDamage;
        public int necroticCrit;

        public int lifeStealModifier;
        public float lifeStealMult;

        public int regenModifier;
        public float regenMult;

        public float nMeleeMult;
        public float nRangedMult;
        public float nMagicMult;
        public float nSummonMult;
        public float nRadiantMult;
        public float nSymphonicMult;

        public bool destabilized;

        public bool meleeHitAcc;
        public int timeWithoutHit = 0;
        public bool rangedHitsAcc;
        public float rangedHitsNum = 0;
        public bool magicCostAcc;
        public bool symphAcc;
        public bool throwAcc;
        public bool summonAcc;
        public bool radiantAcc;

        public bool celestialAccessory;

        public bool fullHealthRespawn;

        public int empowermentMaxTime;

        public int wingUse = 0;

        public List<int> buffedWeapon = new List<int>();
        public int agitation = 0;
        public int resurrect = 0;
        public int taunt = 0;
        public int recoverTimer = 0;

        public bool downOld;
        public bool upOld;

        public bool wraith;
        public int wraithTime = 0;
        public bool wraithInterrupt;

        public bool manaAcc;
        
        public int critEmp;

        public float universalLifeSteal;

        public bool bloodcloth;
        public bool boneplate;
        public bool cursedSet;
        public bool ichorSet;
        public bool hallowedSkull;
        public bool necrodancer;
        public bool midnightHelm;
        public bool midnightMask;
        public bool wormholeSet;
        public bool demonHelm;
        public bool demonCowl;

        public float bloodBoost;
        public float glowBoost;
        public float fireBoost;
        public float iceBoost;
        public float gooBoost;
        public float shockBoost;

        public float mlDmgFromEmp;
        public float rgDmgFromEmp;
        public float mgDmgFromEmp;
        public float smDmgFromEmp;
        public float thDmgFromEmp;

        public float impureBloodDamageTakenMult;

        public int immortalEmp = 0;

        public float attackSpeed;

        public float dodgeChance;

        public int bonusPotionHeal;

        public float ammoConsumeChance;

        public bool vampireLocket;
        public int vampireTime = 0;

        public SortedDictionary<EmpType, Empowerment> activeEmpowerments = new SortedDictionary<EmpType, Empowerment>(new EmpowermentComparer());

        private int recentHeals = 0;

        public int healTimer = 0;
        public int healCDLength;

        public int totalSummonCost;

        public bool sporeSummon;
        public bool iceSummon;
        public bool gooSummon;

        public override bool CloneNewInstances
        {
            get { return false; }
        }

        // current impure health
        public int RecentHeals
        {
            get { return recentHeals; }
            set
            {
                recentHeals = value;
                if (recentHeals < 0) recentHeals = 0;
                else if (recentHeals > player.statLife) recentHeals = player.statLife;
            }
        }

        public override void ResetEffects()
		{
            necroticDamage = 0f;
            necroticCrit = 0;

            lifeCostModifier = 0;

            allCritBonus = 0;
            allDamageMultiplier = 1f;
            allMeleeDamageMultiplier = 1f;
            allRangedDamageMultiplier = 1f;
            allMagicDamageMultiplier = 1f;
            allSummonDamageMultiplier = 1f;

            regenModifier = 0;
            regenMult = 1f;

            lifeStealModifier = 0;
            lifeStealMult = 1f;

            nMeleeMult = 1f;
            nRangedMult = 1f;
            nMagicMult = 1f;
            nSummonMult = 1f;
            nRadiantMult = 1f;
            nSymphonicMult = 1f;

            destabilized = false;

            meleeHitAcc = false;
            if (timeWithoutHit < 300)
            {
                timeWithoutHit++;
            }

            rangedHitsAcc = false;
            magicCostAcc = false;
            symphAcc = false;
            summonAcc = false;
            throwAcc = false;
            radiantAcc = false;
            symphAcc = false;

            rangedHitsNum = Math.Max(0, rangedHitsNum - 1);

            celestialAccessory = false;

            fullHealthRespawn = false;

            empowermentMaxTime = 600;

            wingUse = Math.Max(wingUse - 1, 0);

            agitation = Math.Max(agitation - 1, 0);
            resurrect = Math.Max(resurrect - 1, 0);
            taunt = Math.Max(taunt - 1, 0);

            player.aggro += 401 * taunt;

            recoverTimer = Math.Max(recoverTimer - 1, 0);

            wraithTime = Math.Max(wraithTime - 1, 0);
            wraith = false;
            wraithInterrupt = false;
            
            critEmp = 0;

            universalLifeSteal = 0f;

            manaAcc = false;
            
            immortalEmp = Math.Max(0, immortalEmp - 1);

            vampireLocket = false;
            vampireTime = Math.Max(0, vampireTime - 1);

            bloodcloth = false;
            boneplate = false;
            cursedSet = false;
            ichorSet = false;
            hallowedSkull = false;
            necrodancer = false;
            midnightHelm = false;
            midnightMask = false;
            wormholeSet = false;
            demonHelm = false;
            demonCowl = false;

            bloodBoost = 1f;
            glowBoost = 1f;
            fireBoost = 1f;
            iceBoost = 1f;
            gooBoost = 1.1f;
            shockBoost = 1f;

            mlDmgFromEmp = 0f;
            rgDmgFromEmp = 0f;
            mgDmgFromEmp = 0f;
            smDmgFromEmp = 0f;
            thDmgFromEmp = 0f;
            
            impureBloodDamageTakenMult = 3f;

            attackSpeed = 1f;

            dodgeChance = 0f;

            bonusPotionHeal = 0;

            ammoConsumeChance = 1f;

            healTimer -= 1;
            if (healTimer <= 0)
            {
                healTimer = healCDLength;
                recentHeals = Math.Min(player.statLife, Math.Max(0, recentHeals - 1));
            }
            healCDLength = 4;
            totalSummonCost = 0;

            sporeSummon = false;
            iceSummon = false;
            gooSummon = false;

            UpdateEmpowerments();
        }

        // for syncing empowerments in multiplayer
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            foreach (EmpType empType in activeEmpowerments.Keys)
            {
                Empowerment emp = activeEmpowerments[empType];
                ModPacket packet = mod.GetPacket();
                packet.Write((byte)NecromancyMessageType.SyncEmp);
                packet.Write((byte)empType);
                packet.Write(emp.time);
                packet.Write((byte)emp.owner);
                packet.Write(emp.flag);
                packet.Write(emp.maxTime);
                packet.Write(emp.power);
                packet.Send(toWho, fromWho);
            }
        }

        // removes an empowerment and tells other clients to do the same
        private void BroadcastRemoveEmp(EmpType empType)
        {
            if (Main.netMode == 0) activeEmpowerments.Remove(empType);
            else
            {
                ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                packet.Write((byte)NecromancyMessageType.RemoveEmp);
                packet.Write(player.whoAmI);
                packet.Write((byte)empType);
                packet.Send();
            }
        }

        // ticks each empowerment's timer and applies the effects
        private void UpdateEmpowerments()
        {
            List<EmpType> keys = new List<EmpType>();
            foreach (EmpType type in activeEmpowerments.Keys) keys.Add(type);
            foreach (EmpType type in keys)
            {
                // roundabout foreach to avoid errors for modifying collection
                activeEmpowerments[type].time = Math.Min(empowermentMaxTime, activeEmpowerments[type].time - 1);
                if (activeEmpowerments[type].time < 0)
                {
                    activeEmpowerments.Remove(type);
                    // BroadcastRemoveEmp(type);
                }
            }
            foreach (EmpType type in activeEmpowerments.Keys)
            {
                if (activeEmpowerments[type].flag)
                {
                    activeEmpowerments[type].flag = false;
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), activeEmpowerments[type].TextColor, "Empowerment - " + activeEmpowerments[type].EmpDisplayName);
                }
                activeEmpowerments[type].Behavior();
            }
        }

        // adds an empowerment of the given type for the given time to the player
        public void AddEmpowerment(EmpType empType, int maxTime)
        {
            if (activeEmpowerments.ContainsKey(empType))
            {
                activeEmpowerments[empType].power = Math.Min(activeEmpowerments[empType].power + 1, 200);
                activeEmpowerments[empType].time = maxTime;
                activeEmpowerments[empType].maxTime = maxTime;
                if (symphAcc) RefreshEmpowerments();
            }
            else
            {
                activeEmpowerments.Add(empType, Empowerment.NewEmp(empType, maxTime, player.whoAmI));
            }
        }

        // finds the x-coordinate of where to draw each empowerment
        public float[] GetXOffsets()
        {
            float spacing = 8f;
            float[] offsets = new float[activeEmpowerments.Count];
            for (int i = 0; i < activeEmpowerments.Count; i++)
            {
                offsets[i] = (30f + spacing) * (i - activeEmpowerments.Count / 2f + 0.5f);
            }
            return offsets;
        }

        // finds where to draw each empowerment
        public Vector2[] GetOffsets()
        {
            float spacing = 16f;
            Vector2[] offsets = new Vector2[activeEmpowerments.Count];
            for (int i = 0; i < activeEmpowerments.Count; i++)
            {
                offsets[i] = new Vector2((30f + spacing) * (i - activeEmpowerments.Count / 2f + 0.5f), 96f);
            }
            return offsets;
        }

        // maxes the time for each active empowerment
        public void RefreshEmpowerments()
        {
            foreach (Empowerment empo in activeEmpowerments.Values)
            {
                empo.time = empowermentMaxTime;
                empo.maxTime = empowermentMaxTime;
            }
        }
        
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (immortalEmp > 0)
            {
                // prevents death if immortality empowerment is active
                immortalEmp = 0;
                if (Main.netMode == 0) activeEmpowerments.Remove(EmpType.Immortality);
                else
                {
                    BroadcastRemoveEmp(EmpType.Immortality);
                }
                Necromancy.BroadcastHealPlayer(player, player.statLifeMax2 / 4);
                player.immuneTime = 60;
                return false;
            }

            if (vampireLocket && !player.HasBuff(mod.BuffType("VampiricExhaustion")))
            {
                // prevents death and enables healing on hit for one second if vampire locket is active
                if (Main.netMode == 0)
                {
                    vampireTime = 60;
                    player.AddBuff(mod.BuffType("VampiricExhaustion"), 10800);
                }
                else
                {
                    ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                    packet.Write((byte)NecromancyMessageType.ProcVampireLocket);
                    packet.Write(player.whoAmI);
                    packet.Send();
                }
                Necromancy.BroadcastHealPlayer(player, player.statLifeMax2 / 4);
                return false;
            }
            return true;
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            // reduces respawn time if Resurrect ritual is active nearby
            player.respawnTimer -= (int)(player.respawnTimer * 0.15f * resurrect);

            RecentHeals = 0;
            activeEmpowerments.Clear();
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            // wraith cloak movement logic
            if (wraith && UpButton(triggersSet) && !upOld && UpDoubleTapTimer() > 0)
            {
                player.controlMount = true;
                player.canRocket = false;
                player.canCarpet = false;
                player.channel = false;
                Main.PlaySound(SoundID.Item20, player.Center);
                for (int i = 0; i < 20; i++)
                {
                    Vector2 dv = Main.rand.NextVector2Circular(8f, 8f);
                    Dust d = Dust.NewDustDirect(player.position, player.width, player.height, 54, dv.X, dv.Y);
                    d.scale = Main.rand.NextFloat(1.5f, 2.5f);
                    d.noGravity = true;
                    d.shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
                }
                Necromancy.BroadcastDrainLife(player, 20);
                wraithTime = 2;
            }
            if (wraithTime > 0 && UpButton(triggersSet))
            {

                Necromancy.BroadcastDrainLife(player, 1);
                Vector2 vel = 0.1f * (Main.MouseWorld - player.Center);
                player.direction = vel.X.CompareTo(0);
                if (vel.Length() > 16f)
                {
                    vel = 16f * vel.SafeNormalize(Vector2.Zero);
                }
                player.velocity = vel - (Vector2.UnitY * player.gravity);
                if (player.maxFallSpeed < player.velocity.Y)
                {
                    player.maxFallSpeed = player.velocity.Y;
                }
                player.AddBuff(BuffID.Invisibility, 2, false);

                Vector2 dVel = vel;
                if (Collision.SolidCollision(player.position + vel, player.width, player.height))
                {
                    dVel = Vector2.Zero;
                }
                for (int i = 0; i < 10; i++)
                {
                    Dust d = Dust.NewDustPerfect(player.Center + dVel, 54, Main.rand.NextVector2Circular(4f, 4f) + dVel * Main.rand.NextFloat());
                    d.scale = Main.rand.NextFloat(1.5f, 2.5f);
                    d.noGravity = true;
                    d.shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
                }

                wraithTime = 2;
                if (Main.netMode == 1)
                {
                    ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                    packet.Write((byte)NecromancyMessageType.WraithForm);
                    packet.Write(player.whoAmI);
                    packet.Write(wraithTime);
                    packet.WriteVector2(player.velocity);
                    packet.Send();
                }
            }
            if (wraithInterrupt || (wraithTime > 0 && !UpButton(triggersSet)))
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
            upOld = UpButton(triggersSet);

            // wormhole armor set bonus active effect
            if (wormholeSet && !downOld && DownButton(triggersSet) && DownTapTimer() > 0)
            {
                if (player.armor[0] != null && player.armor[0].modItem is Items.Armor.WormholeHelmet)
                {
                    Items.Armor.WormholeHelmet head = (Items.Armor.WormholeHelmet)player.armor[0].modItem;
                    head.SwitchMode();
                }
            }
            downOld = DownButton(triggersSet);
        }

        public override void GetWeaponDamage(Item item, ref int damage)
        {
            if (item.IsAir) return;

            float totalDmgMult = 1f;
            
            // encahntment ritual (NYI)
            if (buffedWeapon.Contains(item.type))
            {
                totalDmgMult += 0.05f * item.GetGlobalItem<NecromancyGlobalItem>().enchanted;
            }
            else
            {
                item.GetGlobalItem<NecromancyGlobalItem>().enchanted = 0;
            }

            buffedWeapon = new List<int>();
            
            if (item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic)
            {
                // since necrotic weapons are technically magic weapons in order to still go through the same damage system, damage/crit bonuses need to be done manually
                // this section is for giving necrotic weapons the universal damage buffs, but not the magic-specific damage buffs
                // vanilla stores damage multipliers individually per weapon class and has no universal value to use so this is technically guesswork

                totalDmgMult = totalDmgMult + 1 - player.magicDamage;

                // figuring out the player's universal crit modifiers by finding the minimum crit bonus across all vanilla types
                // this can technically be exploited to get bonus necrotic damage without a universal buff but it would be a pain to do anyway
                // plus, there's not really a better way to do this for extra classes as far as I know
                float universalDmgBonusGuess = Math.Min(player.meleeDamage - mlDmgFromEmp, Math.Min(player.rangedDamage - rgDmgFromEmp, Math.Min(player.magicDamage - mgDmgFromEmp, 
                    Math.Min(player.minionDamage - smDmgFromEmp, player.thrownDamage - thDmgFromEmp)))) - 1f;
                
                totalDmgMult += universalDmgBonusGuess;

                float modMult = necroticDamage;
                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).melee) modMult += nMeleeMult - 1;
                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).ranged) modMult += nRangedMult - 1;
                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).magic) modMult += nMagicMult - 1;
                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).summon) modMult += nSummonMult - 1;
                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).radiant) modMult += nRadiantMult - 1;
                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).symphonic) modMult += nSymphonicMult - 1;
                totalDmgMult += modMult;

                // Sharpshooter's Blessing
                if (rangedHitsAcc && item.GetGlobalItem<NecromancyGlobalItem>(mod).ranged) totalDmgMult += GetSharpshooterMultiplier();
            }
            
            damage = (int)(damage * totalDmgMult);
        }

        public override void GetWeaponCrit(Item item, ref int crit)
        {
            crit += allCritBonus;
            if (item.GetGlobalItem<NecromancyGlobalItem>().necrotic)
            {
                crit = NCritChance();
            }
            crit = Math.Min(crit, 100);
        }
        
        private int NCritChance()
        {
            int crit = player.inventory[player.selectedItem].crit + necroticCrit;
            // figuring out the player's universal crit modifiers by finding the minimum crit bonus across all vanilla types
            // this can technically be exploited to get bonus necrotic damage without a universal buff but it would be a pain to do anyway
            // plus, there's not really a better way to do this for extra classes as far as I know
            int universalCritBonusGuess = Math.Min(player.meleeCrit, Math.Min(player.rangedCrit, Math.Min(player.magicCrit, player.thrownCrit)));

            crit += universalCritBonusGuess;
            return crit;
        }

        // increases damage if the damage will hit impure health to triple it
        private void ModifyDamage(ref int damage)
        {
            int normalHealth = player.statLife - recentHeals;
            if (normalHealth < damage) damage = (int)(damage + (damage - normalHealth) * (impureBloodDamageTakenMult - 1f)); // damage to impure blood is tripled
        }

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            ModifyDamage(ref damage);
        }

        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            ModifyDamage(ref damage);
        }

        // Need to do crits manually for whatever reason
        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (item.GetGlobalItem<NecromancyGlobalItem>().necrotic)
            {
                int cChance = NCritChance();
                crit = Main.rand.Next(0, 100) < cChance;
            }
            if (target.HasBuff(mod.BuffType("Wounded"))) damage = (int)(damage * bloodBoost);
            if (target.HasBuff(mod.BuffType("Glowing"))) damage = (int)(damage * glowBoost);
            if (target.HasBuff(mod.BuffType("Burning"))) damage = (int)(damage * fireBoost);
            if (target.HasBuff(mod.BuffType("Chilled"))) damage = (int)(damage * iceBoost);
            if (target.HasBuff(mod.BuffType("Goo"))) damage = (int)(damage * gooBoost);
            if (target.HasBuff(mod.BuffType("Shocked"))) damage = (int)(damage * shockBoost);
        }

        // Need to do crits manually for whatever reason
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (proj.GetGlobalProjectile<NecromancyGlobalProjectile>().necrotic)
            {
                int cChance = NCritChance();
                crit = Main.rand.Next(0, 100) < cChance;
            }
            if (target.HasBuff(mod.BuffType("Wounded"))) damage = (int)(damage * bloodBoost);
            if (target.HasBuff(mod.BuffType("Glowing"))) damage = (int)(damage * glowBoost);
            if (target.HasBuff(mod.BuffType("Burning")) 
             || target.HasBuff(BuffID.OnFire)) damage = (int)(damage * fireBoost);
            if (target.HasBuff(mod.BuffType("Chilled"))) damage = (int)(damage * iceBoost);
            if (target.HasBuff(mod.BuffType("Goo"))) damage = (int)(damage * gooBoost);
            if (target.HasBuff(mod.BuffType("Shocked"))) damage = (int)(damage * shockBoost);
        }

        public override float UseTimeMultiplier(Item item)
        {
            return attackSpeed;
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (Main.rand.NextFloat() < dodgeChance)
            {
                player.NinjaDodge();
                return false;
            }

            // dodge chance with teleport if destabilization potion is active
            if (destabilized)
            {
                if (Main.myPlayer == player.whoAmI && Main.rand.NextFloat() < 0.5f)
                {
                    Vector2 teleportTo = 256f * Vector2.UnitX.RotatedByRandom(MathHelper.ToRadians(360)) + player.position;
                    for (int i = 0; i < 200 && Collision.SolidCollision(teleportTo, player.width, player.height); i++)
                    {
                        if (i < 199)
                        {
                            teleportTo = 256f * Vector2.UnitX.RotatedByRandom(MathHelper.ToRadians(360)) + player.position;
                        }
                    }
                    if (Main.netMode == 0)
                    {
                        player.Teleport(teleportTo, 1);
                    }
                    if (Main.netMode == 1)
                    {
                        ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                        packet.Write((byte)NecromancyMessageType.PreHurt);
                        packet.Write(player.whoAmI);
                        packet.WriteVector2(teleportTo);
                        packet.Send();
                    }
                    return false;
                }
            }

            // vampire locket effect prevents damage
            if (vampireTime > 0)
            {
                player.immuneTime = 6;
                Necromancy.BroadcastHealPlayer(player, damage, false, "vamp");
                return false;
            }

            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            // wraith cloak interruption
            if (Main.netMode == 1)
            {
                ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                packet.Write((byte)NecromancyMessageType.Hurt);
                packet.Write(player.whoAmI);
                packet.Send();
            }
            else
            {
                wraithTime = Math.Max(wraithTime - 1, 0);
                if (wraithTime > 0)
                    wraithInterrupt = true;
                wraithTime = 0;
            }

            // for accessories
            timeWithoutHit = 0;

            // on-hit effects
            if (celestialAccessory)
            {
                player.AddBuff(mod.BuffType("CelestialProtection"), 300, false);
            }
            if (cursedSet)
            {
                player.AddBuff(mod.BuffType("CursedRage"), 300, false); 
            }

            if (midnightHelm)
            {
                Player p = Necromancy.LowestAlly(player.Center, player, 1200f);
                if (p != null && p.active)
                {
                    int healAmount = (int)(damage * 0.5f);
                    Necromancy.BroadcastHealPlayer(p, healAmount, false, "midnight-ls-helm");
                }
            }

            foreach (Player p in Necromancy.NearbyAllies(player.Center, player, 1200f, false))
            {
                if (p.GetModPlayer<NecromancyPlayer>().midnightMask)
                {
                    int healAmount = (int)(damage * 0.2f);
                    Necromancy.BroadcastHealPlayer(p, healAmount, false, "midnight-ls-mask");
                }
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (true /*target.type != NPCID.TargetDummy && !target.SpawnedFromStatue */)
            {
                if (universalLifeSteal > 0f)
                {
                    int lifeSteal = (int)(damage * universalLifeSteal);
                    if (lifeSteal > 0) Necromancy.BroadcastHealPlayer(player, lifeSteal, true, "ls-universal");
                }

                if (proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).symphonic)
                {
                    SymphonicEmp(proj);
                    if (proj.GetGlobalProjectile<NecromancyGlobalProjectile>().empowermentType == EmpType.Immortality && necrodancer) SymphonicEmp(proj);
                }

                if (proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic && ichorSet)
                {
                    player.AddBuff(mod.BuffType<Buffs.IchorEndurance>(), 300, false);
                }

                if (proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic && bloodcloth)
                {
                    target.AddBuff(mod.BuffType<Buffs.Wounded>(), 300, false);
                }

                if (proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic && boneplate && crit)
                {
                    target.AddBuff(mod.BuffType<Buffs.Stunned>(), 60, false);
                }

                if (proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).ichor)
                {
                    target.AddBuff(BuffID.Ichor, 600, false);
                }

                if (proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).cursedfire)
                {
                    target.AddBuff(BuffID.CursedInferno, 600, false);
                }

                if (proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).fire)
                {
                    target.AddBuff(BuffID.OnFire, 300, false);
                }

                if (proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).blood)
                {
                    target.AddBuff(mod.BuffType("Wounded"), 300, false);
                }

                if (proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).glow)
                {
                    target.AddBuff(mod.BuffType("Glow"), 300, false);
                }

                if (proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).burn)
                {
                    target.AddBuff(mod.BuffType("Burning"), 300, false);
                }

                if (proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).ice)
                {
                    target.AddBuff(mod.BuffType("Chilled"), 300, false);
                }

                if (proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).goo)
                {
                    target.AddBuff(mod.BuffType("Goo"), 300, false);
                }

                if (proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shock)
                {
                    target.AddBuff(mod.BuffType("Shocked"), 300, false);
                }

                if (proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).lifeSteal > 0)
                {
                    int ls = LifeSteal(proj);
                    if (proj.GetGlobalProjectile<NecromancyGlobalProjectile>().ranged && !proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).rangedHit)
                    {
                        proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).rangedHit = true;
                        rangedHitsNum = Math.Min(7.5f, rangedHitsNum + proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom.useTime / 60f);
                    }
                    if (ls > 0)
                    {
                        if (proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).melee && demonHelm)
                        {
                            foreach (Player p in Necromancy.NearbyAllies(player.Center, player, 1200f, false))
                            {
                                Necromancy.BroadcastHealPlayer(p, ls / 3, false, "ls-proj-demon");
                            }
                        }
                        Necromancy.BroadcastHealPlayer(player, ls, true, "ls-proj");
                    }
                }

                if (proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).healPower > 0)
                {
                    Player healTarget = Necromancy.LowestAlly(player.Center, player, 1200f);
                    int heal = Necromancy.GetHealPower(proj);
                    if (healTarget != null)
                    {
                        Necromancy.BroadcastHealPlayer(healTarget, heal, false, "radiant-proj");
                    }
                }
            }
        }

        // applies new symphonic empowerments when a projectile hits
        private void SymphonicEmp(Projectile proj)
        {
            if (Main.netMode == 0)
            {
                AddEmpowerment(proj.GetGlobalProjectile<NecromancyGlobalProjectile>().empowermentType, empowermentMaxTime);
                if (symphAcc) RefreshEmpowerments();
            }
            else
            {
                ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                packet.Write((byte)NecromancyMessageType.ApplySymphonicEmps);
                packet.Write(player.whoAmI);
                packet.Write((byte)proj.GetGlobalProjectile<NecromancyGlobalProjectile>().empowermentType);
                packet.Write(empowermentMaxTime);
                packet.Write(symphAcc);
                packet.Send();
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (true /*target.type != NPCID.TargetDummy && !target.SpawnedFromStatue */)
            {
                if (universalLifeSteal > 0f)
                {
                    int lifeSteal = (int)(damage * universalLifeSteal);
                    if (lifeSteal > 0) Necromancy.BroadcastHealPlayer(player, lifeSteal, true, "ls-universal-melee");
                }
                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic && ichorSet)
                {
                    player.AddBuff(mod.BuffType<Buffs.IchorEndurance>(), 300, false);
                }

                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic && bloodcloth)
                {
                    target.AddBuff(mod.BuffType<Buffs.Wounded>(), 300, false);
                }

                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic && boneplate && crit)
                {
                    target.AddBuff(mod.BuffType<Buffs.Stunned>(), 60, false);
                }

                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).ichor)
                {
                    target.AddBuff(BuffID.Ichor, 600, false);
                }
                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).cursedfire)
                {
                    target.AddBuff(BuffID.CursedInferno, 600, false);
                }
                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).fire)
                {
                    target.AddBuff(BuffID.OnFire, 600, false);
                }

                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeSteal > 0)
                {
                    int ls = LifeSteal(item);
                    if (ls > 0)
                    {
                        if (item.GetGlobalItem<NecromancyGlobalItem>(mod).melee && demonHelm)
                        {
                            foreach (Player p in Necromancy.NearbyAllies(player.Center, player, 1200f))
                            {
                                Necromancy.BroadcastHealPlayer(p, ls / 3, false, "melee-demon-ls");
                            }
                        }
                        Player healTarget = player;
                        Necromancy.BroadcastHealPlayer(healTarget, ls, true, "melee-ls");
                    }
                }

                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).healPower > 0)
                {
                    Player healTarget = Necromancy.LowestAlly(player.Center, player, 1200f);
                    int heal = Necromancy.GetHealPower(player, item);
                    if (healTarget != null)
                    {
                        Necromancy.BroadcastHealPlayer(healTarget, heal, false, "melee-radiant");
                    }
                }
            }
        }

        // does life steal for a hit swung item
        public int LifeSteal(Item item)
        {
            if (item.GetGlobalItem<NecromancyGlobalItem>(mod).ranged) return item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeSteal * (demonCowl ? 2 : 1);
            int ls = item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeSteal;
            if (player.HasBuff(BuffID.MoonLeech)) ls /= 2;
            ls = (int) ((ls + lifeStealModifier) * lifeStealMult) + (meleeHitAcc && item.GetGlobalItem<NecromancyGlobalItem>(mod).melee ? 3 - (timeWithoutHit / 60) : 0);
            if (item.GetGlobalItem<NecromancyGlobalItem>().ranged && demonCowl) ls *= 2;
            ls = Math.Max(ls, 1);
            return ls;
        }

        // does life steal for a hit projectile
        public int LifeSteal(Projectile proj)
        {
            int ls = proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).lifeSteal;
            if (proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).ranged) // ranged weapons' lifesteal cannot be changed, and a projectile can only give lifesteal once, so you get equal health back if you hit something
            {
                if (proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).rangedHit)
                {
                    return 0;
                }
                else
                {
                    if (demonCowl) ls *= 2;
                    return ls;
                }
            }
            else
            {
                if (player.HasBuff(BuffID.MoonLeech)) ls /= 2;
                ls = (int)((ls + lifeStealModifier) * lifeStealMult) + (meleeHitAcc && proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).melee ? 3 - (timeWithoutHit / 60) : 0);
            }
            return Math.Max(ls, 1);
        }
        
        public override void NaturalLifeRegen(ref float regen)
        {
            regen *= regenMult;
        }

        public override void UpdateLifeRegen()
        {
            player.lifeRegen += regenModifier;
            player.lifeRegen = (int) (player.lifeRegen * regenMult);
            if (hallowedSkull && player.lifeRegen < 0) player.lifeRegen = 0;
        }

        public override void OnRespawn(Player player)
        {
            if (fullHealthRespawn)
            {
                player.statLife = player.statLifeMax2;
            }
        }

        // multiplier for necrotic ranged weapons if Sharpshooter's Blessing is equipped
        public float GetSharpshooterMultiplier()
        {
            float multiplier = 0.75f;
            multiplier = 0.75f + 0.1f * rangedHitsNum;
            multiplier = Math.Min(multiplier, 1.5f);
            return multiplier;
        }

        // things for finding up/down buttons that change with player's setting

        public int DownTapTimer()
        {
            if (Main.ReversedUpDownArmorSetBonuses)
            {
                return player.doubleTapCardinalTimer[1];
            }
            else
            {
                return player.doubleTapCardinalTimer[0];
            }
        }

        public bool DownButton(TriggersSet triggersSet)
        {
            if (Main.ReversedUpDownArmorSetBonuses)
            {
                return triggersSet.Up;
            }
            else
            {
                return triggersSet.Down;
            }
        }

        public int UpDoubleTapTimer()
        {
            if (Main.ReversedUpDownArmorSetBonuses)
            {
                return player.doubleTapCardinalTimer[0];
            }
            else
            {
                return player.doubleTapCardinalTimer[1];
            }
        }

        public bool UpButton(TriggersSet triggersSet)
        {
            if (Main.ReversedUpDownArmorSetBonuses)
            {
                return triggersSet.Down;
            }
            else
            {
                return triggersSet.Up;
            }
        }

        // proportion of impure blood
        public float GetBloodSaturation()
        {
            return RecentHeals / 1f / player.statLife;
        }

        // more impure blood makes the player darker to indicate
        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            float mult = GetBloodSaturation();
            if (mult > 0f)
            {
                r *= 1 - (0.4f * mult);
                g *= 1 - (0.7f * mult);
                b *= 1 - (0.4f * mult);
            }
        }

        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            for (int i = 0; i < layers.Count; i++)
            {
                if (layers[i].Name.Contains("HeldItem"))
                {
                    layers.Insert(i + 1, new PlayerLayer(
                        "Necromancy",
                        "Necromancy: Item Glow Mask",
                        PlayerLayer.HeldItem,
                        delegate
                        {
                            GlowMaskSwungItem(player);
                        }
                    ));
                }
            }
        }

        // draw swung items' custom glow masks
        private static void GlowMaskSwungItem(Player drawPlayer)
        {
            // copied from vanilla code
            if (drawPlayer != null && drawPlayer.active && drawPlayer.selectedItem >= 0 && drawPlayer.selectedItem < drawPlayer.inventory.Length && drawPlayer.itemAnimation > 0)
            {
                DrawData drawData;
                SpriteEffects effect = SpriteEffects.FlipHorizontally;
                Vector2 itemLocation = drawPlayer.itemLocation;
                Item item = drawPlayer.inventory[drawPlayer.selectedItem];
                if (item.IsAir) return;
                Texture2D glowTex = item.GetGlobalItem<NecromancyGlobalItem>().glowMask;
                if (glowTex == null) return;

                if (drawPlayer.gravDir == 1f)
                {
                    if (drawPlayer.direction == 1) effect = SpriteEffects.None;
                    else effect = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    if (drawPlayer.direction == 1) effect = SpriteEffects.FlipVertically;
                    else effect = (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically);
                }

                Color color = Color.White;

                Color color24 = new Color(250, 250, 250, item.alpha);
                Vector2 zero = Vector2.Zero;

                ItemSlot.GetItemLight(ref color, item, false);
                
                if (glowTex != null && item.useStyle == 1)
                {
                    drawData = new DrawData(glowTex, new Vector2(((int)(itemLocation.X - Main.screenPosition.X)),
                        ((int)(itemLocation.Y - Main.screenPosition.Y))), new Rectangle?(new Rectangle(0, 0, glowTex.Width, glowTex.Height)), new Color(250, 250, 250, item.alpha),
                        drawPlayer.itemRotation, new Vector2(glowTex.Width * 0.5f - glowTex.Width * 0.5f * drawPlayer.direction, drawPlayer.gravDir == -1f ? 0f : glowTex.Height) + Vector2.Zero,
                        item.scale, effect, 0);
                    Main.playerDrawData.Add(drawData);
                }
            }
        }
    }
}
