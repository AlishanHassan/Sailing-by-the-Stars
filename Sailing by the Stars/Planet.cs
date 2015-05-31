using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sailing_by_the_Stars
{
    class Planet : Object
    {
       

        public Planet(int x = 0, int y = 0, float m = 100, float r = 100)
        {
            Radius = r;
            Mass = m;
            Position = new Vector2(x, y);
            Velocity = new Vector2(0, 0);
        }

      
    }

}
