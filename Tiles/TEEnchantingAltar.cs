using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Tiles
{
    public class TEEnchantingAltar : TEAltar
    {
        public override int? UseChalk(int chalk)
        {
            if (chalk == mod.ItemType<Items.BoneChalk>())
            {
                return mod.ProjectileType<Projectiles.Rituals.Enchantment1>();
            }
            else if (chalk == mod.ItemType<Items.ShadowChalk>())
            {
                return mod.ProjectileType<Projectiles.Rituals.Enchantment2>();
            }
            else if (chalk == mod.ItemType<Items.SoulChalk>())
            {
                return mod.ProjectileType<Projectiles.Rituals.Enchantment3>();
            }
            return 0;
        }

        public override void CreateRitual(Player player, int? projectileType, Item chalk)
        {
            Console.WriteLine("Server: CreateRitual");
            Vector2 pos = new Vector2(Position.X * 16f + 8f, Position.Y * 16f - 64f);
            Item weapon = Necromancy.NearestWeapon(pos, 128f);
            if (projectileType != null && weapon != null)
            {
                chalk.stack--;
                if (Main.netMode == 0)
                {
                    Projectile projectile = Projectile.NewProjectileDirect(pos, Vector2.Zero, (int)projectileType, 0, 0f, player.whoAmI);
                    if (projectile.modProjectile is Projectiles.Rituals.Enchantment)
                    {
                        Projectiles.Rituals.Enchantment e = (Projectiles.Rituals.Enchantment)projectile.modProjectile;
                        e.targetItem = weapon;
                        e.targetPlayer = Main.player[weapon.owner];
                    }
                    projectile.Center = pos;
                    activeRitual = projectile;
                    Main.PlaySound(SoundID.Item46, pos);
                }
                else
                {
                    ModPacket packet = ModLoader.GetMod("Necromancy").GetPacket();
                    packet.Write((byte)NecromancyMessageType.Enchant);
                    packet.WriteVector2(pos);
                    packet.Write((int)projectileType);
                    packet.Write(player.whoAmI);
                    packet.Write(weapon.whoAmI);
                    packet.Write(ID);
                    packet.Send();
                }
            }
        }
    }
}