using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Achievements;

namespace Necromancy.Projectiles
{
    public class ThrowingDice : ModProjectile
    {
        private bool exploded;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Throwing Dice");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 18;
            projectile.height = 18;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 120;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).throwing = true;
            exploded = false;
        }

        public override void AI()
        {
            projectile.rotation += projectile.velocity.X / 30f;
            if (!exploded) projectile.velocity.Y += 0.3f;
            if (Main.rand.NextBool())
            {
                Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 53).scale = 0.5f;
            }
            if (exploded && projectile.ai[0] == 5f)
            {
                for (int i = 0; i < 12; i++)
                {
                    Dust.QuickDust(projectile.Center + Main.rand.NextVector2Square(-52f, 52f), Color.Purple);
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            if (!exploded) Explode();
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[0] == 2f)
            {
                target.AddBuff(mod.BuffType<Buffs.Stunned>(), 60, false);
            }
            if (projectile.ai[0] == 3f)
            {
                target.AddBuff(BuffID.OnFire, 600, false);
            }
            if (projectile.ai[0] == 6f && target.type != NPCID.TargetDummy)
            {
                Vector2 teleportTo = Main.rand.NextVector2CircularEdge(256f, 256f) + target.position;
                for (int i = 0; i < 200 && Collision.SolidCollision(teleportTo, target.width, target.height); i++)
                {
                    if (i < 199)
                    {
                        teleportTo = 256f * Vector2.UnitX.RotatedByRandom(MathHelper.ToRadians(360)) + target.position;
                    }
                }
                target.Teleport(teleportTo, 1);
                target.netUpdate = true;
            }
            if (!exploded) Explode();
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X / 1.2f;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y / 3f;
            }
            projectile.velocity.X *= 0.99f;
            return false;
        }

        private void Explode()
        {
            exploded = true;
            projectile.hide = true;
            Vector2 center = projectile.Center;
            projectile.width = 104;
            projectile.height = 104;
            projectile.Center = center;
            projectile.velocity *= 0;
            projectile.timeLeft = (projectile.ai[0] == 5f ? 120 : 2);
            Main.PlaySound(SoundID.Item41, projectile.Center);

            for (int i = 0; i < 12; i++)
            {
                MakeGrayDust(projectile.Center + 8f * new Vector2(-6.5f, -6f + i));
                MakeGrayDust(projectile.Center + 8f * new Vector2(6.5f, -6f + i));
                MakeGrayDust(projectile.Center + 8f * new Vector2(-6f + i, -6.5f));
                MakeGrayDust(projectile.Center + 8f * new Vector2(-6f + i, 6.5f));
            }

            int roll = (int)projectile.ai[0];
            MakeRedDust(roll);
            switch (roll)
            {
                case 1:
                    {
                        foreach (NPC target in Necromancy.NearbyNPCs(projectile.Center, 150f))
                        {
                            target.velocity += 0.1f * (projectile.Center - target.Center);
                        }
                        break;
                    }
                case 3:
                    {

                        break;
                    }
                case 4:
                    {
                        Vector2 vel = new Vector2(4f, 4f);
                        for (int i = 0; i < 4; i++)
                        {
                            vel = vel.RotatedBy(MathHelper.ToRadians(90));
                            Projectile proj = Projectile.NewProjectileDirect(projectile.Center + vel * 6f, vel, mod.ProjectileType("Dice4Shot"), projectile.damage, 4f, projectile.owner);
                            proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = projectile.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom;
                        }
                        break;
                    }
            }
        }

        private void MakeGrayDust(Vector2 pos)
        {
            Dust d = Dust.NewDustPerfect(pos, 53);
            d.scale = Main.rand.NextFloat(1f, 1.5f);
            d.noGravity = true;
            d.velocity *= 0.2f;
        }

        private void MakeRedDust(int num)
        {
            switch (num)
            {
                case 1:
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            Vector2 pos = projectile.Center + Main.rand.NextVector2CircularEdge(150f, 150f);
                            Dust.NewDustPerfect(pos, 54, Main.rand.NextFloat(0.05f, 0.1f) * (projectile.Center - pos)).noGravity = true;
                        }
                        MakeRedDot(projectile.Center);
                        break;
                    }
                case 2:
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            Vector2 pos = projectile.Center + Main.rand.NextVector2CircularEdge(75f, 75f);
                            Dust.NewDustPerfect(pos, 63, Main.rand.NextFloat(.05f, 0.15f) * (pos - projectile.Center).RotatedBy(MathHelper.ToRadians(Main.rand.NextBool() ? 90 : -90))).noGravity = true;
                        }
                        MakeRedDot(projectile.Center + new Vector2(-24f, 24f));
                        MakeRedDot(projectile.Center + new Vector2(24f, -24f));
                        break;
                    }
                case 3:
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            Vector2 pos = projectile.Center + Main.rand.NextVector2Square(-80f, 80f);
                            Vector2 vel = 8f * (Main.rand.NextFloatDirection() * (Main.rand.NextBool() ? Vector2.UnitX : Vector2.UnitY));
                            Dust.NewDustPerfect(pos, 127, vel, 0, default(Color), Main.rand.NextFloat(1f, 2f)).noGravity = true;
                        }
                        MakeRedDot(projectile.Center);
                        MakeRedDot(projectile.Center + new Vector2(-24f, 24f));
                        MakeRedDot(projectile.Center + new Vector2(24f, -24f));
                        break;
                    }
                case 4:
                    {
                        MakeRedDot(projectile.Center + new Vector2(24f, 24f));
                        MakeRedDot(projectile.Center + new Vector2(-24f, 24f));
                        MakeRedDot(projectile.Center + new Vector2(24f, -24f));
                        MakeRedDot(projectile.Center + new Vector2(-24f, -24f));
                        break;
                    }
                case 5:
                    {
                        MakeRedDot(projectile.Center);
                        MakeRedDot(projectile.Center + new Vector2(24f, 24f));
                        MakeRedDot(projectile.Center + new Vector2(-24f, 24f));
                        MakeRedDot(projectile.Center + new Vector2(24f, -24f));
                        MakeRedDot(projectile.Center + new Vector2(-24f, -24f));
                        break;
                    }
                case 6:
                    {
                        MakeRedDot(projectile.Center + new Vector2(24f, 24f));
                        MakeRedDot(projectile.Center + new Vector2(-24f, 24f));
                        MakeRedDot(projectile.Center + new Vector2(24f, -24f));
                        MakeRedDot(projectile.Center + new Vector2(-24f, -24f));
                        MakeRedDot(projectile.Center + new Vector2(-24f, 0f));
                        MakeRedDot(projectile.Center + new Vector2(24f, 0f));
                        break;
                    }
            }
        }

        private void MakeRedDot(Vector2 pos)
        {
            for (int i = 0; i< 4; i++)
            {
                Dust dust = Dust.QuickDust(pos + Main.rand.NextVector2Circular(3f, 3f), Color.Red);
                dust.noGravity = true;
                dust.scale = Main.rand.NextFloat(1f, 1.5f);
            }
        }
    }
}