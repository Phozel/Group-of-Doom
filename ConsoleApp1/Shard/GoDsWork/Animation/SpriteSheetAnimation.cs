
using System;
using System.Drawing;
using System.Numerics;


/*
 * This is very much not working at the moment! DO NOT USE!!!
 */
namespace Shard.Shard.GoD_s_Work.SpriteSheetAnimation
{

    /*
     * Class that handles sprite animations where each animation
     * frame is a different image.
     */
    public class SpriteSheetAnimation
    {
        private string _spriteSheet;
        private GameObject _gameObject;

        private float _spriteSizeX, _spriteSizeY;
        private float _sheetSizeX, _sheetSizeY;

		private Array _spriteArray = new Array;
        private int _spriteToUse = 0;

		public SpriteSheetAnimation(string spriteSheet, float sheetSizeX, float sheetSizeY, float spriteSizeX, float spriteSizeY, GameObject gameObject)
        {
            _gameObject = gameObject;
            _spriteSheet = spriteSheet;

            _spriteSizeX = spriteSizeX;
            _spriteSizeY = spriteSizeY;
            
            _sheetSizeX = sheetSizeX;
            _sheetSizeY = sheetSizeY;

            SplitSheet();
        }

		/*
         * Method that iterates through the list of sprites given to it each time it is called
         * and changes the gameObject's sprite.
         */
		public changeSprite()
		{
			_spriteToUse += 1;

			if (_spriteToUse >= _spriteArray.Length)
			{
				_spriteToUse = 0;
			}
			_gameObject.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(_spriteArray[_spriteToUse]);
		}

        private SplitSheet()
        {
            
        }

	}

}