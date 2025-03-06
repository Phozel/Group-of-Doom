using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.GameOfDoom
{
    class Bomb : Item
    {
        public Bomb(int posx, int posy) : base("Bomb", "PH.png", 64, 64, 1, 1, 0, 0, posx, posy)
        {
        }

    }
}
