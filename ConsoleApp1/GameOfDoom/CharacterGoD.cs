using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.GameOfDoom
{
    internal class CharacterGoD
    {
        private string _name { get; set; }
        private float _maxHealth { get; }
        private float _health { get; set; }
        private float _armour { get; set; }
        //private List<Item> _inventory { get; set; }
        private float _movementSpeed { get; set; }

        public CharacterGoD() {
            _name = "God";
            _maxHealth = 100;
            _health = 100;
            _armour = 0;
            _movementSpeed = (float)(100 * Bootstrap.getDeltaTime());
            //_inventory = new List<Item>();
        }



    }
}
