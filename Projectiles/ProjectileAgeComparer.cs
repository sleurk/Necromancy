using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;

namespace Necromancy.Projectiles
{
    class ProjectileAgeComparer : IComparer<Projectile>
    {
        // for sorting projectiles newest to oldest
        public int Compare(Projectile x, Projectile y)
        {
            int xT = x.timeLeft;
            int yT = y.timeLeft;
            return -Math.Sign(xT - yT);
        }
    }
}
