
using System;
using System.Collections.Generic;

namespace Shard
{
    /*
     * Class that handles sprite animations where each animation
     * frame is a different image.
     */
    class SimpleSpriteAnimation(List<List<string>> spriteArrays, GameObject gameObject)
    {
        private List<List<string>> _spriteArrays = spriteArrays;
        private GameObject _gameObject = gameObject;
        private int _spriteSetToUse = 0;
        private int _spriteToUse = 0;


        /*
         * Method that iterates through the list of sprites given to it each time it is called
         * and changes the gameObject's sprite.
         */
        public void changeSprite()
        {
            _spriteToUse += 1;

            if (_spriteToUse >= _spriteArrays[_spriteSetToUse].Count)
            {
                _spriteToUse = 0;
            }
            _gameObject.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(_spriteArrays[_spriteSetToUse][_spriteToUse]);
        }

        public void swapSpriteSet(int spriteSetToUse)
        {
            if (spriteSetToUse <  _spriteArrays.Count) { 
                _spriteSetToUse = spriteSetToUse;
                _spriteToUse = 0;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
            
        }

    }
}