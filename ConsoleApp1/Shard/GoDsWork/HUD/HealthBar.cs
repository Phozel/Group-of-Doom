﻿using Shard.GameOfDoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Shard.GameOfDoom.CharacterGoD;

namespace Shard.Shard.GoDsWork.HUD
{
    public class HealthBar : HudElement
    {
        private int _maxHealth; //same in player class
        private int _currentHealth;
        private ICharacter _player;
        

        public HealthBar(ICharacter player)
        {
            _player = player;
            _maxHealth = (int)player.getMaxHealth();
            _currentHealth = (int)player.Health;

            int x = (int)Position.X;
            int y = (int)Position.Y;
            int barHeight = 24;
            Display display = Bootstrap.getDisplay();

            display.drawFilledRectangle(x, y, _currentHealth, barHeight, System.Drawing.Color.Black);

            Console.WriteLine($"Drawing Health Bar at {Position} with {_currentHealth}/{_maxHealth} HP");

        }


        public override void Update(float deltaTime)
        {
            if ( _player != null )
            {
                _currentHealth = (int )_player.Health;
            }
        }

        public override void Draw()
        {
            Display display = Bootstrap.getDisplay();

            

            int barWidth = 199;
            int barHeight = 24;
            int outlineWidth = 200;
            int outlineHeight = 25;
            int x = (int)Position.X;
            int y = (int)Position.Y;

            int currentHealth = (int)_currentHealth;

            int filledWidth = (int)((currentHealth / (float)_maxHealth) * (barWidth));
            
            display.drawRectangle(x, y, outlineWidth, outlineHeight, System.Drawing.Color.White);  // Outline
            display.drawFilledRectangle(x, y, filledWidth, barHeight, System.Drawing.Color.Red);
            
            display.showText($"{currentHealth}/{_maxHealth} HP", x - 5, y - 10, 12, 255, 255, 255, "Arial"); // White text


            //Console.WriteLine($"Drawing Health Bar at {Position} with {_currentHealth}/{_maxHealth} HP");
        }

        public void setCurrentHealth(int health)
        {
            _currentHealth = health;
        }
    }
}
