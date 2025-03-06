using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.GameOfDoom
{
    class Weapon : Item
    {
        public Weapon(int posx, int posy) : base("Weapon", "PH.png", 64, 64, 1, 1, 0, 0, posx, posy)
        {
        }

    }
}
