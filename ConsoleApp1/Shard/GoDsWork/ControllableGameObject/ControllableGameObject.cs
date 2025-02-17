using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.Shard.GoDsWork.ControllableGameObject
{

    /*
     * Abstract Class to creat a controllable game object
     */
    internal abstract class ControllableGameObject : GameObject, InputListener
    {

        private List<string> _controls = new List<string>();
        private GameObject _gameObject;
        

        public ControllableGameObject(List<string> controls) { 
            _controls = controls;
            _gameObject = new GameObject();
        }

        public abstract void initialize();

        public abstract void handleInput(InputEvent inp, string eventType);

        public abstract override void update();

    }

    
}
