using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sailing_by_the_Stars
{
    class Object
    {
        public float Radius;
        public float Mass;
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Acceleration;
        public Texture2D Sprite;
        protected float AccAngle
        {
            get
            {
                float radians = (float)(Math.Atan2(Acceleration.Y, Acceleration.X) + Math.PI / 2);
                return radians;
            }
        }
        protected float VelAngle
        {
            get
            {
                float radians = (float)(Math.Atan2(Velocity.Y, Velocity.X) + Math.PI / 2);
                return radians;
            }
        }

        public Vector2 TopLeftCorner
        {
            get
            {
                return Position - new Vector2(Radius, Radius);
            }
        }

        public Object(float m = 100, float r = 100, Vector2? pos = null, Vector2? vel = null)
        {
            Radius = r;
            Mass = m;
            if (pos == null) { pos = new Vector2(0, 0); }
            Position = pos.Value;
            if (vel == null) { vel = new Vector2(0, 0); }
            Velocity = vel.Value;
        }

        internal void Update(Vector2 force, TimeSpan deltaTime)
        {
            Acceleration = force / this.Mass;
            Velocity += Acceleration * (float)deltaTime.TotalSeconds;
            Position += Velocity * (float)deltaTime.TotalSeconds;
        }

        internal void Collide(Object p2)
        {
            this.Velocity = new Vector2(0, 0);
            p2.Velocity = new Vector2(0, 0);
        }

        internal void changeDesnity()
        {

        }

        internal virtual void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Sprite, this.TopLeftCorner, Color.White);
        }

        internal void drawNetForce(SpriteBatch spriteBatch, Texture2D arrow)
        {

            Vector2 location = this.Position;
            Rectangle sourceRectangle = new Rectangle(0, 0, arrow.Width, arrow.Height);
            Vector2 origin = new Vector2(arrow.Width / 2, arrow.Height); //rotate with respect to the bottom-middle point

            spriteBatch.Draw(arrow, location, sourceRectangle, Color.White, this.AccAngle, origin, (float)Acceleration.Length() / 10, SpriteEffects.None, 1);

        }

        internal void Update(Vector2 centerOfMass, float M, TimeSpan deltaTime)
        {
            Vector2 centerOfMassOfOtherObj = (centerOfMass * M - this.Mass * this.Position) / (M - this.Mass);
            Vector2 r = Vector2.Subtract(centerOfMassOfOtherObj, this.Position);
            Vector2 force = 10000F * M * this.Mass * Vector2.Normalize(r) / r.LengthSquared();

            this.Update(force, deltaTime);
        }
    }
}
