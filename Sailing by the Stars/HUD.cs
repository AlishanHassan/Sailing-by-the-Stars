using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sailing_by_the_Stars
{
    class HUD
    {
        int health;
        Vector2 direction;
        int density;
        public HUD()
        {
            health = 100;
            direction = new Vector2(400, 0);
            density = 50;
            
        }

        public void setHealth(int h)
        {
            health = h; 
        }

        public void setDirection(Vector2 dir)
        {
            direction = dir;
        }

        public void setDensity(int dens)
        {
            density = dens;
        }

        public void Update()
        {

        }
        public void Draw()
        {

        }
    }
}
