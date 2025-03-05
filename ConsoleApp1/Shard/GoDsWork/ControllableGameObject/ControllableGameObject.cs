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
    internal abstract class ControllableGameObject : GameObject, InputListener, CollisionHandler
    {

        private List<string> _controls = new List<string>();
        private GameObject _gameObject;
        

        public ControllableGameObject() { 
            _gameObject = new GameObject();
        }

        //public override abstract void initialize();

        public abstract void handleInput(InputEvent inp, string eventType);

        public virtual void onCollisionEnter(PhysicsBody x)
        {
            
        }

        public virtual void onCollisionExit(PhysicsBody x)
        {
            
        }

        public virtual void onCollisionStay(PhysicsBody x)
        {
           
        }

        public abstract override void update();

    }

    
}
