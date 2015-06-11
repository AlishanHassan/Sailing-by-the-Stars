using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Sailing_by_the_Stars
{
    class Laser
    {
        internal static float duration = 0.5f;
        internal static int range = 2000;
        private float timeLeft = duration; //seconds
        internal Vector2 Position;
        internal Vector2 Direction;
        internal float Angle;
        private Color color;
        private static Color[] colors = new Color[] { Color.Green, Color.White, Color.Red, Color.Yellow, Color.Blue };
        private Random rand = new Random();
        internal Color Color
        {
            get
            {
                float a = (1 - Math.Abs(1 - 2 * timeLeft / duration));
                return this.color;
            }
        }

        public static List<Laser> lasers = new List<Laser>();
        public Laser(Vector2 source, Vector2 dest)
        {
            this.Position = new Vector2(source.X, source.Y);
            this.Direction = Vector2.Normalize(Vector2.Subtract(dest, source));
            this.Angle = (float)(Math.Atan2(Direction.Y, Direction.X) + Math.PI / 2);
            this.color = colors[rand.Next(colors.Length)];
        }

        public static void Update(TimeSpan elapsedTime)
        {
            for (int i = lasers.Count - 1; i >= 0; i--)
            {
                var ls = lasers[i];
                ls.timeLeft -= (float)elapsedTime.TotalSeconds;
                ls.Position += ls.Direction * (float)elapsedTime.TotalSeconds / duration * range;
                if (lasers[i].timeLeft < 0) { lasers.RemoveAt(i); }
            }
        }


    }
}
