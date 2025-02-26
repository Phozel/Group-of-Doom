using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Shard.Shard.GoDsWork.HUD
{
    public class HealthBar : HudElement
    {
        private int _maxHealth;
        int _currentHealth;
        

        public HealthBar(int maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
        }

        public void SetHealth(int health) 
            {
                _currentHealth = Math.Clamp(health, 0, _maxHealth);
            }
        public override void Update(float deltaTime)
        {

        }

        public override void Draw()
        {
            Display display = Bootstrap.getDisplay();

            int barWidth = 600;
            int barHeight = 50;
            int x = (int)Position.X;
            int y = (int)Position.Y;

            int filledWidth = (int)((_currentHealth / (float)_maxHealth) * barWidth);
            
            display.drawRectangle(x, y, barWidth, barHeight, System.Drawing.Color.White);  // Outline
            display.drawFilledRectangle(x, y, filledWidth, barHeight, System.Drawing.Color.Red);
            
            display.showText($"{_currentHealth}/{_maxHealth} HP", x + 5, y - 10, 12, 255, 255, 255, "Arial"); // White text


            Console.WriteLine($"Drawing Health Bar at {Position} with {_currentHealth}/{_maxHealth} HP");
        }
    }
}
