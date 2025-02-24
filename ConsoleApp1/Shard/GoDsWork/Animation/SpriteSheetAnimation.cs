using SDL2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shard.Shard.GoDsWork.Animation
{

    /*
     * Class to handle all SpriteSheet animations. Can be used to select sprites for other reasons as well.
     */
    class SpriteSheetAnimation(GameObject gameObject, string spriteSheet, float spriteWidth, float spriteHeight, float rows, float columns)
    {   
        private string _spriteSheet = spriteSheet;
        private GameObject _gameObject = gameObject;

        private float _spriteWidth = spriteWidth;
        private float _spriteHeight = spriteHeight;
        private float _rows = rows;
        private float _columns = columns;

        // Calculate how much space each sprite occupies in the sheet
        private float _spriteWidthPercentage = spriteWidth / (float)(spriteWidth * columns);
        private float _spriteHeightPercentage = spriteHeight / (float)(spriteHeight * rows);

        // Default position of sprite in sheet
        float SpritePosX = 0;
        float SpritePosY = 0;

        /*
         * Function to change what Sprite in the SpriteSheet is being used starting from row & col 0
         */
        public void changeSprite(int row, int col)
        {
            // Calculates sprite position in sheet
            SpritePosX = col * _spriteWidth;
            SpritePosY = row * _spriteHeight;

            // Scales sprite appropriately
            _gameObject.Transform.Scalex = _spriteWidthPercentage;
            _gameObject.Transform.Scaley = _spriteHeightPercentage;

            // Picks out correct sprite based on position
            _gameObject.Transform.StartX = SpritePosX;
            _gameObject.Transform.StartY = SpritePosY;
            
            _gameObject.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(_spriteSheet);
            
        }

    }
}
