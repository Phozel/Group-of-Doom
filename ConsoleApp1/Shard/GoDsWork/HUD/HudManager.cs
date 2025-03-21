﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.Shard.GoDsWork.HUD
{
    public class HudManager
    {
        private List<HudElement> elements = new List<HudElement>();

        public void AddElement(HudElement element)
        {
            elements.Add(element);
        }

        public void RemoveElement(HudElement element)
        {
            elements.Remove(element);
        }

        public void Update (float deltaTime)
        {
            foreach (HudElement element in elements)
            {
                if (element.IsVisible)
                    element.Update(deltaTime);
            }
        }

        public void Draw()
        {
            foreach(HudElement element in elements)
            {
                if (element.IsVisible)
                    element.Draw();
               // Console.WriteLine("HUD Draw Called");
            }

            Display display = Bootstrap.getDisplay();

            int x = 10, y = 10, width = 200, height = 10;
            
        }

        public void UpdateHealthBar(int health)
        {
            foreach (var element in elements)
            {
                if (element is HealthBar healthBar)
                {
                    
                    healthBar.Draw();
                }
            }
        }

        public void UpdateScore(int score)
        {
            foreach (var element in elements)
            {
                if (element is ScoreCount scoreCount)
                {
                    scoreCount.Draw();
                }

            }
        }

    }
}
