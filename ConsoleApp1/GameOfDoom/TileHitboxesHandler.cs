using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shard.Shard.GoD_s_Work.Tiles_Libary;

namespace Shard.GameOfDoom
{
    internal static class TileHitboxesHandler
    {
        internal static void ClearHitboxes<T>(List<List<T>> room) where T : GameObject 
        {
            foreach (List<T> row in room)
            {
                ClearHitboxes(row);
            }
        }
        internal static void ClearHitboxes<T>(List<T> tiles) where T : GameObject
        {
            foreach (T tile in tiles)
            {
                ClearHitboxes(tile);
            }
        }
        internal static void ClearHitboxes<T>(T tile) where T : GameObject 
        {
            if (tile.MyBody != null)
                tile.MyBody.clearColliders();
        }

        internal static void addHitboxes<T>(List<List<T>> room) where T : GameObject {
            T tile;
            //freeWalls
            for (int y = 1; y < room.Count()-1; y++) for (int x = 1; x < room[0].Count()-1; x++) // free walls only
                {
                    tile = room[y][x];
                    if (tile.checkTag("Wall")) HitboxFreeWall(tile);
                }
            //corners
            HitboxTopLeft(room[0][0]);
            HitboxTopRight(room[0][room[0].Count()-1]);
            HitboxBottomLeft(room[room.Count()-1][0]);
            HitboxBottomRight(room[room.Count()-1][room[0].Count()-1]);
            //edges

            for (int y = 1; y < room.Count()-1; y++) 
            {
                HitboxLeftWall(room[y][0]);
                HitboxRightWall(room[y][room[0].Count()-1]);
            }
            for (int x = 1; x < room[0].Count()-1; x++) 
            {
                HitboxTopWall(room[0][x]);
                HitboxBottomWall(room[room.Count()-1][x]);
            }

        }
        //where to add hitboxes
        //corners
        private static void HitboxTopLeft<T>(T tile) where T : GameObject
        {
            tile.MyBody.addRectCollider(0, 0, 64, 32); //top
            tile.MyBody.addRectCollider(0, 0, 32, 64); //left}
        }
        private static void HitboxTopRight<T>(T tile) where T : GameObject {
            tile.MyBody.addRectCollider(0, 0, 64, 32); //top
            tile.MyBody.addRectCollider(32, 0, 32, 64); //right
        }
        private static void HitboxBottomLeft<T>(T tile) where T : GameObject {
            tile.MyBody.addRectCollider(0, 32, 64, 32); //bottom
            tile.MyBody.addRectCollider(0, 0, 32, 64); //left
        }
        private static void HitboxBottomRight<T>(T tile) where T : GameObject {
            tile.MyBody.addRectCollider(0, 32, 64, 32); //bottom
            tile.MyBody.addRectCollider(32, 0, 32, 64); //right
        }
        //walls edges
        private static void HitboxTopWall<T>(T tile) where T : GameObject { 
        tile.MyBody.addRectCollider(0, 0, 64, 32); //top
        }
        private static void HitboxLeftWall<T>(T tile) where T : GameObject { 
        tile.MyBody.addRectCollider(0, 0, 32, 64); //left
        }
        private static void HitboxRightWall<T>(T tile) where T : GameObject { 
        tile.MyBody.addRectCollider(32, 0, 32, 64); //right
        }
        private static void HitboxBottomWall<T>(T tile) where T : GameObject {
            tile.MyBody.addRectCollider(0, 32, 64, 32); //bottom
        }
        //free walls
        private static void HitboxFreeWall<T>(T tile) where T : GameObject { tile.MyBody.addRectCollider(8, 8, 48, 48); }

    }
}
