using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.Shard.GoDsWork.HUD
{
    public class AmmoCount : HudElement
    {
       public int currentAmmo;
       public int totalAmmo;

        public AmmoCount(int currentAmmo, int totalAmmo)
        {
            this.currentAmmo = currentAmmo;
            this.totalAmmo = totalAmmo;
        }

        public override void Draw()
        {
            Display display = Bootstrap.getDisplay();

            int x = (int)Position.X;
            int y = (int)Position.Y;

            display.showText($"Ammo: {currentAmmo}/{totalAmmo}", x + 5, y - 10, 12, 255, 255, 255, "Arial");
            Console.WriteLine($"Drawing Ammo Count at {Position}");
        }
        public override void Update(float deltaTime)
        {
            Console.WriteLine($"Ammo Updated: {currentAmmo}/{totalAmmo}");
        }
    }
}
