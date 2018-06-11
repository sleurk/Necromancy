using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Necromancy.Tiles
{
    public class SoulHarvestAltar : Altar
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(mod.GetTileEntity<TESoulHarvestAltar>().Hook_AfterPlacement, -1, 0, false);
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Soul Harvest Altar");
            AddMapEntry(new Color(60, 60, 60), name);
            dustType = 53;
            animationFrameHeight = 36;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 48, mod.ItemType<Items.Placeable.SoulHarvestAltar>());
            int cornerX = i - (frameX / 18) + 1;
            int cornerY = j - (frameY / 18) + 1;
            mod.GetTileEntity<TESoulHarvestAltar>().Kill(cornerX, cornerY);
        }

        public override TEAltar GetNewTE()
        {
            return new TESoulHarvestAltar();
        }
    }
}