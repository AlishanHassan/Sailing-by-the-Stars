using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sailing_by_the_Stars
{
    class EnemyShip : Ship
    {
        private Vector2 thrust;
        public int difficulty;
        public Planet nearestPlanet;
        public float distanceToNearestPlanetSq
        {
            get
            {
                if (nearestPlanet == null)
                {
                    return float.PositiveInfinity;
                }
                return (this.Position - nearestPlanet.Position).LengthSquared();
            }
        }

        public EnemyShip(float m = 100, float r = 100, Vector2? pos = null, Vector2? vel = null, int difficulty = 1)
            : base(m, r, pos, vel)
        {
            this.thrust = Vector2.Zero;
            this.difficulty = difficulty;
        }

        internal override void Move(TimeSpan deltaTime)
        {
            // apply thrust
            this.Acceleration += thrust;
            thrust = Vector2.Zero;

            // come back if strayed
            if (nearestPlanet != null)
            {
                this.Acceleration += (nearestPlanet.Position - this.Position)/256;
            }

            base.Move(deltaTime);
        }

        public void creatThrust(Vector2 t)
        {
            // at one moment the ship can be close to multiple planets
            // so this method might be used more than once in a single update()
            // so use += instead of =
            thrust += t;
        }

        public override string ToString()
        {
            return "-" + base.ToString() + "," + difficulty;
        }
    }
}
