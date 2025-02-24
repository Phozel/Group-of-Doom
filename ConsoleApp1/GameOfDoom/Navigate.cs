using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.GameOfDoom
{
    internal class Navigate
    {
        //  private static Navigate instance;
        //  public static Navigate Instance;
        //singleton?

        private List<List<Room>> worldMap;
        private Room currentRoom;

        private Navigate() {
            WorldMap wm = new WorldMap(3, 0, (5, 7));
            this.worldMap = wm.getMap();
            currentRoom = wm.GetStartRoom();


            
        }
    }
}
