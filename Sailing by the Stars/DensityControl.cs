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
        private Camera camera;
        public DensityControl(object[] allGravObjects, Camera camera)
        {
            this.objects = allGravObjects;
            this.camera = camera;
        }

        public void update()
        {
            var mouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(mouseState.Position.X, mouseState.Position.Y);
            mousePosition = Vector2.Transform(mousePosition, Matrix.Invert(camera.GetTransform()));
            //mousePosition -= new Vector2(640, 360);
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                //Debug.WriteLine("\nMouse pos: ");
                //Debug.WriteLine(mousePosition);
                //Debug.WriteLine("Obj position:");
                foreach (Object o in objects)
                {
                    //Debug.WriteLine(o.Position);
                    if ((mousePosition.X - o.Position.X) * (mousePosition.X - o.Position.X) + (mousePosition.Y - o.Position.Y) * (mousePosition.Y - o.Position.Y) <= o.Radius * o.Radius)
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
                    if ((mousePosition.X - o.Position.X) * (mousePosition.X - o.Position.X) + (mousePosition.Y - o.Position.Y) * (mousePosition.Y - o.Position.Y) <= o.Radius * o.Radius)
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
