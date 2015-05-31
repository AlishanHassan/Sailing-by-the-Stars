using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sailing_by_the_Stars
{
    class EnemyShip : Object
    {
        public int difficulty;

        public EnemyShip(int x = 0, int y = 0, float m = 100, float r = 100, int difficulty = 1)
            : base(x, y, m, r)
        {
            this.difficulty = difficulty;
        }
        void thrust()
        {

        }
    }
}
