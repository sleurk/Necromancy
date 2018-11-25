using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Necromancy.Items.Placeable;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles.Rituals
{
	public abstract class Ritual : ModProjectile
	{
        readonly int[] dusts = { 0, 63, 27, 135 };

        protected virtual RitualPower Power
        {
            get { return RitualPower.RitualInvalid; }
        }

        protected int DustType
        {
            get { return dusts[(byte)Power]; }
        }

        protected virtual int TileType
        {
            get
            {
                return 0;
            }
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 64;
			projectile.height = 64;
			projectile.timeLeft = 36000;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.netImportant = true;
        }

        public override void AI()
        {
            Tick();
            if (!ValidTile()) projectile.Kill();
            projectile.alpha = (int)(60 * Math.Sin(projectile.timeLeft / 25f)) + 160;
            int x = (int)(projectile.Center.X / 16f);
            int y = (int)(projectile.Center.Y / 16f);
            
            projectile.Center -= projectile.oldVelocity;

            Vector2 offset = new Vector2(0f, (float)Math.Sin(MathHelper.ToRadians(projectile.timeLeft)) / 10f) - projectile.velocity;
            for (int i = 0; i < 3; i++)
            {
                Vector2 dustPos = projectile.position + offset + new Vector2(0, projectile.height / -2f);
                Dust d = Dust.NewDustDirect(dustPos, projectile.width, projectile.height * 2, DustType);
                d.noGravity = true;
                d.velocity = new Vector2(Main.rand.NextFloat(-2f, 2f), 0f);
                dustPos = new Vector2(Main.rand.NextFloat(-28f, 28f), 48f) + projectile.Center;
                d = Dust.NewDustPerfect(dustPos, DustType);
                d.noGravity = true;
                d.velocity = new Vector2(0, Main.rand.NextFloat(0f, -4f));
                dustPos = new Vector2(Main.rand.NextFloat(-28f, 28f), 48f) + projectile.Center;
                d = Dust.NewDustPerfect(dustPos, DustType);
                d.scale = Main.rand.NextFloat(1f, 1.3f);
                d.noGravity = true;
                d.velocity = new Vector2(0, 0);
            }
            projectile.oldVelocity = projectile.velocity;
            projectile.velocity = new Vector2(0f, (float)Math.Cos(MathHelper.ToRadians(projectile.timeLeft)) / 10f) * 32f;
        }

        public bool ValidTile()
        {
            int i = (int)(projectile.Center.X / 16f);
            int j = (int)(projectile.Center.Y / 16f) + 4;
            Tile tile = Main.tile[i, j];
            return tile.active() && tile.type == TileType;
        }

        public abstract void Tick();
    }

    public enum RitualPower : byte
    {
        RitualInvalid = 0,
        RitualBone,
        RitualShadow,
        RitualSoul,
    }
}