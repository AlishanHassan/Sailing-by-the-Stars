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
        public int Id; // The same as the number on the sprite name. For finding the right sprite when loading game.
        public float Mass;
        public readonly float OriginalMass;
        public float Radius;
        public Vector2 Position;
        protected Vector2 PreviousPosition;
        public Vector2 Velocity;
        public Vector2 Acceleration;
        internal float AccAngle
        {
            get
            {
                float radians = (float)(Math.Atan2(Acceleration.Y, Acceleration.X) + Math.PI / 2);
                return radians;
            }
        }
        internal float VelAngle
        {
            get
            {
                float radians = (float)(Math.Atan2(Velocity.Y, Velocity.X) + Math.PI / 2);
                return radians;
            }
        }

        public Object(float m = 100, float r = 100, Vector2? pos = null, Vector2? vel = null)
        {
            Radius = r;
            Mass = m;
            OriginalMass = m;
            if (pos == null) { pos = Vector2.Zero; }
            Position = pos.Value;
            PreviousPosition = Position;
            if (vel == null) { vel = Vector2.Zero; }
            Velocity = vel.Value;
        }

        internal virtual void Move(TimeSpan deltaTime)
        {
            Velocity += Acceleration * (float)deltaTime.TotalSeconds;
            PreviousPosition = Position;
            Position += Velocity * (float)deltaTime.TotalSeconds;
        }

        internal static bool CheckCollision(Object o1, Object o2)
        {
            Vector2 r = Vector2.Subtract(o2.Position, o1.Position);
            if (r.Length() < o1.Radius + o2.Radius)
            {
                // go back one step
                o1.Position = o1.PreviousPosition;
                o2.Position = o2.PreviousPosition;
                // bounce
                Bounce(o1, o2);
                return true;
            }
            return false;
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
            o1.DecreaseHealth(18);
            o2.DecreaseHealth(18);
        }

        protected virtual bool DecreaseHealth(int damage)
        {
            // Blank. 
            // Ship class overrides this method to take real damage.
            return true; // true for alive and false for dead
        }

        public virtual bool IsDead()
        {
            return false;
        }

        internal bool CheckHitByLaser()
        {
            foreach (Laser laser in Laser.lasers)
            {
                if (Physics.distanceSq(laser.HitBoxLocation1, this.Position) < this.Radius * this.Radius - 100 ||
                    Physics.distanceSq(laser.HitBoxLocation2, this.Position) < this.Radius * this.Radius + 100)
                {
                    this.DecreaseHealth(10);
                    laser.hitTarget();
                    return true;
                }
            }
            return false;
        }

        public virtual bool explode()
        {
            return false;
        }
        public override string ToString()
        {
            return this.Id + "," + this.Mass + "," + this.Radius + "," + this.Position.X + "," + this.Position.Y + "," + this.Velocity.X + "," + this.Velocity.Y;
        }
    }
}
