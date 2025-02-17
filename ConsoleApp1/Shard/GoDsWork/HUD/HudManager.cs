using System;
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
            }
        }

    }
}
