using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;

namespace Necromancy.Projectiles
{
	public class PylonCreator : ModProjectile
	{
        // spawns when player is using weapon
        // does all the stuff so the server doesn't have to communicate every time

        // ai0 = nothing, used to be something that has since been removed
        // ai1 = index of last pylon projectile created

        private List<int> createdProjectiles;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pylon Creator");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 4;
			projectile.height = 4;
            projectile.penetrate = 1;
			projectile.timeLeft = 4;
            projectile.hide = true;
            projectile.netImportant = true;
            createdProjectiles = new List<int>();
        }

		public override void AI()
        {
            projectile.velocity = Vector2.Zero;
            if (Main.myPlayer == projectile.owner)
            {
                Player player = Main.player[projectile.owner];
                projectile.netUpdate = true;
                if (player.channel && projectile.ai[1] < 4.5f)
                {
                    projectile.timeLeft = 4;
                    if (projectile.ai[1] < 0f)
                    {
                        CreatePylon(Main.MouseWorld);
                    }
                    else
                    {
                        Vector2 toHere = Main.MouseWorld - Main.projectile[createdProjectiles[(int)projectile.ai[1]]].Center;
                        if (toHere.Length() > 200f)
                        {
                            toHere = toHere.SafeNormalize(Vector2.Zero) * 200f;
                            CreatePylon(Main.projectile[createdProjectiles[(int)projectile.ai[1]]].Center + toHere);
                        }
                    }
                }
                else
                {
                    if (projectile.ai[1] < 3.5f)
                    {
                        CreatePylon(Main.MouseWorld);
                    }

                    projectile.Kill();
                }
            }
        }

        private void CreatePylon(Vector2 pos)
        {
            Main.PlaySound(SoundID.Item46, pos);
            if (projectile.ai[1] < 0)
            {
                createdProjectiles.Add(Projectile.NewProjectile(pos, Vector2.Zero, mod.ProjectileType("Pylon"), projectile.damage,
                    projectile.knockBack, projectile.owner, -1f, -1f));
            }
            else
            {
                Projectile previous = Main.projectile[createdProjectiles[(int)projectile.ai[1]]];
                createdProjectiles.Add(Projectile.NewProjectile(pos, Vector2.Zero, mod.ProjectileType("Pylon"), projectile.damage,
                    projectile.knockBack, projectile.owner, previous.Center.X, previous.Center.Y));
            }
            projectile.ai[1]++;
        }
    }
}