using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Tile
    {
        public bool collidable = true;

        public Tile(bool collidable)
        {
            this.collidable = collidable;
        }
    }
}
