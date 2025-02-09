
using System;

namespace Shard.Shard.GoD_s_Work.SimpleSpriteAnimation
{

    /*
     * Class that handles sprite animations where each animation
     * frame is a different image.
     */
    public class SimpleSpriteAnim
    {
        private Array _spriteArray = new Array;
        private GameObject _gameObject;
        private int spriteToUse = 0;

        public SimpleSpriteAnim(Array spriteArray, GameObject gameObject)
        {
            _spriteArray = spriteArray;
            _gameObject = gameObject;
        }

        /*
         * Method that iterates through the list of sprites given to it each time it is called
         * and changes the gameObject's sprite.
         */
        public changeSprite()
        {
            spriteToUse += 1;

            if (spriteToUse >= _spriteArray.Length)
            {
                spriteToUse = 0;
            }
            _gameObject.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(_spriteArray[spriteToUse]);
        }

    }

}