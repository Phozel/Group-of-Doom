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
        internal static void ClearHitboxes(List<List<Tile>> room) 
        {
            foreach (List<Tile> row in room)
            {
                ClearHitboxes(row);
            }
        }
        internal static void ClearHitboxes(List<Tile> tiles)
        {
            foreach (Tile tile in tiles)
            {
                ClearHitboxes(tile);

            }
        }
        internal static void ClearHitboxes(Tile tile) 
        {
            if (tile.MyBody != null) PhysicsManager.getInstance().removePhysicsObject(tile.MyBody);
            if(tile.item != null) PhysicsManager.getInstance().removePhysicsObject(tile.item.MyBody);
        }
        internal static void addHitboxes(List<List<Tile>> room)
        { 
            foreach(List<Tile> row in room) foreach(Tile tile in row)
                {
                    if (tile.MyBody != null) PhysicsManager.getInstance().addPhysicsObject(tile.MyBody);
                    if (tile.item != null) PhysicsManager.getInstance().addPhysicsObject(tile.item.MyBody);
                }
        }
        internal static void createHitboxes(List<List<Tile>> room) {
            Tile tile;
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
        //where to add Tile hitboxes
        //corners
        private static void HitboxTopLeft(Tile tile) 
        {
            tile.setPhysicsEnabled();
            tile.MyBody.Kinematic = true;
            tile.MyBody.addRectCollider(0, 0, 64, 32); //top
            tile.MyBody.addRectCollider(0, 0, 32, 64); //left}
        }
        private static void HitboxTopRight(Tile tile) {
            tile.setPhysicsEnabled();
            tile.MyBody.Kinematic = true;
            tile.MyBody.addRectCollider(0, 0, 64, 32); //top
            tile.MyBody.addRectCollider(32, 0, 32, 64); //right
        }
        private static void HitboxBottomLeft(Tile tile) {
            tile.setPhysicsEnabled();
            tile.MyBody.Kinematic = true;
            tile.MyBody.addRectCollider(0, 32, 64, 32); //bottom
            tile.MyBody.addRectCollider(0, 0, 32, 64); //left
        }
        private static void HitboxBottomRight(Tile tile) {
            tile.setPhysicsEnabled();
            tile.MyBody.Kinematic = true;
            tile.MyBody.addRectCollider(0, 32, 64, 32); //bottom
            tile.MyBody.addRectCollider(32, 0, 32, 64); //right
        }
        //walls edges
        private static void HitboxTopWall(Tile tile) {
            tile.setPhysicsEnabled();
            tile.MyBody.Kinematic = true;
            tile.MyBody.addRectCollider(0, 0, 64, 32); //top
        }
        private static void HitboxLeftWall(Tile tile) {
            tile.setPhysicsEnabled();
            tile.MyBody.Kinematic = true;
            tile.MyBody.addRectCollider(0, 0, 32, 64); //left
        }
        private static void HitboxRightWall(Tile tile) {
            tile.setPhysicsEnabled();
            tile.MyBody.Kinematic = true;
            tile.MyBody.addRectCollider(32, 0, 32, 64); //right
        }
        private static void HitboxBottomWall(Tile tile) {
            tile.setPhysicsEnabled();
            tile.MyBody.Kinematic = true;
            tile.MyBody.addRectCollider(0, 32, 64, 32); //bottom
        }
        //free walls
        private static void HitboxFreeWall(Tile tile) {
            tile.setPhysicsEnabled();
            tile.MyBody.Kinematic = true; 
            tile.MyBody.addRectCollider(8, 8, 48, 48); 
        }

    }
}
