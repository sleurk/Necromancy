using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Necromancy.Projectiles;
using Terraria.Utilities;

namespace Necromancy.Items
{
    public class NecromancyGlobalItem : GlobalItem
    {
        public bool necrotic; // necrotic damage
        public bool melee; // necrotic melee damage
        public bool ranged; // necrotic ranged damage
        public bool magic; // // necrotic magic damage
        public bool summon; // necrotic summon damage
        public bool throwing; // necrotic throwing damage
        public bool radiant; // necrotic radiant damage
        public bool symphonic; // necrotic symphonic damage

        public bool ichor; // item applies ichor on hit (melee swings)
        public bool cursedfire;  // item applies cursed flame on hit (melee swings)
        public bool fire; // item applies flame on hit (melee swings)

        public bool thoriumRarity; // item has "Blood Orange" colored rarity, to replicate the rarity from thorium

        public int lifeCost; // cost of using the item, for necrotic ranged/magic/radiant/symphonic weapons

        public int reloadCost; // cost to add 10 to the stack on right click, for necrotic throwing weapons
        
        public int lifeSteal; // life stolen on hit with item (melee swings/tooltip)

        public int healPower; // healing done to an ally on hit (melee swings/tooltip)

        public int summonCost; // base maximum life cost per minion, for necrotic summon weapons

        public int rClickDelay; // internal timer for separating right clicks for 

        public int enchanted; // enchantment level of the item - not yet implemented, may be removed

        public Texture2D glowMask; // texture of glowmask to use for items on the ground and regularly swung items

        public bool rClickCostOnly; // if the item only applies the life cost on right click rather than left click - literally only for one item but there may be more in the future

        // setting default values for each field that resets every tick
        public NecromancyGlobalItem()
        {
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

            lifeCost = 0;

            reloadCost = 0;

            lifeSteal = 0;

            healPower = 0;

            summonCost = 0;

            rClickDelay = 0;

            enchanted = 0;

            glowMask = null;

            rClickCostOnly = false;
        }

        // so items can hold individual data
        public override bool InstancePerEntity
        {
            get { return true; }
        }

        // required for every important field on items that isn't reset every frame - I probably forgot some here
        public override GlobalItem Clone(Item item, Item itemClone)
        {
            NecromancyGlobalItem myClone = (NecromancyGlobalItem)base.Clone(item, itemClone);

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
            
            myClone.lifeCost = lifeCost;

            myClone.reloadCost = reloadCost;

            myClone.lifeSteal = lifeSteal;

            myClone.healPower = healPower;

            myClone.summonCost = summonCost;

            myClone.rClickDelay = rClickDelay;

            myClone.enchanted = enchanted;

            return myClone;
        }

        // damage modifications based on the item's data - enchantment (NYI) would apply here
        public override void GetWeaponDamage(Item item, Player player, ref int damage)
        {
            if (item.GetGlobalItem<NecromancyGlobalItem>(mod).enchanted > 0)
            {
                damage = (int)(damage * (1 + 0.05f * item.GetGlobalItem<NecromancyGlobalItem>(mod).enchanted));
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.GetGlobalItem<NecromancyGlobalItem>(mod).thoriumRarity) // Blood Orange Rarity (Ragnarok tier items)
            {
                tooltips[0].overrideColor = new Color(255, 127, 0);
            }
            if (tooltips.Count > 1) // precaution to avoid errors
            {
                int damageLine = -1;
                
                for (int i = 0; i < tooltips.Count; i++)
                {
                    if (tooltips[i].Name.Contains("Damage"))
                    {
                        damageLine = i; // search for line that says "(number) (damage type) damage"
                        i = tooltips.Count;
                    }
                }
                
                if (damageLine == -1) return; // if there is no "Damage" line

                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic) // necrotic weapons are labelled as "magic" so the game does things like prefixes
                {
                    tooltips[damageLine].text = tooltips[damageLine].text.Replace(" magic damage", " necrotic damage"); // replace 'magic' with 'necrotic'
                }

                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).enchanted > 0) // NYI, adds "Enchanted" to the tooltips
                {
                    tooltips.Insert(damageLine, new TooltipLine(mod, "Necromancy Item Info: Enchant", "Enchanted"));
                    damageLine++;
                    tooltips[damageLine - 1].overrideColor = new Color(1f, 0.2f, 1f);
                }

                if (Config.ExplicitlyShowNecroticSubtypes)
                {
                    // specifies subtype in tooltip, so the player knows how the weapon works
                    // disabled by default so as not to confuse players, but the option is there to see these
                    if (item.GetGlobalItem<NecromancyGlobalItem>(mod).melee)
                    {
                        tooltips[damageLine].text = tooltips[damageLine].text.Replace("necrotic damage", "necrotic melee damage");
                    }
                    if (item.GetGlobalItem<NecromancyGlobalItem>(mod).ranged)
                    {
                        tooltips[damageLine].text = tooltips[damageLine].text.Replace("necrotic damage", "necrotic ranged damage");
                    }
                    if (item.GetGlobalItem<NecromancyGlobalItem>(mod).magic)
                    {
                        tooltips[damageLine].text = tooltips[damageLine].text.Replace("necrotic damage", "necrotic magic damage");
                    }
                    if (item.GetGlobalItem<NecromancyGlobalItem>(mod).summon)
                    {
                        tooltips[damageLine].text = tooltips[damageLine].text.Replace("necrotic damage", "necrotic summon damage");
                    }
                    if (item.GetGlobalItem<NecromancyGlobalItem>(mod).throwing)
                    {
                        tooltips[damageLine].text = tooltips[damageLine].text.Replace("necrotic damage", "necrotic throwing damage");
                    }
                    if (item.GetGlobalItem<NecromancyGlobalItem>(mod).radiant)
                    {
                        tooltips[damageLine].text = tooltips[damageLine].text.Replace("necrotic damage", "necrotic radiant damage");
                    }
                    if (item.GetGlobalItem<NecromancyGlobalItem>(mod).symphonic)
                    {
                        tooltips[damageLine].text = tooltips[damageLine].text.Replace("necrotic damage", "necrotic symphonic damage");
                    }
                }

                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeSteal > 0) // add the lifesteal to the tooltip
                {
                    tooltips.Insert(damageLine + 1, new TooltipLine(mod, "Necromancy Item Info: Lifesteal", "Steals " + Main.player[item.owner].GetModPlayer<NecromancyPlayer>(mod).LifeSteal(item) + " life"));
                }

                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).healPower > 0) // add necrotic radiant healing power to the tooltip
                {
                    tooltips.Insert(damageLine + 1, new TooltipLine(mod, "Necromancy Item Info: Radiant Heal", "Heals allies for " + Necromancy.GetHealPower(Main.player[item.owner], item) + " life"));
                }

                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeCost > 0) // add the life cost to the tooltip
                {
                    string text = "Uses " + Necromancy.GetCost(Main.player[item.owner], item) + " life";
                    if (Main.player[item.owner].GetModPlayer<NecromancyPlayer>().manaAcc && !item.GetGlobalItem<NecromancyGlobalItem>().ranged) // ranged life costs cannot use mana even with the accessory
                    {
                        text += " (" + Necromancy.GetCost(Main.player[item.owner], item) * 15 + " mana)";
                    }
                    tooltips.Insert(damageLine + 1, new TooltipLine(mod, "Necromancy life cost", text));
                }

                // summon max life costs - calculates based on number of summons as well as the sum of their costs
                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).summonCost > 0)
                {
                    Player player = Main.player[item.owner];
                    int summons = Necromancy.CountSummons(player);

                    tooltips.Insert(damageLine + 1, new TooltipLine(mod, "Necromancy Item Info: Summon Cost", "Costs " + item.GetGlobalItem<NecromancyGlobalItem>(mod).summonCost + "x" + summons + " maximum life per minion"));
                    tooltips.Insert(damageLine + 1, new TooltipLine(mod, "Necromancy Item Info: Total Summon Cost", "Current total summon cost: " + player.GetModPlayer<NecromancyPlayer>().totalSummonCost));
                }
                
                if (item.GetGlobalItem<NecromancyGlobalItem>(mod).reloadCost > 0) // add the necrotic throwing refill cost to the tooltip
                {
                    tooltips.Insert(damageLine + 1, new TooltipLine(mod, "Necromancy Item Info: Throwing Cost", "Reload 10 for " + GetReloadCost(item) + " life"));
                }
            }
        }

        public override bool UseItem(Item item, Player player) // using items that don't shoot
        {
            if (item.healLife > 0) Necromancy.HealPlayer(player, player.GetModPlayer<NecromancyPlayer>().bonusPotionHeal, false);
            return base.UseItem(item, player);
        }

        public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) // using items that do shoot
        {
            if (player.itemAnimation > player.itemAnimationMax - item.useTime)
            {
                Necromancy.DoLifeCost(player, item); // only do life cost once per click, so items that shoot several times per click don't do the cost multiple times
            }
            return base.Shoot(item, player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (item.GetGlobalItem<NecromancyGlobalItem>().throwing && item.stack == 1) return false; // cannot use throwing weapons with only one left, so they don't run out
            if (player.GetModPlayer<NecromancyPlayer>().wraithTime > 0) return false; // can't use item while the player is using the Wraith Cloak
            return base.CanUseItem(item, player);
        }

        public override void HoldItem(Item item, Player player) // ticks timer for the right click delay for necrotic throwing refills
        {
            if (item.GetGlobalItem<NecromancyGlobalItem>(mod).rClickDelay > 0)
            {
                item.GetGlobalItem<NecromancyGlobalItem>(mod).rClickDelay--;
            }
        }

        public static int GetReloadCost(Item item)
        {
            Mod mod = ModLoader.GetMod("Necromancy");
            // reload cost is affected by items that reduce or increase life cost
            return Math.Max(1, item.GetGlobalItem<NecromancyGlobalItem>(mod).reloadCost + Main.player[item.owner].GetModPlayer<NecromancyPlayer>(mod).lifeCostModifier);
        }

        public override bool AltFunctionUse(Item item, Player player) // on right click
        {
            // reload on necrotic throwing weapons
            if (item.GetGlobalItem<NecromancyGlobalItem>(mod).rClickDelay == 0 && item.GetGlobalItem<NecromancyGlobalItem>(mod).reloadCost > 0 && item.stack < item.maxStack)
            {
                item.stack = Math.Min(item.stack + 10, item.maxStack);
                Necromancy.BroadcastDrainLife(player, GetReloadCost(item));
            }

            if (item.GetGlobalItem<NecromancyGlobalItem>(mod).rClickDelay == 0 && item.GetGlobalItem<NecromancyGlobalItem>(mod).summonCost > 0) // right click also desummons necrotic summons
            {
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p != null && p.active && p.GetGlobalProjectile<NecromancyGlobalProjectile>().summonCost > 0 && p.owner == player.whoAmI && SummonedFromItem(p, item))
                    {
                        p.Kill();
                        i = Main.projectile.Length;
                    }
                }
            }
            rClickDelay = 3;
            return base.AltFunctionUse(item, player);
        }

        public override bool ConsumeItem(Item item, Player player) // whether or not to consume a consumable
        {
            // reduced chance from empowerment
            if (Main.rand.NextFloat() > player.GetModPlayer<NecromancyPlayer>().ammoConsumeChance) return false;

            // reduced chance from necrotic throwing accessory
            if (player.GetModPlayer<NecromancyPlayer>().throwAcc && item.GetGlobalItem<NecromancyGlobalItem>(mod).throwing)
            {
                return Main.rand.NextFloat() < (float)player.statLife / player.statLifeMax2;
            }
            return base.ConsumeItem(item, player);
        }

        public override bool PreOpenVanillaBag(string context, Player player, int arg) // opening expert boss bags
        {
            if (arg == ItemID.PlanteraBossBag)
            {
                player.QuickSpawnItem(mod.ItemType("AcidSpray"));
            }
            if (arg == ItemID.MoonLordBossBag)
            {
                player.QuickSpawnItem(mod.ItemType("AncientHeptagram"));
            }
            if (arg == ItemID.WallOfFleshBossBag)
            {
                if (Main.rand.NextFloat() < 0.25f)
                {
                    player.QuickSpawnItem(mod.ItemType("NecromancerEmblem"));
                }
                if (Main.rand.NextFloat() < 1/3f)
                {
                    player.QuickSpawnItem(mod.ItemType("GrenadeBow"));
                }
            }
            if (arg == ItemID.FishronBossBag)
            {
                player.QuickSpawnItem(mod.ItemType("SubmarineGun"));
            }

            Mod thorium = ModLoader.GetMod("ThoriumMod");
            if (thorium != null && arg == thorium.ItemType("LichBag"))
            {
                player.QuickSpawnItem(mod.ItemType("LichSoul"));
            }
            else if (thorium != null && arg == thorium.ItemType("BoreanBag"))
            {
                player.QuickSpawnItem(mod.ItemType("TenorDrum"));
            }
            else if (thorium != null && arg == thorium.ItemType("BeholderBag"))
            {
                player.QuickSpawnItem(mod.ItemType("HyperthermalSlicer"));
            }
            else if (thorium != null && arg == thorium.ItemType("AbyssionBag"))
            {
                player.QuickSpawnItem(mod.ItemType("Octobass"));
            }
            return true;
        }

        public bool SummonedFromItem(Projectile p, Item item) // determining if a projectile was created by the given item
        {
            return p.active && p.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom != null && p.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom.type == item.type;
        }

        public override bool WingUpdate(int wings, Player player, bool inUse) // using wings
        {
            if (inUse)
            {
                // for use elsewhere
                player.GetModPlayer<NecromancyPlayer>().wingUse = 2;
            }
            return base.WingUpdate(wings, player, inUse);
        }

        public int ShootTimes(Item item) // number of times Shoot is called per click
        {
            return item.useAnimation / item.useTime;
        }

        public override void PostDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            // drawing glowmasks on items thrown on the ground
            if (item.GetGlobalItem<NecromancyGlobalItem>().glowMask != null)
            {
                Texture2D texture = item.GetGlobalItem<NecromancyGlobalItem>().glowMask;
                spriteBatch.Draw
                (
                    texture,
                    new Vector2
                    (
                        item.position.X - Main.screenPosition.X + item.width * 0.5f,
                        item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                    ),
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White,
                    rotation,
                    texture.Size() * 0.5f,
                    scale,
                    SpriteEffects.None,
                    0f
                );
            }
        }
    }
}