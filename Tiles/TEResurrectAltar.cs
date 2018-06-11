
namespace Necromancy.Tiles
{
    public class TEResurrectAltar : TEAltar
    {
        public override int? UseChalk(int chalk)
        {
            if (chalk == mod.ItemType<Items.BoneChalk>())
            {
                return mod.ProjectileType<Projectiles.Rituals.Resurrect1>();
            }
            else if (chalk == mod.ItemType<Items.ShadowChalk>())
            {
                return mod.ProjectileType<Projectiles.Rituals.Resurrect2>();
            }
            else if (chalk == mod.ItemType<Items.SoulChalk>())
            {
                return mod.ProjectileType<Projectiles.Rituals.Resurrect3>();
            }
            return 0;
        }
    }
}