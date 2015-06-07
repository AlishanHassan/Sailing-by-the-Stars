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
        public int Id; // The same as the number of the sprite name. For finding the right sprite when loading game.
        public float Mass;
        public float Radius;
        public Vector2 Position;
        protected Vector2 PreviousPosition;
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
            if (pos == null) { pos = Vector2.Zero; }
            Position = pos.Value;
            PreviousPosition = Position;
            if (vel == null) { vel = Vector2.Zero; }
            Velocity = vel.Value;
        }

        internal virtual void Move(Vector2 force, TimeSpan deltaTime)
        {
            Acceleration = force / this.Mass;
            Velocity += Acceleration * (float)deltaTime.TotalSeconds;
            PreviousPosition = Position;
            Position += Velocity * (float)deltaTime.TotalSeconds;
        }

        internal static void CheckCollision(Object o1, Object o2, TimeSpan elapsedTime)
        {
            Vector2 r = Vector2.Subtract(o2.Position, o1.Position);
            if (r.Length() < o1.Radius + o2.Radius)
            {
                // go back one step
                o1.Position = o1.PreviousPosition;
                o2.Position = o2.PreviousPosition;
                // bounce
                Bounce(o1, o2);
            }

        }

        internal static void Bounce(Object o1, Object o2)
        {
            var m1 = o1.Mass;
            var m2 = o2.Mass;
            var x1 = o1.Position;
            var x2 = o2.Position;
            var v1 = o1.Velocity;
            var v2 = o2.Velocity;
            Vector2 v1f = v1 - 2 * m2 / (m1 + m2) * Vector2.Dot(v1 - v2, x1 - x2) / (x1 - x2).LengthSquared() * (x1 - x2);
            Vector2 v2f = v2 - 2 * m1 / (m1 + m2) * Vector2.Dot(v2 - v1, x2 - x1) / (x2 - x1).LengthSquared() * (x2 - x1);
            o1.Velocity = v1f * .9f;
            o2.Velocity = v2f * .9f;
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
            Vector2 force = 5000F * (M - this.Mass) * this.Mass * Vector2.Normalize(r) / r.LengthSquared();

            this.Move(force, deltaTime);
        }

        public override string ToString()
        {
            return this.Id + "," + this.Mass + "," + this.Radius + "," + this.Position.X + "," + this.Position.Y + "," + this.Velocity.X + "," + this.Velocity.Y;
        }
    }
}
