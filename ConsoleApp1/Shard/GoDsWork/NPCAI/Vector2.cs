using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.Shard.GoDsWork.NPCAI
{
    public struct Vector2
    {
        public float X; public float Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float Distance(Vector2 other)
        {
            return (float)Math.Sqrt((X - other.X) * (X - other.X) + (Y - other.Y) * (Y - other.Y));
        }

        public Vector2 Normalize()
        {
            float length = Distance(new Vector2(0, 0));
            return length > 0 ? new Vector2(X / length, Y / length) : new Vector2(0, 0);
        }

        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.X + b.X, a.Y + b.Y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.X - b.X, a.Y - b.Y);
        public static Vector2 operator *(Vector2 a, float scalar) => new Vector2(a.X * scalar, a.Y * scalar);


    }

}

