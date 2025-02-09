

using System;

namespace Shard.Shard.GoD_s_Work.SimpleSpriteAnimation
{

    /*
     * Class that handles sprite animations where each animation
     * frame is a different image.
     */
    public class SimpleSpriteAnim
    {
        private Array sprites = new Array;
        private GameObject game_Object;
        private int spriteToUse = 0;

        public SimpleSpriteAnim(Array spriteArray, GameObject gameObject)
        {
            sprites = spriteArray;
            game_Object = gameObject;
        }

        /*
         * Method that iterates through the list of sprites given to it and 
         */
        public changeSprite()
        {
            spriteToUse += 1;

            if (spriteToUse >= sprites.Length)
            {
                spriteToUse = 0;
            }
            game_Object.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(sprites[spriteToUse]);
        }

        
    }

}