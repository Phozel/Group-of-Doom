using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Shard.Shard.GoDsWork.HUD
{
    public abstract class HudElement
    {
        public Vector2 Position { get; set; }
        public bool IsVisible { get; set; } = true;

        public abstract void Update(float deltaTime);
        public abstract void Draw();
    }
}
