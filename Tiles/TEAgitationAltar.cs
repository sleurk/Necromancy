using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Tiles
{
    public class TEAgitationAltar : TEAltar
    {
        public override int? UseChalk(int chalk)
        {
            if (chalk == mod.ItemType<Items.BoneChalk>())
            {
                return mod.ProjectileType<Projectiles.Rituals.Agitation1>();
            }
            else if (chalk == mod.ItemType<Items.ShadowChalk>())
            {
                return mod.ProjectileType<Projectiles.Rituals.Agitation2>();
            }
            else if (chalk == mod.ItemType<Items.SoulChalk>())
            {
                return mod.ProjectileType<Projectiles.Rituals.Agitation3>();
            }
            return 0;
        }
    }
}