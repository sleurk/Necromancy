using System.Linq;
using Terraria;
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
            return NPC.downedPirates;
		}

		public override bool CheckConditions(int left, int right, int top, int bottom)
		{
            int score = 0;
            for (int x = left - 1; x <= right + 1; x++)
            {
                int type = Main.tile[x, top - 1].type;
                if (type != TileID.GoldBrick && type != TileID.PlatinumBrick)
                {
                    // Main.NewText("Missing tile at (" + x + ", " + top + ")");
                    score--;
                }
                type = Main.tile[x, bottom + 1].type;
                if (type != TileID.GoldBrick && type != TileID.PlatinumBrick)
                {
                    // Main.NewText("Missing tile at (" + x + ", " + bottom + ")");
                    score--;
                }
            }

            for (int y = top - 1; y <= bottom + 1; y++)
            {
                int type = Main.tile[left - 1, y].type;
                if (type != TileID.GoldBrick && type != TileID.PlatinumBrick)
                {
                    // Main.NewText("Missing tile at (" + left + ", " + y + ")");
                    score--;
                }
                type = Main.tile[right + 1, y].type;
                if (type != TileID.GoldBrick && type != TileID.PlatinumBrick)
                {
                    score--;
                    // Main.NewText("Missing tile at (" + right + ", " + y + ")");
                }
            }

            for (int x = left; x <= right; x++)
            {
                for (int y = top; y <= bottom; y++)
				{
					if (Main.tile[x, y].wall != WallID.GoldBrick && Main.tile[x,y].wall != WallID.PlatinumBrick)
					{
                        // Main.NewText("Missing wall at (" + x + ", " + y + ")");
                        score--;
					}
				}
			}
            // Main.NewText(score + "(" + (score > -10) + ")");
			return score > -10;
		}

		public override string TownNPCName()
		{
			switch (WorldGen.genRand.Next(5))
			{
				case 0:
					return "Freddie";
                case 1:
                    return "Nicolas";
                case 2:
                    return "Jonathan";
                case 3:
                    return "Brian";
                default:
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
			button = Lang.inter[28].Value;
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
			shop.item[nextSlot].SetDefaults(mod.ItemType("GoldenLute"));
			nextSlot++;
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
    }
}