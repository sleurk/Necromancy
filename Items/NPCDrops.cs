using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items
{
    public class NPCDrops : GlobalNPC
    {
        /* Separate from NecromancyGlobalNPC for organization purposes */

        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.CultistBoss)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Sigil"), 1);
            }

            if (npc.type == NPCID.MartianSaucer)
            {
                float rand = Main.rand.NextFloat();
                if (rand < 2 / 9f)
                {
                    switch (Main.rand.Next(2))
                    {
                        case 0:
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MagneticPulseGenerator"), 1);
                            break;
                        default:
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ElectricWhip"), 1);
                            break;
                    }
                }
            }

            if (!Main.expertMode && npc.type == NPCID.WallofFlesh)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AncientHeptagram"));
            }

            if (!Main.expertMode && npc.type == NPCID.Plantera)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AcidSpray"));
            }
            if (!Main.expertMode && npc.type == NPCID.WallofFlesh)
            {
                if (Main.rand.NextFloat() < 1 / 8f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("NecromancerEmblem"));
                }
                if (Main.rand.NextFloat() < 1 / 6f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GrenadeBow"));
                }
            }
            if (!Main.expertMode && ModLoader.GetMod("ThoriumMod") != null && npc.type == ModLoader.GetMod("ThoriumMod").NPCType("BoreanStrider"))
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TenorDrum"));
            }
            if (!Main.expertMode && ModLoader.GetMod("ThoriumMod") != null && npc.type == ModLoader.GetMod("ThoriumMod").NPCType("Beholder"))
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HyperthermalSlicer"));
            }
            if (!Main.expertMode && ModLoader.GetMod("ThoriumMod") != null && npc.type == ModLoader.GetMod("ThoriumMod").NPCType("Lich"))
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("LichSoul"));
            }
            if (!Main.expertMode && ModLoader.GetMod("ThoriumMod") != null && npc.type == ModLoader.GetMod("ThoriumMod").NPCType("Abyssion"))
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Octobass"));
            }

            if (npc.type == NPCID.Reaper)
            {
                float rand = Main.rand.NextFloat();
                if (rand < 1 / 10f || (Main.expertMode && rand < 1 / 5f))
                {
                    switch (Main.rand.Next(ModLoader.GetMod("ThoriumMod") != null ? 4 : 3))
                    {
                        case 0:
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GhoulPortal"));
                            break;
                        case 1:
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GhostGlaive"));
                            break;
                        case 2:
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("WraithCloak"));
                            break;
                        default:
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DarkSlidewhistle"));
                            break;
                    }
                }
            }

            if (npc.type == NPCID.Vampire || npc.type == NPCID.VampireBat)
            {
                float rand = Main.rand.NextFloat();
                if (rand < 1 / 100f || (Main.expertMode && rand < 1 / 50f))
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VampireLocket"));
                }
            }

            if (npc.type >= 402 && npc.type <= 429) // Lunar Event Enemies
            {
                float rand = Main.rand.NextFloat();
                if (rand < 1 / 50f || (Main.expertMode && rand < 1 / 25f))
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FragmentWormhole"));
                }
            }

            if (npc.type == NPCID.GiantCursedSkull)
            {
                float rand = Main.rand.NextFloat();
                if (rand < 1 / 20f || (Main.expertMode && rand < 1 / 10f))
                {
                    switch (Main.rand.Next(2))
                    {
                        case 0:
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HarmonyOrb"));
                            break;
                        default:
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DiscordOrb"));
                            break;
                    }
                }
            }

            if (npc.type == NPCID.Mothron)
            {
                if (Main.rand.NextFloat() < 1 / 2f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BrokenHeroTome"));
                }
            }

            if (Main.bloodMoon && Main.rand.NextFloat() < 1 / 2f)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BloodEssence"));
            }

            if (Main.bloodMoon && Main.hardMode && Main.rand.NextFloat() < 1 / 40f)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BloodyMirror"));
            }

            if (npc.type == NPCID.GoblinSummoner)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ShadowBlood"), Main.rand.Next(11, 21));
            }

            if (npc.type > 25 && npc.type < 30 && Main.hardMode) // goblin enemies
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ShadowBlood"), Main.rand.Next(1, 2));
            }

            if (npc.type == NPCID.LavaSlime 
             || npc.type == NPCID.Hellbat
             || npc.type == NPCID.Demon
             || npc.type == NPCID.FireImp)
            {
                if (Main.rand.NextFloat() < 2 / 3f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Brimstone"));
                }
            }

            if (npc.type == NPCID.WallofFlesh && !Main.expertMode)
            {
                float rand = Main.rand.NextFloat();
                if (rand < 1 / 8f || (Main.expertMode && rand < 1 / 4f))
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("NecromancerEmblem"));
                }
                rand = Main.rand.NextFloat();
                if (rand < 1 / 8f || (Main.expertMode && rand < 1 / 4f))
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GrenadeBow"));
                }
            }
        }
    }
}
