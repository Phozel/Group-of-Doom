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
    class SpriteSheetAnimation(GameObject gameObject, string spriteSheet, int spriteWidth, int spriteHeight, int rows, int columns)
    {   
        private string _spriteSheet = spriteSheet;
        private GameObject _gameObject = gameObject;

        private int _spriteWidth = spriteWidth;
        private int _spriteHeight = spriteHeight;
        private int _rows = rows;
        private int _columns = columns;

        private Rectangle _spriteRect = new Rectangle(0, 0, spriteWidth, spriteHeight);

        public void changeSprite(int row, int col)
        {

            _spriteRect.X = col * _spriteWidth;
            _spriteRect.Y = row * _spriteHeight;

            _gameObject.Transform.Scalex = 0.33f;
            _gameObject.Transform.Scaley = 0.33f;

            _gameObject.Transform.StartX = _spriteRect.X;
            _gameObject.Transform.StartY = _spriteRect.Y;

            //_gameObject.Transform.transformXYPosInImage(_spriteRect.X, _spriteRect.Y);
            
            _gameObject.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(_spriteSheet);
            
        }

    }
}
