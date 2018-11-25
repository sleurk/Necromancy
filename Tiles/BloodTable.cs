using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Necromancy.Tiles
{
	public class BloodTable : ModTile
	{
        // buff table for necrotic damage
		public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = false;
            Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 18 };
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Blood Table");
            AddMapEntry(new Color(150, 0, 0), name);
			dustType = 7;
            animationFrameHeight = 56;
            disableSmartCursor = true;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 48, 48, mod.ItemType("BloodTable"));
		}

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frame = Main.tileFrame[TileID.BewitchingTable];
        }

        public override void RightClick(int i, int j)
        {
            Main.LocalPlayer.AddBuff(mod.BuffType<Buffs.Favored>(), 36000);
            Main.PlaySound(2, (int)Main.LocalPlayer.position.X, (int)Main.LocalPlayer.position.Y, 25);
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.showItemIcon = true;
            player.showItemIcon2 = mod.ItemType("BloodTable");
        }
    }
}