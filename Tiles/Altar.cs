using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Necromancy.Tiles
{
    public abstract class Altar : ModTile
    {
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
            Tile tile = Main.tile[i, j];
            int cornerX = i - (tile.frameX / 18) + 1;
            int cornerY = j - (tile.frameY / 18) + 1;
            bool keepGoing = true;
            while (keepGoing)
            {
                if (TEAltar.RClickStatic(cornerX, cornerY))
                {
                    TEAltar te = GetNewTE();
                    te.Place(cornerX, cornerY);
                }
                else
                {
                    keepGoing = false;
                }
            }
        }

        public abstract TEAltar GetNewTE();
        
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

        // returns a chalk item to consume
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
    }
}