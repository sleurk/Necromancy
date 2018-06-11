using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Localization;

namespace Necromancy
{
	public class NecromancyPlayer : ModPlayer
    {
        public static readonly Type[] empowermentIndex =
        {
            typeof(Buffs.EmpowermentCritChance),
            typeof(Buffs.EmpowermentDamage),
            typeof(Buffs.EmpowermentDefense),
            typeof(Buffs.EmpowermentEndurance),
            typeof(Buffs.EmpowermentFlight),
            typeof(Buffs.EmpowermentImmortality),
            typeof(Buffs.EmpowermentLifeSteal),
            typeof(Buffs.EmpowermentMagicDamage),
            typeof(Buffs.EmpowermentMaxLife),
            typeof(Buffs.EmpowermentMaxMana),
            typeof(Buffs.EmpowermentMeleeDamage),
            typeof(Buffs.EmpowermentMoveSpeed),
            typeof(Buffs.EmpowermentNecroticDamage),
            typeof(Buffs.EmpowermentRangedDamage),
            typeof(Buffs.EmpowermentRegen),
            typeof(Buffs.EmpowermentSummonDamage)
        };

        public int[] empowermentType =
        {
            ModLoader.GetMod("Necromancy").BuffType<Buffs.EmpowermentCritChance>(),
            ModLoader.GetMod("Necromancy").BuffType<Buffs.EmpowermentDamage>(),
            ModLoader.GetMod("Necromancy").BuffType<Buffs.EmpowermentDefense>(),
            ModLoader.GetMod("Necromancy").BuffType<Buffs.EmpowermentEndurance>(),
            ModLoader.GetMod("Necromancy").BuffType<Buffs.EmpowermentFlight>(),
            ModLoader.GetMod("Necromancy").BuffType<Buffs.EmpowermentImmortality>(),
            ModLoader.GetMod("Necromancy").BuffType<Buffs.EmpowermentLifeSteal>(),
            ModLoader.GetMod("Necromancy").BuffType<Buffs.EmpowermentMagicDamage>(),
            ModLoader.GetMod("Necromancy").BuffType<Buffs.EmpowermentMaxLife>(),
            ModLoader.GetMod("Necromancy").BuffType<Buffs.EmpowermentMaxMana>(),
            ModLoader.GetMod("Necromancy").BuffType<Buffs.EmpowermentMeleeDamage>(),
            ModLoader.GetMod("Necromancy").BuffType<Buffs.EmpowermentMoveSpeed>(),
            ModLoader.GetMod("Necromancy").BuffType<Buffs.EmpowermentNecroticDamage>(),
            ModLoader.GetMod("Necromancy").BuffType<Buffs.EmpowermentRangedDamage>(),
            ModLoader.GetMod("Necromancy").BuffType<Buffs.EmpowermentRegen>(),
            ModLoader.GetMod("Necromancy").BuffType<Buffs.EmpowermentSummonDamage>()
        };

        public int lifeCostModifier = 0;

        public int painSummonPower = 0;

        public int allCritBonus = 0;
        public float allDamageMultiplier = 1f;
        public float allMeleeDamageMultiplier = 1f;
        public float allRangedDamageMultiplier = 1f;
        public float allMagicDamageMultiplier = 1f;
        public float allSummonDamageMultiplier = 1f;

        public float necroticMult = 0f;
        public int necroticCritBonus = 0;

        public int lifeStealModifier = 0;
        public float lifeStealMult = 1f;

        public int regenModifier = 0;
        public float regenMult = 1f;

        public List<Projectile> magnets = new List<Projectile>();
        public bool magnetsActive = false;
        public Projectile rotCloud = null;

        public float nMeleeMult = 1f;
        public float nRangedMult = 1f;
        public float nMagicMult = 1f;
        public float nSummonMult = 1f;
        public float nRadiantMult = 1f;
        public float nSymphonicMult = 1f;

        public bool destabilized = false;

        public bool meleeHitAcc = false;
        public int timeWithoutHit = 0;
        public float rangedHitsNum = 0f;
        public bool rangedHitsAcc = false;
        public bool magicCostAcc = false;
        public bool symphAcc = false;
        public bool throwAcc = false;
        public bool summonAcc = false;
        public bool radiantAcc = false;

        public bool celestialAccessory = false;

        public bool fullHealthRespawn = false;

        public int empowermentMaxTime = 600;

        public int wingUse = 0;

        public List<int> buffedWeapon = new List<int>();
        public int agitation = 0;
        public int resurrect = 0;

        public bool downOld = false;
        public bool upOld = false;

        public bool wraith = false;
        public int wraithTime = 0;
        public bool wraithInterrupt = false;

        public bool manaAcc = false;

        public float dmgEmp = 0f;
        public int critEmp = 0;

        public float universalLifeSteal = 0f;

        public bool bloodcloth = false;
        public bool boneplate = false;
        public bool cursedSet = false;
        public bool ichorSet = false;
        public bool hallowedSkull = false;
        public bool necrodancer = false;
        public bool midnightHelm = false;
        public bool midnightMask = false;
        public bool wormholeSet = false;
        public bool demonHelm = false;
        public bool demonCowl = false;

        public bool immortalEmp = false;

        public bool vampireLocket = false;
        public int vampireTime = 0;

        public int[] empPotency = new int[empowermentIndex.Length];
        public bool[] empActiveCheck = new bool[empowermentIndex.Length];

        public override void ResetEffects()
		{
            necroticMult = 0f;
            necroticCritBonus = 0;

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
            throwAcc = false;
            summonAcc = false;

            celestialAccessory = false;

            fullHealthRespawn = false;

            empowermentMaxTime = 600;

            wingUse = Math.Max(wingUse - 1, 0);

            agitation = Math.Max(agitation - 1, 0);
            resurrect = 0;
            
            wraithTime = Math.Max(wraithTime - 1, 0);
            wraith = false;
            wraithInterrupt = false;

            dmgEmp = 0f;
            critEmp = 0;

            universalLifeSteal = 0f;

            manaAcc = false;
            
            immortalEmp = false;

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

            for (int i = 0; i < empowermentIndex.Length; i++)
            {
                if (!empActiveCheck[i])
                {
                    empPotency[i] = 0;
                }
            }
            empActiveCheck = new bool[empowermentIndex.Length];
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (immortalEmp)
            {
                immortalEmp = false;
                for (int i = 0; i < player.buffType.Length; i++)
                {
                    if (player.buffType[i] == mod.BuffType<Buffs.EmpowermentImmortality>())
                    {
                        player.buffTime[i] = 1;
                        Necromancy.HealPlayer(player, 100, "revive-imm");
                        for (int j = 0; j < 15; j++)
                        {
                            Dust d = Dust.NewDustPerfect(player.Center, 21);
                            d.velocity = Main.rand.NextVector2Circular(6f, 6f);
                        }
                        return false;
                    }
                }
            }

            if (vampireLocket && Array.IndexOf(player.buffType, mod.BuffType("VampiricExhaustion")) == -1)
            {
                Necromancy.HealPlayer(player, (int)damage, "revive-vamp");
                vampireTime = 60;
                player.AddBuff(mod.BuffType("VampiricExhaustion"), 10800);
                return false;
            }
            return true;
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            player.respawnTimer -= (int)(player.respawnTimer * 0.15f * resurrect);
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (wraith && UpButton(triggersSet) && !upOld && UpDoubleTapTimer() > 0)
            {
                player.channel = false;
                Main.PlaySound(SoundID.Item20, player.Center);
                for (int i = 0; i < 20; i++)
                {
                    Vector2 dv = Main.rand.NextVector2Circular(8f, 8f);
                    Dust d = Dust.NewDustDirect(player.position, player.width, player.height, 54, dv.X, dv.Y);
                    d.scale = Main.rand.NextFloat(1.5f, 2.5f);
                    d.noGravity = true;
                }
                Necromancy.DrainLife(player, 20);
                wraithTime = 2;
            }
            if (wraithTime > 0 && UpButton(triggersSet))
            {
                Necromancy.DrainLife(player, 1);
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
                for (int i = 0; i < 10; i++)
                {
                    Dust d = Dust.NewDustPerfect(player.Center + vel, 54, Main.rand.NextVector2Circular(4f, 4f));
                    d.scale = Main.rand.NextFloat(1.5f, 2.5f);
                    d.noGravity = true;
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
            if (wraithInterrupt)
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
            upOld = UpButton(triggersSet);

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
            float totalDmgMult = 1f;

            if (item.IsAir) return;
            
            // encahntment ritual
            if (buffedWeapon.Contains(item.type))
            {
                totalDmgMult += 0.05f * item.GetGlobalItem<Items.NecromancyGlobalItem>().enchanted;
            }
            else
            {
                item.GetGlobalItem<Items.NecromancyGlobalItem>().enchanted = 0;
            }

            buffedWeapon = new List<int>();

            if (item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).necrotic)
            {
                damage = (int)(damage / player.magicDamage);

                float vanillaMult = dmgEmp;
                if (Array.IndexOf(player.buffType, BuffID.Wrath) != -1) vanillaMult += 0.1f;
                if (Array.IndexOf(player.buffType, BuffID.WellFed) != -1) vanillaMult += 0.05f;
                if (Array.IndexOf(player.buffType, BuffID.NebulaUpDmg1) != -1) vanillaMult += 0.15f;
                if (Array.IndexOf(player.buffType, BuffID.NebulaUpDmg2) != -1) vanillaMult += 0.3f;
                if (Array.IndexOf(player.buffType, BuffID.NebulaUpDmg3) != -1) vanillaMult += 0.45f;
                if (Array.IndexOf(player.armor, ItemID.AvengerEmblem) != -1) vanillaMult += 0.12f;
                if (Array.IndexOf(player.armor, ItemID.DestroyerEmblem) != -1) vanillaMult += 0.1f;
                if (Array.IndexOf(player.armor, ItemID.CelestialStone) != -1) vanillaMult += 0.1f;
                if (Array.IndexOf(player.armor, ItemID.SunStone) != -1 && Main.dayTime) vanillaMult += 0.1f;
                if (Array.IndexOf(player.armor, ItemID.MoonStone) != -1 && !Main.dayTime) vanillaMult += 0.1f;
                if (Array.IndexOf(player.armor, ItemID.Gi) != -1) vanillaMult += 0.05f;
                if (Array.IndexOf(player.armor, ItemID.ChlorophytePlateMail) != -1) vanillaMult += 0.05f;
                for (int i = 0; i < player.armor.Length; i++)
                {
                    Item acc = player.armor[i];
                    if (acc != null)
                    {
                        if (acc.prefix == 69) vanillaMult += 0.01f;
                        if (acc.prefix == 70) vanillaMult += 0.02f;
                        if (acc.prefix == 71) vanillaMult += 0.03f;
                        if (acc.prefix == 72) vanillaMult += 0.04f;
                    }
                }
                totalDmgMult += vanillaMult;

                float modMult = necroticMult;
                if (item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).melee) modMult += nMeleeMult - 1;
                if (item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).ranged) modMult += nRangedMult - 1;
                if (item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).magic) modMult += nMagicMult - 1;
                if (item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).summon) modMult += nSummonMult - 1;
                if (item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).radiant) modMult += nRadiantMult - 1;
                if (item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).symphonic) modMult += nSymphonicMult - 1;
                totalDmgMult += modMult;

                item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).lifeCost = item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).baseLifeCost;
                if (!item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).ranged && item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).baseLifeCost > 0)
                {
                    item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).lifeCost += lifeCostModifier;
                }

                // Call of the Void
                if (magicCostAcc && item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).magic)
                {
                    item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).lifeCost = (int) (item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).lifeCost * (float) player.statLife / (player.statLifeMax2 / 2f));
                }

                // Sharpshooter's Blessing
                if (rangedHitsAcc && item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).ranged) damage = (int)(damage * GetSharpshooterMultiplier());


                if (item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).baseLifeCost > 0) item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).lifeCost = Math.Max(item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).lifeCost, 1);
            }

            damage = (int)(damage * totalDmgMult);
        }

        public override void GetWeaponCrit(Item item, ref int crit)
        {
            crit += allCritBonus;
            if (item.GetGlobalItem<Items.NecromancyGlobalItem>().necrotic)
            {
                crit = NCritChance();
            }
            crit = Math.Min(crit, 100);
        }

        // this only works because all weapons have 4% base crit chance
        private int NCritChance()
        {
            int crit = 4 + necroticCritBonus;
            if (Array.IndexOf(player.buffType, BuffID.Rage) != -1) crit += 10;
            if (Array.IndexOf(player.buffType, BuffID.WellFed) != -1) crit += 2;
            if (Array.IndexOf(player.armor, ItemID.EyeoftheGolem) != -1) crit += 10;
            if (Array.IndexOf(player.armor, ItemID.DestroyerEmblem) != -1) crit += 8;
            if (Array.IndexOf(player.armor, ItemID.CelestialStone) != -1) crit += 2;
            if (Array.IndexOf(player.armor, ItemID.SunStone) != -1 && Main.dayTime) crit += 2;
            if (Array.IndexOf(player.armor, ItemID.MoonStone) != -1 && !Main.dayTime) crit += 2;
            if (Array.IndexOf(player.armor, ItemID.Gi) != -1) crit += 5;
            if (Array.IndexOf(player.armor, ItemID.ChlorophytePlateMail) != -1) crit += 7;
            if (Array.IndexOf(player.armor, ItemID.ChlorophyteGreaves) != -1) crit += 8;
            crit += allCritBonus;
            for (int i = 0; i < player.armor.Length; i++)
            {
                Item acc = player.armor[i];
                if (acc != null)
                {
                    if (acc.prefix == 67) crit += 2;
                    if (acc.prefix == 68) crit += 4;
                }
            }
            return crit;
        }

        // Need to do crits manually for whatever reason
        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (item.GetGlobalItem<Items.NecromancyGlobalItem>().necrotic)
            {
                int cChance = NCritChance();
                crit = Main.rand.Next(0, 100) < cChance;
            }
        }

        // Need to do crits manually for whatever reason
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>().necrotic)
            {
                int cChance = NCritChance();
                crit = Main.rand.Next(0, 100) < cChance;
            }
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (destabilized)
            {
                if (Main.rand.NextFloat() < 0.5f)
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

            if (vampireTime > 0)
            {
                player.immuneTime = 6;
                Necromancy.HealPlayer(player, damage, "vamp");
                return false;
            }

            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            wraithTime = Math.Max(wraithTime - 1, 0);
            if (wraithTime > 0) wraithInterrupt = true;
            wraithTime = 0;
            if (Main.netMode == 1)
            {
                ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                packet.Write((byte)NecromancyMessageType.Hurt);
                packet.Write(player.whoAmI);
                packet.Send();
            }
            timeWithoutHit = 0;
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
                Player p = Necromancy.LowestAlly(player.Center, player, 600f);
                if (p != null && p.active)
                {
                    int healAmount = (int)(damage * 0.5f);
                    Necromancy.HealPlayer(p, healAmount, "midnight-ls-helm");
                }
            }

            foreach (Player p in Necromancy.NearbyAllies(player.Center, player, 600f, false))
            {
                if (p.GetModPlayer<NecromancyPlayer>().midnightMask)
                {
                    int healAmount = (int)(damage * 0.2f);
                    Necromancy.HealPlayer(p, healAmount, "midnight-ls-mask");
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
                    if (lifeSteal > 0) Necromancy.HealPlayer(player, lifeSteal, "ls-universal");
                }

                if (proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).symphonic)
                {
                    SymphonicBuff(proj);
                }

                if (proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).necrotic && ichorSet)
                {
                    player.AddBuff(mod.BuffType<Buffs.IchorEndurance>(), 300, false);
                }

                if (proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).necrotic && bloodcloth)
                {
                    target.AddBuff(mod.BuffType<Buffs.Wounded>(), 300, false);
                }

                if (proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).necrotic && boneplate && crit)
                {
                    target.AddBuff(mod.BuffType<Buffs.Stunned>(), 60, false);
                }

                if (proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).ichor)
                {
                    target.AddBuff(BuffID.Ichor, 600, false);
                }

                if (proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).cursedfire)
                {
                    target.AddBuff(BuffID.CursedInferno, 600, false);
                }

                if (proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).fire)
                {
                    target.AddBuff(BuffID.OnFire, 600, false);
                }
                
                if (proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).lifeSteal > 0)
                {
                    int ls = LifeSteal(proj);
                    if (ls > 0)
                    {
                        if (proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).melee && demonHelm)
                        {
                            foreach (Player p in Necromancy.NearbyAllies(player.Center, player, 600f, false))
                            {
                                Necromancy.HealPlayer(p, ls / 3, "ls-proj-demon");
                            }
                        }
                        Necromancy.HealPlayer(player, ls, "ls-proj");
                    }
                }

                if (proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).healPower > 0)
                {
                    Player healTarget = Necromancy.LowestAlly(proj.Center, Main.player[proj.owner], 300f);
                    int heal = Necromancy.GetHealPower(proj);
                    if (healTarget != null)
                    {
                        Necromancy.HealPlayer(healTarget, heal, "radiant-proj");
                    }
                }
            }
        }

        private void SymphonicBuff(Projectile proj)
        {
            foreach (Player p in Necromancy.NearbyAllies(proj.Center, Main.player[proj.owner], 1000, true))
            {
                if (Main.netMode == 0) p.AddBuff(proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).buffType, empowermentMaxTime, false);
                if (Main.netMode == 1)
                {
                    ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                    packet.Write((byte)NecromancyMessageType.ApplySymphonicBuffs);
                    packet.Write(p.whoAmI);
                    packet.Write(proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).buffType);
                    packet.Write(empowermentMaxTime);
                    packet.Send();
                }
                if (symphAcc)
                {
                    p.GetModPlayer<NecromancyPlayer>().RefreshEmpowerments();
                }
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (true /*target.type != NPCID.TargetDummy && !target.SpawnedFromStatue */)
            {
                if (universalLifeSteal > 0f)
                {
                    int lifeSteal = (int)(damage * universalLifeSteal);
                    if (lifeSteal > 0) Necromancy.HealPlayer(player, lifeSteal, "ls-universal-melee");
                }
                if (item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).necrotic && ichorSet)
                {
                    player.AddBuff(mod.BuffType<Buffs.IchorEndurance>(), 300, false);
                }

                if (item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).necrotic && bloodcloth)
                {
                    target.AddBuff(mod.BuffType<Buffs.Wounded>(), 300, false);
                }

                if (item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).necrotic && boneplate && crit)
                {
                    target.AddBuff(mod.BuffType<Buffs.Stunned>(), 60, false);
                }

                if (item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).ichor)
                {
                    target.AddBuff(BuffID.Ichor, 600, false);
                }
                if (item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).cursedfire)
                {
                    target.AddBuff(BuffID.CursedInferno, 600, false);
                }
                if (item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).fire)
                {
                    target.AddBuff(BuffID.OnFire, 600, false);
                }

                if (item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).lifeSteal > 0)
                {
                    int ls = LifeSteal(item);
                    if (ls > 0)
                    {
                        if (item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).melee && demonHelm)
                        {
                            foreach (Player p in Necromancy.NearbyAllies(player.Center, player, 600f))
                            {
                                Necromancy.HealPlayer(p, ls / 3, "melee-demon-ls");
                            }
                        }
                        Player healTarget = player;
                        Necromancy.HealPlayer(healTarget, ls, "melee-ls");
                    }
                }

                if (item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).healPower > 0)
                {
                    Player healTarget = Necromancy.LowestAlly(player.Center, player, 300f);
                    int heal = Necromancy.GetHealPower(player, item);
                    if (healTarget != null)
                    {
                        Necromancy.HealPlayer(healTarget, heal, "melee-radiant");
                    }
                }
            }
        }

        public int LifeSteal(Item item)
        {
            int ls = item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).lifeSteal;
            if (Array.IndexOf(player.buffType, BuffID.MoonLeech) > -1) ls /= 2;
            if (item.GetGlobalItem<Items.NecromancyGlobalItem>().ranged && demonCowl) ls++; // should only apply in tooltip, see projectile for actual effect
            ls = (int) ((ls + lifeStealModifier) * lifeStealMult) + (meleeHitAcc && item.GetGlobalItem<Items.NecromancyGlobalItem>(mod).melee ? (timeWithoutHit / 60) - 2 : 0);
            ls = Math.Max(ls, 1);
            return ls;
        }

        public int LifeSteal(Projectile proj)
        {
            int ls = proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).lifeSteal;
            if (proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).ranged) // ranged weapons' lifesteal cannot be changed, and a projectile can only give lifesteal once, so you get equal health back if you hit something
            {
                if (proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).rangedHit)
                {
                    return 0;
                }
                else
                {
                    proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).rangedHit = true;
                    rangedHitsNum += proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom.useTime / 60f;
                    if (demonCowl) return ls + 1;
                    return ls;
                }
            }
            else
            {
                if (Array.IndexOf(player.buffType, BuffID.MoonLeech) > -1) ls /= 2;
                return (int)((ls + lifeStealModifier) * lifeStealMult) + (meleeHitAcc && proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).melee ? timeWithoutHit / 60 : 0);
            }
        }

        public override void NaturalLifeRegen(ref float regen)
        {
            regen *= regenMult;
        }

        public override void UpdateLifeRegen()
        {
            player.lifeRegen += regenModifier;
            player.lifeRegen = (int) (player.lifeRegen * regenMult);
            for (int i = 0; i < 200; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.type != NPCID.TargetDummy && npc.GetGlobalNPC<NPCs.NecromancyNPC>(mod).pestilencePlayer == player)
                {
                    player.lifeRegen += 16;
                }
            }
            if (hallowedSkull && player.lifeRegen < 0) player.lifeRegen = 0;
        }

        public override void OnRespawn(Player player)
        {
            if (fullHealthRespawn)
            {
                player.statLife = player.statLifeMax2;
            }
        }

        public float GetSharpshooterMultiplier()
        {
            float multiplier = 0.75f;
            multiplier = 0.75f + 0.1f * rangedHitsNum;
            multiplier = Math.Min(multiplier, 1.5f);
            return multiplier;
        }

        public void RefreshEmpowerments()
        {
            for (int i = 0; i < player.buffType.Length; i++)
            {
                if (Array.IndexOf(empowermentType, player.buffType[i]) > -1)
                {
                    player.buffTime[i] = empowermentMaxTime;
                }
            }
        }

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
    }
}
