using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.GameOfDoom
{
    class Armor : Item
    {
        public Armor(int posx, int posy) : base("Armor", "Armor.png", 64, 64, posx, posy)
        {
        }

    }
}