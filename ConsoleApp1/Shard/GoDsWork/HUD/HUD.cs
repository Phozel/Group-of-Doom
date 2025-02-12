using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Shard;
using Shard.Shard.GoDsWork.HUD;

public class HUD : GameObject
{
        private int playerHealth = 100;
        private int score = 0;

    public override void initialize()
    {
        Visible = true;
    }

    public override void update()
    {
        if (score < 1000)
        {
            score++;
        }
    }

    public void draw()
    {
       DisplayText display = Bootstrap.getDisplay() as DisplayText;
        if (display != null)
        {
            throw new InvalidOperationException("Bootstrap.getDisplay() did not return a DisplayText instance.");
        }

        string fontname = "Arial";

        char[,] healthText = TextUtils.ConvertStringToCharArray($"Health: {playerHealth}");
        char[,] scoreText = TextUtils.ConvertStringToCharArray($"Score: {score}");

        display.showText(healthText, 10, 10, 16, 255, 255, 255, fontname);
        display.showText(scoreText, 10, 30, 16, 255, 255, 255, fontname); //displays text in top-left corner in white
    }


}

