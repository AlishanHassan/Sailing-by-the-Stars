using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sailing_by_the_Stars
{
    //http://stackoverflow.com/questions/17452808/moving-a-camera-in-xna-c-sharp
    public class Camera
    {
        public Camera()
        {
            Zoom = 1;
            Position = Vector2.Zero;
            Rotation = 0;
            Origin = Vector2.Zero;
            Position = Vector2.Zero;
        }

        public float Zoom { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }

        public void Move(Vector2 direction)
        {
            Position += direction;
        }

        public void SetZoom(float zoomChange)
        {
            Zoom += zoomChange;
        }

        public Matrix GetTransform()
        {
            var translationMatrix = Matrix.CreateTranslation(new Vector3(Position.X, Position.Y, 0));
            var rotationMatrix = Matrix.CreateRotationZ(Rotation);
            var scaleMatrix = Matrix.CreateScale(new Vector3(Zoom, Zoom, 1));
            var originMatrix = Matrix.CreateTranslation(new Vector3(Origin.X, Origin.Y, 0));

            return translationMatrix * rotationMatrix * scaleMatrix * originMatrix;
        }
    }
}

    /*
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
}*/