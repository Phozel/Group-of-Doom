using SDL2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.Shard.GoDsWork.Animation
{
    class SpriteSheetAnimation(GameObject gameObject, List<string> spriteSheets)
    {   
        private List<string> _spriteSheets = spriteSheets;
        private GameObject _gameObject = gameObject;
        private int _currentSpriteSheet = 0;
        
        public void changeSprite()
        {

        }

        public void swapSpriteSheet(int sheetIndex)
        {
            if (sheetIndex < _spriteSheets.Count)
            {
                _currentSpriteSheet = sheetIndex;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}
