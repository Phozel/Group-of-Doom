/*
*
*   This class intentionally left blank.  
*   @author Michael Heron
*   @version 1.0
*   
*/

namespace Shard
{
    abstract public class Sound
    {
        abstract public void playSound(string file, int volume);

        abstract public void playMusic(string file, int volume);

        abstract public void stopMusic();

    }

}
