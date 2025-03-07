using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shard.Shard.GoDsWork.Animation;

namespace Shard.GameOfDoom
{
    class Bomb : Item
    {
        public Bomb(int posx, int posy, bool collectible) : base("Bomb", collectible ? "Bomb.png" : "BombPlaced.png", collectible ? 64 : 32, collectible ? 64 : 32, 1, collectible ? 2 : 1, 0, 0, posx, posy, collectible)
        {
            if (!collectible)
            {
                // Explode!
            }
        }

    }
}
