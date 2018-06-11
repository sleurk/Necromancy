using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;
using Terraria.Localization;

namespace Necromancy.Items
{
    public class NecromancyGlobalItem : GlobalItem
    {
        public int baseCrit;

        public bool necrotic;
        public bool melee;
        public bool ranged;
        public bool magic;
        public bool summon;
        public bool throwing;
        public bool radiant;
        public bool symphonic;

        public bool ichor;
        public bool cursedfire;
        public bool fire;

        public bool thoriumRarity;

        public int numProjectiles;
        public int numShoot;

        public int baseLifeCost;
        public int lifeCost;

        public int reloadCost;
        
        public int lifeSteal;

        public int healPower;

        public int summonCost;

        public int rClickDelay;

        public int enchanted;

        public NecromancyGlobalItem()
        {
            baseCrit = 0;

            necrotic = false;
            melee = false;
            ranged = false;
            magic = false;
            summon = false;
            throwing = false;
            radiant = false;
            symphonic = false;

            ichor = false;
            cursedfire = false;
            fire = false;

            thoriumRarity = false;

            numProjectiles = 1;
            numShoot = 1;

            baseLifeCost = 0;
            lifeCost = baseLifeCost;

            reloadCost = 0;

            lifeSteal = 0;

            healPower = 0;

            summonCost = 0;

            rClickDelay = 0;

            enchanted = 0;
        }

        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public override GlobalItem Clone(Item item, Item itemClone)
        {
            NecromancyGlobalItem myClone = (NecromancyGlobalItem)base.Clone(item, itemClone);
            myClone.baseCrit = baseCrit;

            myClone.necrotic = necrotic;
            myClone.melee = melee;
            myClone.ranged = ranged;
            myClone.magic = magic;
            myClone.summon = summon;
            myClone.throwing = throwing;
            myClone.radiant = radiant;
            myClone.symphonic = symphonic;

            myClone.ichor = ichor;
            myClone.cursedfire = cursedfire;
            myClone.fire = fire;

            myClone.thoriumRarity = thoriumRarity;

            myClone.numProjectiles = numProjectiles;

            myClone.baseLifeCost = baseLifeCost;
            myClone.lifeCost = lifeCost;

            myClone.reloadCost = reloadCost;

            myClone.lifeSteal = lifeSteal;

            myClone.healPower = healPower;

            myClone.summonCost = summonCost;

            myClone.rClickDelay = rClickDelay;

            myClone.enchanted = enchanted;

            return myClone;
        }

        public override void GetWeaponDamage(Item item, Player player, ref int damage)
        {
            if (item.GetGlobalItem<NecromancyGlobalItem>(mod).enchanted > 0)
            {
                damage = (int)(damage * (1 + 0.05f * item.GetGlobalItem<NecromancyGlobalItem>(mod).enchanted));
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            // encahntment incantation
            if (item.GetGlobalItem<NecromancyGlobalItem>(mod).thoriumRarity) // Blood Orange Rarity (Ragnarok tier items)
            {
                tooltips[0].overrideColor = new Color(255, 127, 0);
            }
            if (tooltips.Count > 1)
            {
                int damageLine = tooltips[1].text.Equals("Marked as favorite") ? 3 : 1;
                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic)
                {
                    tooltips[damageLine].text = tooltips[damageLine].text.Replace(" magic damage", " necrotic damage");
                }

                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).enchanted > 0)
                {
                    tooltips[damageLine].text = "Enchanted\n" + tooltips[damageLine].text;
                }

                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).melee)
                {
                    tooltips[damageLine].text = tooltips[damageLine].text.Replace(" damage", " melee damage");
                }
                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).ranged)
                {
                    tooltips[damageLine].text = tooltips[damageLine].text.Replace(" damage", " ranged damage");
                }
                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).magic)
                {
                    tooltips[damageLine].text = tooltips[damageLine].text.Replace(" damage", " magic damage");
                }
                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).summon)
                {
                    tooltips[damageLine].text = tooltips[damageLine].text.Replace(" damage", " summon damage");
                }
                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).throwing)
                {
                    tooltips[damageLine].text = tooltips[damageLine].text.Replace(" damage", " throwing damage");
                }
                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).radiant)
                {
                    tooltips[damageLine].text = tooltips[damageLine].text.Replace(" damage", " radiant damage");
                }
                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).symphonic)
                {
                    tooltips[damageLine].text = tooltips[damageLine].text.Replace(" damage", " symphonic damage");
                }

                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeSteal > 0)
                {
                    tooltips[damageLine].text = tooltips[damageLine].text.Replace(" damage", " damage\nSteals " + Main.player[item.owner].GetModPlayer<NecromancyPlayer>(mod).LifeSteal(item) * Shoots(item) + " life");
                }

                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).healPower > 0)
                {
                    tooltips[damageLine].text = tooltips[damageLine].text.Replace(" damage", " damage\nHeals allies for " + Necromancy.GetHealPower(Main.player[item.owner], item) * Shoots(item) + " life");
                }

                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost > 0)
                {
                    string text = "\nUses " + Necromancy.GetCost(Main.player[item.owner], item, true) + " life";
                    if (Main.player[item.owner].GetModPlayer<NecromancyPlayer>().manaAcc && !item.GetGlobalItem<NecromancyGlobalItem>().ranged)
                    {
                        text += " (" + Necromancy.GetCost(Main.player[item.owner], item, true) * 10 + " mana)";
                    }
                    tooltips[damageLine].text = tooltips[damageLine].text.Replace(" damage", " damage" + text);
                }

                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).summonCost > 0)
                {
                    Player player = Main.player[item.owner];
                    int summons = Necromancy.CountSummons(player);
                    tooltips[damageLine].text = tooltips[damageLine].text.Replace(" damage", " damage\nCosts " + item.GetGlobalItem<NecromancyGlobalItem>(mod).summonCost + "x" + summons + " maximum life per minion" +
                        "\nCurrent total summon cost: " + item.GetGlobalItem<NecromancyGlobalItem>(mod).summonCost * summons * summons);
                }

                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).reloadCost > 0)
                {
                    tooltips[damageLine].text = tooltips[damageLine].text.Replace(" damage", " damage\nReload 10 for " + item.GetGlobalItem<NecromancyGlobalItem>(mod).reloadCost + " life");
                }
                
                int shoot = item.GetGlobalItem<NecromancyGlobalItem>(mod).numShoot * item.GetGlobalItem<NecromancyGlobalItem>(mod).numProjectiles;
                if (shoot > 1)
                {
                    tooltips[damageLine].text = tooltips[damageLine].text.Replace(" damage", " damage\nFires " + shoot + " projectiles");
                }
            }
        }

        public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.itemAnimation > player.itemAnimationMax - item.useTime)
            {
                Necromancy.DoLifeCost(player, item);
            }
            return base.Shoot(item, player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (player.GetModPlayer<NecromancyPlayer>().wraithTime > 0) return false;
            return base.CanUseItem(item, player);
        }

        public override void HoldItem(Item item, Player player)
        {
            if (item.GetGlobalItem<NecromancyGlobalItem>(mod).rClickDelay > 0)
            {
                item.GetGlobalItem<NecromancyGlobalItem>(mod).rClickDelay--;
            }
        }

        public override bool AltFunctionUse(Item item, Player player)
        {
            if (item.GetGlobalItem<NecromancyGlobalItem>(mod).rClickDelay == 0 && item.GetGlobalItem<NecromancyGlobalItem>(mod).reloadCost > 0 && item.stack < item.maxStack)
            {
                item.stack = Math.Min(item.stack + 10, item.maxStack);
                Necromancy.DrainLife(player, item.GetGlobalItem<NecromancyGlobalItem>(mod).reloadCost);
            }

            if (item.GetGlobalItem<NecromancyGlobalItem>(mod).rClickDelay == 0 && item.GetGlobalItem<NecromancyGlobalItem>(mod).summonCost > 0)
            {
                for (int i = 0; i < 1000; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (SummonedFromItem(p, item) && p.owner == player.whoAmI)
                    {
                        p.Kill();
                        i = 1000;
                    }
                }
            }
            rClickDelay = 3;
            return base.AltFunctionUse(item, player);
        }

        public override bool ConsumeItem(Item item, Player player)
        {
            if (player.GetModPlayer<NecromancyPlayer>().throwAcc && item.GetGlobalItem<NecromancyGlobalItem>(mod).throwing)
            {
                return Main.rand.NextFloat() < (float)player.statLife / player.statLifeMax2;
            }
            return base.ConsumeItem(item, player);
        }

        public override bool PreOpenVanillaBag(string context, Player player, int arg)
        {
            if (arg == ItemID.PlanteraBossBag)
            {
                Item.NewItem(player.position, player.width, player.height, mod.ItemType("AcidSpray"));
            }
            if (arg == ItemID.WallOfFleshBossBag)
            {
                if (Main.rand.NextFloat() < 0.25)
                {
                    Item.NewItem(player.position, player.width, player.height, mod.ItemType("NecromancerEmblem"));
                }
                if (Main.rand.NextFloat() < 1/3f)
                {
                    Item.NewItem(player.position, player.width, player.height, mod.ItemType("GrenadeBow"));
                }
            }

            Mod thorium = ModLoader.GetMod("ThoriumMod");
            if (thorium != null && arg == thorium.ItemType("LichBag"))
            {
                player.QuickSpawnItem(mod.ItemType<Weapons.Magic.LichSoul>());
            }
            else if (thorium != null && arg == thorium.ItemType("BoreanBag"))
            {
                player.QuickSpawnItem(mod.ItemType<Weapons.Symphonic.TenorDrum>());
            }
            else if (thorium != null && arg == thorium.ItemType("BeholderBag"))
            {
                player.QuickSpawnItem(mod.ItemType<Weapons.Radiant.HyperthermalSlicer>());
            }
            return true;
        }

        public bool SummonedFromItem(Projectile p, Item item)
        {
            return p.active && p.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>().shotFrom != null && p.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>().shotFrom.type == item.type;
        }

        public override bool WingUpdate(int wings, Player player, bool inUse)
        {
            if (inUse)
            {
                player.GetModPlayer<NecromancyPlayer>().wingUse = 2;
            }
            return base.WingUpdate(wings, player, inUse);
        }

        // Number of projectiles an item shoots
        // only reliable for necrotic weapons, since numProjectiles is only changed in the mod and not globally
        public int Shoots(Item item)
        {
            return ShootTimes(item) * item.GetGlobalItem<NecromancyGlobalItem>().numProjectiles;
        }

        // Number of times Shoot is called per click
        public int ShootTimes(Item item)
        {
            return item.useAnimation / item.useTime;
        }
    }
}