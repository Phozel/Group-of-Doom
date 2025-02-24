using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Shard.Shard.GoDsWork.TilesLibrary;

namespace Shard.Shard.GoD_s_Work.Tiles_Libary
{
    public class Tile : Node
    {
        internal static float width = 64;
        private string imagePath;

        internal Tile(int x, int y) : base(x, y) 
        { 
            this.Transform.X = x * width; // where to draw
            this.Transform.Y = y * width; // where to draw
        }

        internal void setImagePath(string imagePath)
        {
            this.imagePath = imagePath;
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(imagePath);
        }
        internal override bool isNodeEmpty() { return imagePath == null; }
        public override void initialize() { }
        public override void update() { Bootstrap.getDisplay().addToDraw(this); }


    }

}
