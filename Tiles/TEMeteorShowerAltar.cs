
namespace Necromancy.Tiles
{
    public class TEMeteorShowerAltar : TEAltar
    {
        public override int? UseChalk(int chalk)
        {
            if (chalk == mod.ItemType<Items.BoneChalk>())
            {
                return mod.ProjectileType<Projectiles.Rituals.MeteorShower1>();
            }
            else if (chalk == mod.ItemType<Items.ShadowChalk>())
            {
                return mod.ProjectileType<Projectiles.Rituals.MeteorShower2>();
            }
            else if (chalk == mod.ItemType<Items.SoulChalk>())
            {
                return mod.ProjectileType<Projectiles.Rituals.MeteorShower3>();
            }
            return 0;
        }
    }
}