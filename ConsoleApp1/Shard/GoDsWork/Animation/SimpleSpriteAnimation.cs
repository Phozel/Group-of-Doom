
using System;
using System.Collections.Generic;

namespace Shard.Shard.GoD_s_Work.SimpleSpriteAnimation
{

    /*
     * Class that handles sprite animations where each animation
     * frame is a different image.
     */
    public class SimpleSpriteAnimation
    {
        private List<List<string>> _spriteArrays;
        private GameObject _gameObject;
        private int _spriteSetToUse = 0;
        private int _spriteToUse = 0;

        /*
         * spriteArrays should be an Array with Arrays in it containing the sprite-paths like was done in the example games
         */
        public SimpleSpriteAnimation(List<List<string>> spriteArrays, GameObject gameObject) {
            _spriteArrays = spriteArrays;
            _gameObject = gameObject;
        }

        /*
         * Method that iterates through the list of sprites given to it each time it is called
         * and changes the gameObject's sprite.
         */
        public void changeSprite()
        {
            _spriteToUse += 1;

            if (_spriteToUse >= _spriteArrays[_spriteSetToUse].Length)
            {
                _spriteToUse = 0;
            }
            _gameObject.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(_spriteArrays[_spriteSetToUse][_spriteToUse]);
        }

        public void swapSpriteSet(int spriteSetToUse) {
            _spriteSetToUse = spriteSetToUse;
            _spriteToUse = 0;
        }

    }

}