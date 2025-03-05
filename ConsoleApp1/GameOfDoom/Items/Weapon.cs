using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.GameOfDoom
{
    class Weapon : Item
    {
        public Weapon(int posx, int posy) : base("weapon", "PH.png", 64, 64, posx, posy)
        {
        }

    }
}
