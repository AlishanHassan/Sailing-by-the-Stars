using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sailing_by_the_Stars
{
    class Ship
    {
        public float Radius;
        public float Mass;
        public Vector2 Position;
        public Vector2 Velocity;
        public Texture2D Sprite;
        public Vector2 TopLeftCorner
        {
            get
            {
                return Position - new Vector2(Radius, Radius);
            }
        }

        public Ship(int x = 0, int y = 0, float m = 100, float r = 100)
        {
            Radius = r;
            Mass = m;
            Position = new Vector2(x, y);
            Velocity = new Vector2(5, 5);
        }

        internal void Update(Vector2 force, TimeSpan deltaTime)
        {
            Vector2 Acceleration = force / this.Mass;
            Velocity += Acceleration * (float)deltaTime.TotalSeconds;
            Position += Velocity * (float)deltaTime.TotalSeconds;
        }
    }

}
