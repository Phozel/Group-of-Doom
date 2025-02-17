using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.Shard.GoDsWork.HUD
{
    public static class TextUtils
    {
        public static char[,] ConvertStringToCharArray(string text)
        {
            char[,] charArray = new char[text.Length, 1];

            for (int i = 0; i < text.Length; i++)
            {
                charArray[i, 0] = text[i];
            }

            return charArray;
        }
    }
}
