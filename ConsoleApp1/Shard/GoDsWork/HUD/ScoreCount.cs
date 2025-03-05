using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.Shard.GoDsWork.HUD
{
    public class ScoreCount : HudElement
    {
        int currentScore;

        public override void Draw()
        {
            Display display = Bootstrap.getDisplay();

            int x = (int)Position.X;
            int y = (int)Position.Y;

            display.showText($"S c o r e: {currentScore}", x + 5, y - 10, 12, 255, 255, 255, "Arial");

            Console.WriteLine($"Drawing Score Counter at {Position}");
        }

        public override void Update(float deltaTime)
        {

        }

    }
}
