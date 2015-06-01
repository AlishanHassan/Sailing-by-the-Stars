using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sailing_by_the_Stars
{
    class Camera
    {
        public Vector2 Position;
        public float Zoom;
        public float Rotation;
        public Rectangle Bounds;

        public Camera(int x = 0, int y = 0, float z = 1.0f)
        {
            float Zoom = z;
            Position = new Vector2(x, y);
        }



        private Matrix TransformMatrix //http://gamedev.stackexchange.com/questions/59301/xna-2d-camera-scrolling-why-use-matrix-transform
        { 
            get
            {
                return 
                    Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                    Matrix.CreateRotationZ(Rotation) *
                    Matrix.CreateScale(Zoom) *
                    Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));
            }
        }


        public void PanCamera(Vector2 cameraMovement)
        {
            Position = Position + cameraMovement; //may need to do something at the bounds
        }

        public void ZoomCamera(float factor)
        {
            Zoom = Zoom + factor;
        }



    }
}
