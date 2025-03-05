using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.GameOfDoom
{
    class Armor : Item
    {
        public Armor(int posx, int posy) : base("armor", "PH.png", 64, 64, posx, posy)
        {
        }

    }
}