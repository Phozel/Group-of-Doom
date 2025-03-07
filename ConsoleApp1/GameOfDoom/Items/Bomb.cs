using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.GameOfDoom
{
    class Bomb : Item
    {
        public Bomb(int posx, int posy, bool collectible) : base("Bomb", "Bomb.png", 64, 64, 1, 2, 0, 0, posx, posy, collectible)
        {
            if (!collectible){
                animation.changeSprite(0,1);
            }
        }

    }
}
