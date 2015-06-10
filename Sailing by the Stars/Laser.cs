using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sailing_by_the_Stars
{
    class Laser
    {
        internal float duration = 0.5f; //seconds
        internal Vector2 Position;
        internal Vector2 Direction;
        internal float Angle;

        public static List<Laser> lasers = new List<Laser>();
        public Laser(Vector2 source, Vector2 dest)
        {
            this.Position = new Vector2(source.X, source.Y);
            this.Direction = Vector2.Subtract(dest, source);
            this.Angle = (float)(Math.Atan2(Direction.Y, Direction.X) + Math.PI / 2);
        }

        public static void Update(TimeSpan elapsedTime)
        {
            for (int i = lasers.Count - 1; i >= 0; i--)
            {
                lasers[i].duration -= (float)elapsedTime.TotalSeconds;
                if (lasers[i].duration < 0) { lasers.RemoveAt(i); }
            }
        }


    }
}
