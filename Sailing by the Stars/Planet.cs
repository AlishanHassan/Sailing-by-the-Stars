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


        public Planet(float m = 100, float r = 100, Vector2? pos = null, Vector2? vel = null)
            : base(m, r, pos, vel)
        {

        }
        internal override void Move(Vector2 force, TimeSpan deltaTime)
        {
            Acceleration = force / this.Mass;
            Velocity += Acceleration * (float)deltaTime.TotalSeconds * (float).1;
            PreviousPosition = Position;
            Position += Velocity * (float)deltaTime.TotalSeconds;
        }

        public override string ToString()
        {
            return "0," + base.ToString();
        }
    }

}
