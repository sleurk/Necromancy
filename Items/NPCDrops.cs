using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items
{
    public class NPCDrops : GlobalNPC
    {
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

            if (!Main.expertMode && ModLoader.GetMod("ThoriumMod") != null && npc.type == ModLoader.GetMod("ThoriumMod").NPCType("BoreanStrider"))
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TenorDrum"), 1);
            }
            if (!Main.expertMode && ModLoader.GetMod("ThoriumMod") != null && npc.type == ModLoader.GetMod("ThoriumMod").NPCType("Beholder"))
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HyperthermalSlicer"), 1);
            }
            if (!Main.expertMode && ModLoader.GetMod("ThoriumMod") != null && npc.type == ModLoader.GetMod("ThoriumMod").NPCType("Lich"))
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("LichSoul"), 1);
            }
            if (!Main.expertMode && ModLoader.GetMod("ThoriumMod") != null && npc.type == ModLoader.GetMod("ThoriumMod").NPCType("Abyssion"))
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Octobass"), 1);
            }

            if (npc.type == NPCID.Reaper)
            {
                float rand = Main.rand.NextFloat();
                if (rand < 0.05f || (Main.expertMode && rand < 0.1f))
                {
                    switch (Main.rand.Next(4))
                    {
                        case 0:
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DarkSlidewhistle"), 1);
                            break;
                        case 1:
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GhostGlaive"), Main.rand.Next(45, 60));
                            break;
                        case 2:
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("WraithCloak"), 1);
                            break;
                        default:
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GhoulPortal"), 1);
                            break;
                    }
                }
            }

            if (npc.type == NPCID.Vampire || npc.type == NPCID.VampireBat)
            {
                float rand = Main.rand.NextFloat();
                if (rand < 0.01f || (Main.expertMode && rand < 0.02f))
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VampireLocket"), 1);
                }
            }

            if (npc.type >= 402 && npc.type <= 429) // Lunar Event Enemies
            {
                float rand = Main.rand.NextFloat();
                if (rand < 0.1f || (Main.expertMode && rand < 0.15f))
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FragmentWormhole"), 1);
                }
            }

            if (npc.type == NPCID.GiantCursedSkull)
            {
                float rand = Main.rand.NextFloat();
                if (rand < 0.05f || (Main.expertMode && rand < 0.1f))
                {
                    switch (Main.rand.Next(2))
                    {
                        case 0:
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HarmonyOrb"), 1);
                            break;
                        default:
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DiscordOrb"), 1);
                            break;
                    }
                }
            }

            if (npc.type == NPCID.Mothron)
            {
                if (Main.rand.NextFloat() < 0.5f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BrokenHeroTome"), 1);
                }
            }

            if (npc.type == NPCID.BloodZombie || npc.type == NPCID.Drippler)
            {
                if (Main.rand.NextFloat() < 0.5f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BloodEssence"), 1);
                }

                if (Main.hardMode && Main.rand.NextFloat() < .05f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BloodyMirror"), 1);
                }
            }

            if (npc.type == NPCID.GoblinSummoner)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ShadowBlood"), Main.rand.Next(11, 21));
            }

            if (npc.type > 25 && npc.type < 30 && Main.expertMode) // goblin enemies
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ShadowBlood"), Main.rand.Next(1, 2));
            }

            if (npc.type == NPCID.LavaSlime 
             || npc.type == NPCID.Hellbat
             || npc.type == NPCID.Demon
             || npc.type == NPCID.FireImp)
            {
                if (Main.rand.NextFloat () < 2/3f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Brimstone"), 1);
                }
            }

            if (npc.type == NPCID.WallofFlesh && !Main.expertMode)
            {
                float rand = Main.rand.NextFloat();
                if (rand < 0.125f || (Main.expertMode && rand < 0.25f))
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("NecromancerEmblem"), 1);
                }
                rand = Main.rand.Next(8);
                if (rand < 1 || (Main.expertMode && rand < 2))
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GrenadeBow"), 1);
                }
            }
        }
    }
}
