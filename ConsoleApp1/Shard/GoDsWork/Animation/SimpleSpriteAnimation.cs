
using System;

namespace Shard.Shard.GoD_s_Work.SimpleSpriteAnimation
{

    /*
     * Class that handles sprite animations where each animation
     * frame is a different image.
     */
    public class SimpleSpriteAnim
    {
        private Array _spriteArrays = new Array;
        private GameObject _gameObject;
        private int _spriteSetToUse = 0;
        private int _spriteToUse = 0;

        /*
         * spriteArray should be an Array with Arrays in it containing the sprites
         */
        public SimpleSpriteAnim(Array spriteArrays, GameObject gameObject)
        {
            _spriteArrays = spriteArrays;
            _gameObject = gameObject;
        }

        /*
         * Method that iterates through the list of sprites given to it each time it is called
         * and changes the gameObject's sprite.
         */
        public changeSprite()
        {
            spriteToUse += 1;

            if (spriteToUse >= _spriteArrays[_spriteSetToUse].Length)
            {
                spriteToUse = 0;
            }
            _gameObject.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(_spriteArrays[_spriteSetToUse][spriteToUse]);
        }

        public swapSpriteSet(int spriteSetToUse) {
            _spriteSetToUse = spriteSetToUse;
            _spriteToUse = 0;
        }

    }

}