using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Tiles
{
    public abstract class TEAltar : ModTileEntity
    {
        public Projectile activeRitual;

        public override void Update()
        {
            if (activeRitual != null && activeRitual.active)
            {
                activeRitual.timeLeft = 2;
            }
        }

        public override bool ValidTile(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            return tile.active() &&
                (tile.type == mod.TileType<AgitationAltar>()
                || tile.type == mod.TileType<ProtectionAltar>()
                || tile.type == mod.TileType<EnchantingAltar>()
                || tile.type == mod.TileType<SoulHarvestAltar>()
                || tile.type == mod.TileType<MeteorShowerAltar>()
                || tile.type == mod.TileType<ResurrectAltar>());
        }

        public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
        {
            // i - 1 and j - 2 come from the fact that the origin of the tile is "new Point16(1, 2);", so we need to pass the coordinates back to the top left tile. If using a vanilla TileObjectData.Style, make sure you know the origin value.
            if (Main.netMode == 1)
            {
                NetMessage.SendTileSquare(Main.myPlayer, i - 1, j - 1, 3); // this is -1, -1, however, because -1, -1 places the 3 diameter square over all the tiles, which are sent to other clients as an update.
                NetMessage.SendData(87, -1, -1, null, i - 1, j - 2, Type, 0f, 0, 0, 0);
                return -1;
            }
            return Place(i, j);
        }

        public void RClick()
        {
            if (activeRitual != null && activeRitual.active)
            {
                if (Main.netMode == 1)
                {
                    ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                    packet.Write((byte)NecromancyMessageType.RitualCancel);
                    packet.Write(ID);
                    packet.Send();
                }
                else
                {
                    activeRitual.Kill();
                    activeRitual.netUpdate = true;
                    activeRitual = null;
                }
            }
            else
            {
                Player player = Main.LocalPlayer;
                Item use = DetectChalk(player);
                if (use != null)
                {
                    Vector2 pos = new Vector2(Position.X * 16f + 8f, Position.Y * 16f - 64f);
                    int? ritual = UseChalk(use.type);
                    if (ritual != null)
                    {
                        CreateRitual(player, ritual, use);
                    }
                }
            }
        }

        // returns ritual projectile type
        public abstract int? UseChalk(int chalk);

        public static bool RClickStatic(int i, int j)
        {
            try
            {
                TileEntity te = ByPosition[new Point16(i, j)];
                if (te is TEAltar)
                {
                    ((TEAltar)(te)).RClick();
                }
            }
            catch (Exception e)
            {
                return true;
            }
            return false;
        }

        // creates the projectile at the right place
        public virtual void CreateRitual(Player player, int? projectileType, Item chalk)
        {
            Vector2 pos = new Vector2(Position.X * 16f + 8f, Position.Y * 16f - 64f);
            if (projectileType != null)
            {
                chalk.stack--;
                Projectile projectile = Projectile.NewProjectileDirect(pos, Vector2.Zero, (int)projectileType, 0, 0f, player.whoAmI);
                projectile.Center = pos;
                activeRitual = projectile;
                Main.PlaySound(SoundID.Item46, pos);
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