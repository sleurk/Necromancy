using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Items.Placeable
{
	public class BloodAlchemyStation : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Alchemy Station");
            Tooltip.SetDefault("Uses 50 life per craft" +
                    "\n50% chance to not consume potion crafting ingredients" +
                    "\nCan also make necromancer's potions" +
                    "\nCapable of more advanced alchemy and forging rare trinkets");
        }

        public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.maxStack = 99;
            item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 2;
            item.createTile = mod.TileType("BloodAlchemyStation");
		}

        public override void AddRecipes()
		{
            ModRecipe tableRecipe = new ModRecipe(mod);
            tableRecipe.AddIngredient(ItemID.AlchemyTable);
            tableRecipe.AddIngredient(mod, "BloodEssence", 15);
            tableRecipe.AddIngredient(mod, "BeatingHeart");
            tableRecipe.AddTile(TileID.Anvils);
            tableRecipe.SetResult(this);
            tableRecipe.AddRecipe();

            foreach (Recipe recipe in Main.recipe)
            {
                int index = Array.IndexOf(recipe.requiredTile, TileID.Bottles);
                if (index > -1)
                {
                    BloodAlchemyRecipe newRecipe = new BloodAlchemyRecipe(mod);
                    newRecipe.requiredItem = recipe.requiredItem;
                    newRecipe.AddTile(mod, "BloodAlchemyStation");
                    newRecipe.createItem = recipe.createItem;
                    newRecipe.AddRecipe();
                }
            }

            BloodAlchemyRecipe bRecipe;
            /* Advanced Alchemy */

            // Ash and Snow to Sand
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.SnowBlock, 5);
            bRecipe.AddIngredient(ItemID.AshBlock, 5);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.SandBlock, 10);
            bRecipe.AddRecipe();

            // Better Glass Smelting
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.SandBlock, 5);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.Glass, 20);
            bRecipe.AddRecipe();

            // Copper/Tin to Hellstone
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.CopperOre, 12);
            bRecipe.AddIngredient(mod, "Brimstone", 3);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.Hellstone, 12);
            bRecipe.AddRecipe();

            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.TinOre, 12);
            bRecipe.AddIngredient(mod, "Brimstone", 3);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.Hellstone, 12);
            bRecipe.AddRecipe();

            // Hellstone to Chlorophyte
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.Hellstone, 12);
            bRecipe.AddIngredient(mod, "LivingHeart");
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.ChlorophyteOre, 12);
            bRecipe.AddRecipe();

            // Seeds to Strange Plants
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.FireblossomSeeds, 5);
            bRecipe.AddIngredient(ItemID.BlinkrootSeeds, 5);
            bRecipe.AddIngredient(ItemID.DaybloomSeeds, 5);
            bRecipe.AddIngredient(ItemID.MoonglowSeeds, 5);
            bRecipe.AddIngredient(ItemID.ShiverthornSeeds, 5);
            bRecipe.AddIngredient(ItemID.WaterleafSeeds, 5);
            bRecipe.AddIngredient(ItemID.DeathweedSeeds, 5);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.StrangePlant1);
            bRecipe.AddRecipe();


            /* Artifacts (Mimics) */
            // Dual Hook
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.Emerald, 5);
            bRecipe.AddIngredient(ItemID.SoulofFlight, 5);
            bRecipe.AddIngredient(ItemID.Chain, 10);
            bRecipe.AddIngredient(ItemID.CobaltBar, 10);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.DualHook);
            bRecipe.AddRecipe();
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.Emerald, 5);
            bRecipe.AddIngredient(ItemID.SoulofFlight, 5);
            bRecipe.AddIngredient(ItemID.Chain, 10);
            bRecipe.AddIngredient(ItemID.PalladiumBar, 10);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.DualHook);
            bRecipe.AddRecipe();

            // Magic Dagger
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.Topaz, 5);
            bRecipe.AddIngredient(ItemID.SoulofLight, 5);
            bRecipe.AddIngredient(ItemID.GoldBar, 10);
            bRecipe.AddIngredient(ItemID.CobaltBar, 10);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.MagicDagger);
            bRecipe.AddRecipe();
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.Topaz, 5);
            bRecipe.AddIngredient(ItemID.SoulofLight, 5);
            bRecipe.AddIngredient(ItemID.GoldBar, 10);
            bRecipe.AddIngredient(ItemID.PalladiumBar, 10);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.MagicDagger);
            bRecipe.AddRecipe();

            // Star Cloak
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.Amethyst, 5);
            bRecipe.AddIngredient(ItemID.SoulofNight, 5);
            bRecipe.AddIngredient(ItemID.Silk, 10);
            bRecipe.AddIngredient(ItemID.MythrilBar, 10);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.StarCloak);
            bRecipe.AddRecipe();
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.Amethyst, 5);
            bRecipe.AddIngredient(ItemID.SoulofNight, 5);
            bRecipe.AddIngredient(ItemID.Silk, 10);
            bRecipe.AddIngredient(ItemID.OrichalcumBar, 10);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.StarCloak);
            bRecipe.AddRecipe();

            // Titan Glove
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.Sapphire, 5);
            bRecipe.AddIngredient(ItemID.SoulofFlight, 5);
            bRecipe.AddIngredient(ItemID.Silk, 10);
            bRecipe.AddIngredient(ItemID.MythrilBar, 10);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.TitanGlove);
            bRecipe.AddRecipe();
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.Sapphire, 5);
            bRecipe.AddIngredient(ItemID.SoulofFlight, 5);
            bRecipe.AddIngredient(ItemID.Silk, 10);
            bRecipe.AddIngredient(ItemID.OrichalcumBar, 10);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.TitanGlove);
            bRecipe.AddRecipe();

            // Cross Necklace
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.Diamond, 5);
            bRecipe.AddIngredient(ItemID.SoulofLight, 5);
            bRecipe.AddIngredient(ItemID.SilverBar, 10);
            bRecipe.AddIngredient(ItemID.AdamantiteBar, 10);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.CrossNecklace);
            bRecipe.AddRecipe();
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.Diamond, 5);
            bRecipe.AddIngredient(ItemID.SoulofLight, 5);
            bRecipe.AddIngredient(ItemID.SilverBar, 10);
            bRecipe.AddIngredient(ItemID.TitaniumBar, 10);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.CrossNecklace);
            bRecipe.AddRecipe();

            // Philosopher's Stone
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.Ruby, 5);
            bRecipe.AddIngredient(ItemID.SoulofNight, 5);
            bRecipe.AddIngredient(mod, "BloodEssence", 10);
            bRecipe.AddIngredient(ItemID.AdamantiteBar, 10);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.PhilosophersStone);
            bRecipe.AddRecipe();
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.Ruby, 5);
            bRecipe.AddIngredient(ItemID.SoulofNight, 5);
            bRecipe.AddIngredient(mod, "BloodEssence", 10);
            bRecipe.AddIngredient(ItemID.TitaniumBar, 10);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.PhilosophersStone);
            bRecipe.AddRecipe();


            /* Artifacts (Ice Mimics) */
            // Frostbrand
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.Amber, 5);
            bRecipe.AddIngredient(ItemID.FrostCore);
            bRecipe.AddIngredient(ItemID.FieryGreatsword);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.Frostbrand);
            bRecipe.AddRecipe();

            // Ice Bow
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.Amber, 5);
            bRecipe.AddIngredient(ItemID.FrostCore);
            bRecipe.AddIngredient(ItemID.MoltenFury);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.IceBow);
            bRecipe.AddRecipe();

            // Flower of Frost
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.Amber, 5);
            bRecipe.AddIngredient(ItemID.FrostCore);
            bRecipe.AddIngredient(ItemID.FlowerofFire);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.FlowerofFrost);
            bRecipe.AddRecipe();


            /* Artifacts (Corrupt Mimics) */
            // Dart Rifle
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.SoulofNight, 30);
            bRecipe.AddIngredient(ItemID.CursedFlame, 10);
            bRecipe.AddIngredient(ItemID.Musket);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.DartRifle);
            bRecipe.AddRecipe();

            // Chain Guillotines
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.SoulofNight, 30);
            bRecipe.AddIngredient(ItemID.CursedFlame, 10);
            bRecipe.AddIngredient(ItemID.BallOHurt);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.ChainGuillotines);
            bRecipe.AddRecipe();

            // Clinger Staff
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.SoulofNight, 30);
            bRecipe.AddIngredient(ItemID.CursedFlame, 10);
            bRecipe.AddIngredient(ItemID.Vilethorn);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.ClingerStaff);
            bRecipe.AddRecipe();

            // Worm Hook
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.SoulofNight, 30);
            bRecipe.AddIngredient(ItemID.CursedFlame, 10);
            bRecipe.AddIngredient(ItemID.DualHook);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.WormHook);
            bRecipe.AddRecipe();


            /* Artifacts (Crimson Mimics) */
            // Dart Pistol
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.SoulofNight, 30);
            bRecipe.AddIngredient(ItemID.Ichor, 10);
            bRecipe.AddIngredient(ItemID.TheUndertaker);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.DartPistol);
            bRecipe.AddRecipe();

            // Fetid Baghnakhs
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.SoulofNight, 30);
            bRecipe.AddIngredient(ItemID.Ichor, 10);
            bRecipe.AddIngredient(ItemID.TheMeatball);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.FetidBaghnakhs);
            bRecipe.AddRecipe();

            // Life Drain
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.SoulofNight, 30);
            bRecipe.AddIngredient(ItemID.Ichor, 10);
            bRecipe.AddIngredient(ItemID.CrimsonRod);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.SoulDrain);
            bRecipe.AddRecipe();

            // Tendon Hook
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.SoulofNight, 30);
            bRecipe.AddIngredient(ItemID.Ichor, 10);
            bRecipe.AddIngredient(ItemID.DualHook);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.TendonHook);
            bRecipe.AddRecipe();


            /* Artifacts (Hallowed Mimics) */
            // Daedalus Stormbow
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.SoulofLight, 30);
            bRecipe.AddIngredient(ItemID.CrystalShard, 10);
            bRecipe.AddIngredient(ItemID.HellwingBow);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.DaedalusStormbow);
            bRecipe.AddRecipe();

            // Flying Knife
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.SoulofLight, 30);
            bRecipe.AddIngredient(ItemID.CrystalShard, 10);
            bRecipe.AddIngredient(ItemID.DarkLance);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.FlyingKnife);
            bRecipe.AddRecipe();

            // Crystal Vile Shard
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.SoulofLight, 30);
            bRecipe.AddIngredient(ItemID.CrystalShard, 10);
            bRecipe.AddIngredient(ItemID.Flamelash);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.CrystalVileShard);
            bRecipe.AddRecipe();

            // Illuminant Hook
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(ItemID.SoulofLight, 30);
            bRecipe.AddIngredient(ItemID.CrystalShard, 10);
            bRecipe.AddIngredient(ItemID.DualHook);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.IlluminantHook);
            bRecipe.AddRecipe();


            /* Other Artifacts */
            // Sanguine Contract
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(mod, "Brimstone", 5);
            bRecipe.AddIngredient(mod, "BloodEssence", 20);
            bRecipe.AddIngredient(mod, "Parchment");
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(mod, "SanguineContract");
            bRecipe.AddRecipe();

            // Astral Contract
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(mod, "Brimstone", 5);
            bRecipe.AddIngredient(ItemID.FallenStar, 20);
            bRecipe.AddIngredient(mod, "Parchment");
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(mod, "AstralContract");
            bRecipe.AddRecipe();

            // Money Trough
            bRecipe = new BloodAlchemyRecipe(mod);
            bRecipe.AddIngredient(mod, "BloodEssence", 50);
            bRecipe.AddTile(mod, "BloodAlchemyStation");
            bRecipe.SetResult(ItemID.MoneyTrough);
            bRecipe.AddRecipe();
        }
    }
}