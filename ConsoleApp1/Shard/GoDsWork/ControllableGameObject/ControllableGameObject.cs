using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.Shard.GoDsWork.ControllableGameObject
{
    /*
     * What does this class need to do?
     * - Everything a GameObject already does
     * - Receive input and send it to Input System 
     */
    internal abstract class ControllableGameObject : GameObject, InputListener
    {

        private List<string> _controls = new List<string>();
        private GameObject _gameObject;
        

        public ControllableGameObject(List<string> controls) { 
            _controls = controls;
            _gameObject = new GameObject();
        }

        public void handleInput(InputEvent inp, string eventType)
        {
            if (Bootstrap.getRunningGame().isRunning() == false)
            {
                return;
            }

            throw new NotImplementedException();
        }
    }

    
}
