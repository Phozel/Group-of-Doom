using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.GameOfDoom
{
    class HealthPack : Item
    {
        public HealthPack(int posx, int posy) : base("HealthPack", "HealthPack.png", 64, 64, posx, posy)
        {
        }

    }
}