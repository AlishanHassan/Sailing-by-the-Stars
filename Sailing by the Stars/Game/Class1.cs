using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sailing_by_the_Stars.Game
{
    class Planet
    {
        public int Radius;
        public int Mass;
        public Vector2 Position;
        public Vector2 Velocity;

        public Planet(int x = 0, int y = 0, int m = 100, int r = 100)
        {
            Radius = r;
            Mass = m;
            Position = new Vector2(x, y);
            Velocity = new Vector2(x, y);
        }

        public void Move(Vector2 force)
        {
            Vector2 Acceleration = new Vector2(force.X / Mass, force.Y / Mass); // a = F/m
            Velocity.X += Acceleration.X;
            Velocity.Y += Acceleration.Y;
            Position.X += Velocity.X;
            Position.Y += Velocity.Y;
        }
    }

}
