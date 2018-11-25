using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Necromancy.Tiles
{
    public abstract class Altar : ModTile
    {
        public readonly string[] chalkName = { "Invalid", "Bone", "Shadow", "Soul" };

        // base altar - when right clicked with chalk in the player's inventory, it makes a ritual projectile
        // if a ritual projectile exists above the altar, it instead clears it
        // the type of projectile created depends on the altar and the chalk used
        public override void SetDefaults()
        {
            Main.tileNoAttach[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileSolidTop[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
        }

        public override void RightClick(int i, int j)
        {
            Projectile ritual = GetActiveRitual(i, j);
            if (ritual == null)
            {
                int chalk = ConsumeChalk();
                if (chalk > 0) CreateRitual(chalk, i, j);
            }
            else
            {
                if (Main.netMode == 0) ritual.Kill();
                else
                {
                    ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                    packet.Write((byte)NecromancyMessageType.KillProj);
                    packet.Write(ritual.whoAmI);
                    packet.Send();
                }
            }
        }
        
        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            Item c = DetectChalk(player);
            if (c != null)
            {
                int chalk = c.type;
                player.showItemIcon = true;
                player.showItemIcon2 = chalk;
                player.noThrow = 2;
            }
        }
        
        public Item DetectChalk(Player player)
        {
            Item chalk = null;
            for (int k = 0; k < player.inventory.Length; k++)
            {
                if (player.inventory[k].type == mod.ItemType<Items.BoneChalk>()
                 || player.inventory[k].type == mod.ItemType<Items.ShadowChalk>()
                 || player.inventory[k].type == mod.ItemType<Items.SoulChalk>())
                {
                    chalk = player.inventory[k];
                    k = player.inventory.Length;
                }
            }
            return chalk;
        }

        private int ConsumeChalk()
        {
            Item chalk = DetectChalk(Main.LocalPlayer);
            if (chalk == null) return 0;
            if (chalk.type == mod.ItemType("BoneChalk"))
            {
                chalk.stack--;
                return 1;
            }
            else if (chalk.type == mod.ItemType("ShadowChalk"))
            {
                chalk.stack--;
                return 2;
            }
            else if (chalk.type == mod.ItemType("SoulChalk"))
            {
                chalk.stack--;
                return 3;
            }
            return -1;
        }

        private Projectile GetActiveRitual(int i, int j)
        {
            Vector2 projectilePos = GetProjectileCenter(i, j);
            foreach (Projectile proj in Main.projectile)
            {
                if (proj != null && proj.active && proj.modProjectile is Projectiles.Rituals.Ritual
                 && Math.Abs(projectilePos.X - proj.Center.X) < 32f && Math.Abs(projectilePos.Y - proj.Center.Y) < 32f)
                {
                    return proj;
                }
            }
            return null;
        }

        // to get exact location to spawn projectile
        public Vector2 GetProjectilePosition(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            int cornerX = i - (tile.frameX / 18) + 1;
            int cornerY = j - (tile.frameY / 18) + 1;

            return new Vector2(16f * (cornerX + 1), 16f * (cornerY - 4)) + new Vector2(-8f, 0f);
        }

        // to find existing projectile - not very accurate
        public Vector2 GetProjectileCenter(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            int cornerX = i - (tile.frameX / 18) + 1;
            int cornerY = j - (tile.frameY / 18) + 1;
            Vector2 tilePos = new Vector2(cornerX, cornerY) * 16f;
            return tilePos + new Vector2(24f, -56f);
        }

        public abstract bool CreateRitual(int chalk, int i, int j);
    }
}