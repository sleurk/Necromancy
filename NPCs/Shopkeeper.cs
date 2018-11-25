using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Necromancy.NPCs
{
	[AutoloadHead]
	public class Shopkeeper : ModNPC
	{
		public override string Texture
		{
			get
			{
				return "Necromancy/NPCs/Shopkeeper";
			}
		}

		public override bool Autoload(ref string name)
		{
			name = "Shopkeeper";
			return mod.Properties.Autoload;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shopkeeper");
			Main.npcFrameCount[npc.type] = 26;
			NPCID.Sets.ExtraFramesCount[npc.type] = 9;
			NPCID.Sets.AttackFrameCount[npc.type] = 5;
			NPCID.Sets.DangerDetectRange[npc.type] = 700;
			NPCID.Sets.AttackType[npc.type] = 0;
			NPCID.Sets.AttackTime[npc.type] = 90;
			NPCID.Sets.AttackAverageChance[npc.type] = 30;
			NPCID.Sets.HatOffsetY[npc.type] = -2;
		}

		public override void SetDefaults()
		{
			npc.townNPC = true;
			npc.friendly = true;
			npc.width = 18;
			npc.height = 40;
			npc.aiStyle = 7;
			npc.damage = 10;
			npc.defense = 15;
			npc.lifeMax = 250;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.knockBackResist = 0.5f;
			animationType = NPCID.Guide;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			int num = npc.life > 0 ? 1 : 5;
			for (int k = 0; k < num; k++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood);
			}
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            // Spawns after pirates have been defeated
            return NPC.downedPirates;
		}
        
        // Valid house is made out of mostly Platinum/Gold bricks
		public override bool CheckConditions(int left, int right, int top, int bottom)
		{
            int score = 0;
            for (int x = left - 1; x <= right + 1; x++)
            {
                int type = Main.tile[x, top - 1].type;
                if (type != TileID.GoldBrick && type != TileID.PlatinumBrick)
                {
                    score--;
                }
                type = Main.tile[x, bottom + 1].type;
                if (type != TileID.GoldBrick && type != TileID.PlatinumBrick)
                {
                    score--;
                }
            }

            for (int y = top - 1; y <= bottom + 1; y++)
            {
                int type = Main.tile[left - 1, y].type;
                if (type != TileID.GoldBrick && type != TileID.PlatinumBrick)
                {
                    score--;
                }
                type = Main.tile[right + 1, y].type;
                if (type != TileID.GoldBrick && type != TileID.PlatinumBrick)
                {
                    score--;
                }
            }

            for (int x = left; x <= right; x++)
            {
                for (int y = top; y <= bottom; y++)
				{
					if (Main.tile[x, y].wall != WallID.GoldBrick && Main.tile[x,y].wall != WallID.PlatinumBrick)
					{
                        score--;
					}
				}
			}
			return score > -10;
        }

        public override string TownNPCName()
		{
			switch (WorldGen.genRand.Next(4))
			{
				default:
					return "Ryan";
                case 1:
                    return "Nicolas";
                case 2:
                    return "Freddie";
                case 3:
					return "Freddie";
			}
		}

		public override string GetChat()
		{
			if (Main.LocalPlayer.GetModPlayer<NecromancyPlayer>().necrodancer && Main.rand.Next(5) == 0)
			{
				return "Nice beard.";
			}
			switch (Main.rand.Next(4))
			{
				case 0:
					return "I've recently lost weight!";
                case 1:
                    return "You know, I'm a pretty good singer.";
                case 2:
                    return "At least those pirates could enjoy music. It's as quiet as a graveyard here!";
                default:
					return "You seem like the kind of fellow who can dance.";
			}
		}

		public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
        }

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if (firstButton)
			{
				shop = true;
			}
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
        {
            // Summon weapons aren't implemented yet because I still haven't fully figured them out
            switch (GetZone())
            {
                case 6:
                    {
                        shop.item[nextSlot].SetDefaults(mod.ItemType("StaticFlail"));
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(mod.ItemType("ArcBow"));
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(mod.ItemType("PylonHarmonizer"));
                        nextSlot++;
                        /*
                        shop.item[nextSlot].SetDefaults(mod.ItemType("StormCaller"));
                        nextSlot++;
                        */
                        shop.item[nextSlot].SetDefaults(mod.ItemType("BoltAxe"));
                        nextSlot++;
                        /*
                        if (ModLoader.GetMod("ThoriumMod") != null)
                        {
                            shop.item[nextSlot].SetDefaults(mod.ItemType("Thunderbolt"));
                            nextSlot++;
                            shop.item[nextSlot].SetDefaults(mod.ItemType("ElectricGuitar"));
                            nextSlot++;
                        }
                        */
                        shop.item[nextSlot].SetDefaults(mod.ItemType("LightningShock"));
                        nextSlot++;
                        break;
                    }
                case 5:
                    {
                        shop.item[nextSlot].SetDefaults(mod.ItemType("AcidDisc"));
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(mod.ItemType("ToxicCannon"));
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(mod.ItemType("SlimeCrystal"));
                        nextSlot++;
                        /*
                        shop.item[nextSlot].SetDefaults(mod.ItemType("GreenSwarm"));
                        nextSlot++;
                        */
                        shop.item[nextSlot].SetDefaults(mod.ItemType("GunkCapsule"));
                        nextSlot++;
                        /*
                        if (ModLoader.GetMod("ThoriumMod") != null)
                        {
                            shop.item[nextSlot].SetDefaults(mod.ItemType("Ooze"));
                            nextSlot++;
                            shop.item[nextSlot].SetDefaults(mod.ItemType("Goong"));
                            nextSlot++;
                        }
                        */
                        shop.item[nextSlot].SetDefaults(mod.ItemType("GooRod"));
                        nextSlot++;
                        break;
                    }
                case 4:
                    {
                        shop.item[nextSlot].SetDefaults(mod.ItemType("IceWhip"));
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(mod.ItemType("FrozenRailgun"));
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(mod.ItemType("GlacialTome"));
                        nextSlot++;
                        /*
                        shop.item[nextSlot].SetDefaults(mod.ItemType("ArcticTablet"));
                        nextSlot++;
                        */
                        shop.item[nextSlot].SetDefaults(mod.ItemType("Frostbite"));
                        nextSlot++;
                        /*
                        if (ModLoader.GetMod("ThoriumMod") != null)
                        {
                            shop.item[nextSlot].SetDefaults(mod.ItemType("SnowShock"));
                            nextSlot++;
                            shop.item[nextSlot].SetDefaults(mod.ItemType("ChillingHarmonica"));
                            nextSlot++;
                        }
                        */
                        shop.item[nextSlot].SetDefaults(mod.ItemType("CryonicJavelin"));
                        shop.item[nextSlot].value = Item.buyPrice(1);
                        nextSlot++;
                        break;
                    }
                case 3:
                    {
                        shop.item[nextSlot].SetDefaults(mod.ItemType("Magmatica"));
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(mod.ItemType("InfernalBlaster"));
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(mod.ItemType("VolcanicEruption"));
                        nextSlot++;
                        /*
                        shop.item[nextSlot].SetDefaults(mod.ItemType("Firewall"));
                        nextSlot++;
                        */
                        shop.item[nextSlot].SetDefaults(mod.ItemType("SearedDagger"));
                        nextSlot++;
                        /*
                        if (ModLoader.GetMod("ThoriumMod") != null)
                        {
                            shop.item[nextSlot].SetDefaults(mod.ItemType("MeteorEye"));
                            nextSlot++;
                            shop.item[nextSlot].SetDefaults(mod.ItemType("Martin"));
                            nextSlot++;
                        }
                        */
                        shop.item[nextSlot].SetDefaults(mod.ItemType("DevilTrident"));
                        nextSlot++;
                        break;
                    }
                case 2:
                    {
                        shop.item[nextSlot].SetDefaults(mod.ItemType("LivingBlade"));
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(mod.ItemType("Chloroflamethrower"));
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(mod.ItemType("FungalGrasp"));
                        nextSlot++;
                        /*
                        shop.item[nextSlot].SetDefaults(mod.ItemType("SporeStaff"));
                        nextSlot++;
                        */
                        shop.item[nextSlot].SetDefaults(mod.ItemType("JungleThorn"));
                        nextSlot++;
                        /*
                        if (ModLoader.GetMod("ThoriumMod") != null)
                        {
                            shop.item[nextSlot].SetDefaults(mod.ItemType("Bioluminescence"));
                            nextSlot++;
                            shop.item[nextSlot].SetDefaults(mod.ItemType("VerdantViola"));
                            nextSlot++;
                        }
                        */
                        shop.item[nextSlot].SetDefaults(mod.ItemType("GaiaGatlingGun"));
                        nextSlot++;
                        break;
                    }
                case 1:
                    {
                        shop.item[nextSlot].SetDefaults(mod.ItemType("SanguineLongsword"));
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(mod.ItemType("Blooderbuss"));
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(mod.ItemType("Cruor"));
                        nextSlot++;
                        /*
                        shop.item[nextSlot].SetDefaults(mod.ItemType("?"));
                        nextSlot++;
                        */
                        shop.item[nextSlot].SetDefaults(mod.ItemType("RedStar"));
                        nextSlot++;
                        /*
                        if (ModLoader.GetMod("ThoriumMod") != null)
                        {
                            shop.item[nextSlot].SetDefaults(mod.ItemType("Plasm"));
                            nextSlot++;
                            shop.item[nextSlot].SetDefaults(mod.ItemType("Hemoharp"));
                            nextSlot++;
                        }
                        */
                        shop.item[nextSlot].SetDefaults(mod.ItemType("CrimsonFamiliar"));
                        nextSlot++;
                        break;
                    }
            }
            
            // Adds Golden Lute to the next open row if player is wearing full Necrodancer armor
            if (ModLoader.GetMod("ThoriumMod") != null && Main.LocalPlayer.GetModPlayer<NecromancyPlayer>().necrodancer)
            {
                nextSlot = Math.Min(shop.item.Length - 1, (nextSlot + 9) - (nextSlot + 9) % 10);
                shop.item[nextSlot].SetDefaults(mod.ItemType("GoldenLute"));
                nextSlot++;
            }
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 20;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 10;
			randExtraCooldown = 10;
		}

		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
		{
			projType = mod.ProjectileType("CelestialNote");
			attackDelay = 1;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{
			multiplier = 12f;
			randomOffset = 2f;
		}

        // Different items depending on what biome he is in; minimum boss requirements for each
        public int GetZone()
        {
            if (NPC.downedGolemBoss && TempleWalls())
            {
                return 6;
            }
            else if (NPC.downedPlantBoss && Main.dungeonTiles >= 100)
            {
                return 5;
            }
            else if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && Main.LocalPlayer.ZoneSnow)
            {
                return 4;
            }
            else if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && Main.LocalPlayer.ZoneUnderworldHeight)
            {
                return 3;
            }
            else if (NPC.downedMechBossAny && Main.LocalPlayer.ZoneJungle)
            {
                return 2;
            }
            else if (Main.LocalPlayer.ZoneDirtLayerHeight)
            {
                return 1;
            }
            return 0;
        }

        // if he is close to the temple
        private bool TempleWalls()
        {
            Player player = Main.LocalPlayer;
            for (int i = -20; i <= 20; i++)
            {
                for (int j = -20; j <= 20; j++)
                {
                    int x = (int)player.Center.X / 16 + i;
                    int y = (int)player.Center.Y / 16 + j;
                    if (x >= 0 && y >= 0 && Main.tile[x, y].wall == WallID.LihzahrdBrickUnsafe) return true;
                }
            }
            return false;
        }
    }
}