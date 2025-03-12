using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.GameOfDoom
{
    public interface ICharacter
    {
        float Health { get; }
        float getMaxHealth();

        int Score { get; }
    }
}
