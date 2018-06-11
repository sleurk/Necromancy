using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Melee
{
    public class Phoebus : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Phoebus");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 81;
            item.crit = 4;
            item.width = 96;
            item.height = 96;
            item.useTime = 13;
            item.useAnimation = 13;
            item.useStyle = 1;
            item.knockBack = 10;
            item.value = 10000;
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).melee = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeSteal = 7;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            for (int i = 0; i < 3; i++)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Fire);
                Main.dust[dust].scale = 1f + Main.rand.NextFloat();
                Main.dust[dust].noGravity = true;
            }
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 240);
        }
    }
}
