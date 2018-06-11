
namespace Necromancy.Tiles
{
    public class TESoulHarvestAltar : TEAltar
    {
        public override int? UseChalk(int chalk)
        {
            if (chalk == mod.ItemType<Items.BoneChalk>())
            {
                return mod.ProjectileType<Projectiles.Rituals.SoulHarvest1>();
            }
            else if (chalk == mod.ItemType<Items.ShadowChalk>())
            {
                return mod.ProjectileType<Projectiles.Rituals.SoulHarvest2>();
            }
            else if (chalk == mod.ItemType<Items.SoulChalk>())
            {
                return mod.ProjectileType<Projectiles.Rituals.SoulHarvest3>();
            }
            return 0;
        }
    }
}