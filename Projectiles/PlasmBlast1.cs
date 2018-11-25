using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
    public class PlasmBlast1 : PlasmBlast
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Main.projFrames[mod.ProjectileType("PlasmBlast1")] = 7;
        }
    }
}
