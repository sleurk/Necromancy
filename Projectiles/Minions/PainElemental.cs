using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Necromancy.Projectiles.Minions
{
	public class PainElemental : HoverShooter
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pain Elemental");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.netImportant = true;
			projectile.width = 64;
			projectile.height = 64;
			Main.projFrames[projectile.type] = 4;
			projectile.friendly = true;
			Main.projPet[projectile.type] = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 3600;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
			inertia = 20f;
			shoot = mod.ProjectileType("Pain");
			shootSpeed = 12f;
		}

		public override void CheckActive()
		{
			Player player = Main.player[projectile.owner];
			NecromancyPlayer modPlayer = player.GetModPlayer<NecromancyPlayer>(mod);
			if (player.dead)
			{
				modPlayer.painSummonPower = 0;
			}
			if (modPlayer.painSummonPower > 0)
			{
				projectile.timeLeft = 2;
			}
		}

		public override void SelectFrame()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter >= 8)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 4;
			}
        }

        public override void CreateDust()
        {
            Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), 0.3f, 0.9f, 0.7f);
        }
    }
}