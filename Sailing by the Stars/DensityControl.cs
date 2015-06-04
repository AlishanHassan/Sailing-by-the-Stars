using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Sailing_by_the_Stars
{
    class DensityControl
    {
        private object[] objects;
        public DensityControl(object[] allGravObjects)
        {
            this.objects = allGravObjects;
        }

        public void update()
        {
            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                foreach (Object o in objects)
                {
                    if ((mouseState.X >= (o.Position.X - o.Radius)) && (mouseState.X <= (o.Position.X + o.Radius)) && (mouseState.Y >= (o.Position.Y - o.Radius)) && (mouseState.Y <= (o.Position.Y + o.Radius)))
                    {
                        o.Mass = o.Mass + 20;
                        Debug.WriteLine(o.Mass);
                    }
                }
            }
            if (mouseState.RightButton == ButtonState.Pressed)
            {
                foreach (Object o in objects)
                {
                    if ((mouseState.X >= (o.Position.X - o.Radius)) && (mouseState.X <= (o.Position.X + o.Radius)) && (mouseState.Y >= (o.Position.Y - o.Radius)) && (mouseState.Y <= (o.Position.Y + o.Radius)))
                    {
                        if (o.Mass > 20)
                        {
                            o.Mass = o.Mass - 20;
                        }

                        Debug.WriteLine(o.Mass);
                    }
                }
            }
        }
    }
}
