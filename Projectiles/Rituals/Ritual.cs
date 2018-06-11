using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles.Rituals
{
	public abstract class Ritual : ModProjectile
	{
        private float yOffset = 0f;
        protected int power = 0;
        protected int dustType = 0;

        protected int timer = -1;

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 64;
			projectile.height = 64;
			projectile.timeLeft = 2;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.netImportant = true;
            Config();
        }

        public override void AI()
        {
            if (timer == -1)
            {
                timer = 36000;
            }
            timer--;
            if (timer == 0)
            {
                projectile.Kill();
            }
            Tick();
            if (ValidTile()) projectile.timeLeft = 2;
            projectile.alpha = (int)(60 * Math.Sin(projectile.timeLeft / 25f)) + 160;
            int x = (int)(projectile.Center.X / 16f);
            int y = (int)(projectile.Center.Y / 16f);
            Vector2 offset = new Vector2(0, yOffset);
            projectile.netUpdate = true;
            for (int i = 0; i < 3; i++)
            {
                Vector2 dustPos = projectile.position + offset + new Vector2(0, projectile.height / -2f);
                Dust d = Dust.NewDustDirect(dustPos, projectile.width, projectile.height * 2, dustType);
                d.noGravity = true;
                d.velocity = new Vector2(Main.rand.NextFloat(-2f, 2f), 0f);
                dustPos = new Vector2(Main.rand.NextFloat(-28f, 28f), 48f) + projectile.Center + offset;
                d = Dust.NewDustPerfect(dustPos, dustType);
                d.noGravity = true;
                d.velocity = new Vector2(0, Main.rand.NextFloat(0f, -4f));
                dustPos = new Vector2(Main.rand.NextFloat(-28f, 28f), 48f) + projectile.Center + offset;
                d = Dust.NewDustPerfect(dustPos, dustType);
                d.scale = Main.rand.NextFloat(1f, 1.3f);
                d.noGravity = true;
                d.velocity = new Vector2(0, 0);
            }
            projectile.velocity.Y = (float)Math.Sin(MathHelper.ToRadians(timer)) / 10f;
            yOffset -= projectile.velocity.Y;
        }

        public bool ValidTile()
        {
            int i = (int)(projectile.Center.X / 16f);
            int j = (int)(projectile.Center.Y / 16f) + 4;
            Tile tile = Main.tile[i, j];
            return tile.active() &&
                (tile.type == mod.TileType<Tiles.AgitationAltar>()
                || tile.type == mod.TileType<Tiles.ProtectionAltar>()
                || tile.type == mod.TileType<Tiles.EnchantingAltar>()
                || tile.type == mod.TileType<Tiles.SoulHarvestAltar>()
                || tile.type == mod.TileType<Tiles.MeteorShowerAltar>()
                || tile.type == mod.TileType<Tiles.ResurrectAltar>());
        }

        public abstract void Config();

        public abstract void Tick();
    }
}