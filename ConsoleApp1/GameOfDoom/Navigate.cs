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




        //        sprites[0] = "invader1.png";

        //          this.Transform.X = 200.0f;
        //        this.Transform.Y = 100.0f;
        //      this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(sprites[0]);

    }
}
