using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Items
{
    public class BloodAlchemyRecipe : ModRecipe
    {
        public BloodAlchemyRecipe(Mod mod) : base(mod)
        {
        }

        public override int ConsumeItem(int type, int numRequired)
        {
            if (createItem.potion) // if the item is a potion
            {
                int actualNumRequired = 0;
                for (int i = 0; i < numRequired; i++)
                {
                    if (Main.rand.Next(2) < 1)
                    {
                        actualNumRequired++;
                    }
                }
                return actualNumRequired;
            }
            return numRequired;
        }

        public override void OnCraft(Item item)
        {
            Player player = Main.player[item.owner];
            Necromancy.DrainLife(player, 50);
        }
    }
}