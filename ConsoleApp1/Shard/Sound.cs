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
        abstract public void playSound(string file, int volume, uint dev);
        abstract public void playSound2(string file);

    }

}
