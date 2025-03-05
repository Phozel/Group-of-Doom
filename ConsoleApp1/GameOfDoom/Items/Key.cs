using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.GameOfDoom
{
    class Key : Item
    {
        public Key(int posx, int posy) : base("key", "Key.png", 64, 64, posx, posy)
        {
        }

    }
}
