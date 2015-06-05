using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sailing_by_the_Stars
{
    class EnemyShip : Ship
    {
        public int difficulty;

        public EnemyShip(float m = 100, Vector2? pos = null, Vector2? vel = null, int difficulty = 1)
            : base(m, pos, vel)
        {
            this.difficulty = difficulty;
        }
        void thrust()
        {

        }
    }
}
