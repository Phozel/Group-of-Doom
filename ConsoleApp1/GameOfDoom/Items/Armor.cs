using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shard.Shard.GoDsWork.Animation;

namespace Shard.GameOfDoom
{
    class Armor : Item
    {

        public Armor(int posx, int posy, int startrow) : base("Armor", "Armor.png", 64, 64, 3, 1, startrow, 0, posx, posy)
        {
            
        }

    }
}