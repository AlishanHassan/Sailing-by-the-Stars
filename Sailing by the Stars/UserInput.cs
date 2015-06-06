using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Sailing_by_the_Stars
{
    class UserInput
    {
        private object[] objects;
        private Camera camera;
        private MainGame game;
        public UserInput(MainGame game)
        {
            this.objects = game.allGravObjects;
            this.camera = game.Camera;
            this.game = game;
        }

        public void update()
        {
            keyboard();
            cameraControl();
            if (!game.pause)
            {
                densityControl();
            }
        }

        private void cameraControl()
        {
            keyboardCameraControl();
            mouseCameraControl();
        }

        private void mouseCameraControl()
        {
            //basic camera control
            //might want to clean this up with its own class for the final build

            var mainMouseState = Mouse.GetState();
            //var mousePosition = new Point(mouseState.X, mouseState.Y);
            if (mainMouseState.X > 1152 && mainMouseState.X < 1280 && mainMouseState.Y > 0 && mainMouseState.Y < 720)
            {
                Vector2 pan = new Vector2(-50, 0);
                camera.Move(pan);
            }
            if (mainMouseState.X < 128 && mainMouseState.X > 0 && mainMouseState.Y > 0 && mainMouseState.Y < 720)
            {
                Vector2 pan = new Vector2(50, 0);
                camera.Move(pan);
            }
            if (mainMouseState.Y > 648 && mainMouseState.Y < 720 && mainMouseState.X > 0 && mainMouseState.X < 1280)
            {
                Vector2 pan = new Vector2(0, -50);
                camera.Move(pan);
            }
            if (mainMouseState.Y < 72 && mainMouseState.Y > 0 && mainMouseState.X > 0 && mainMouseState.X < 1280)
            {
                Vector2 pan = new Vector2(0, 50);
                camera.Move(pan);
            }

            /*
            if (mainMouseState.ScrollWheelValue > 0)
            {
                Camera.SetZoom(.01f);
            }
             */

            //Debug.WriteLine(mainMouseState.ScrollWheelValue);
        }

        private void keyboardCameraControl()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                Vector2 pan = new Vector2(50, 0);
                camera.Move(pan);
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                Vector2 pan = new Vector2(-50, 0);
                camera.Move(pan);
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                Vector2 pan = new Vector2(0, 50);
                camera.Move(pan);
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                Vector2 pan = new Vector2(0, -50);
                camera.Move(pan);
            }

            if (keyboardState.IsKeyDown(Keys.O))
            {
                camera.SetZoom(-.01f);
            }
            if (keyboardState.IsKeyDown(Keys.P))
            {
                camera.SetZoom(.01f);
            }

        }

        private KeyboardState oldKeyState;
        private void keyboard()
        {
            KeyboardState newKeyState = Keyboard.GetState();

            if (oldKeyState.IsKeyUp(Keys.I) && newKeyState.IsKeyDown(Keys.I))
            {
                game.pause = !game.pause;
                Debug.WriteLine(game.pause);
            }

            oldKeyState = newKeyState;
        }


        private void densityControl()
        {
            var mouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(mouseState.Position.X, mouseState.Position.Y);
            mousePosition = Vector2.Transform(mousePosition, Matrix.Invert(camera.GetTransform()));
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                foreach (Object o in objects)
                {
                    if ((mousePosition.X - o.Position.X) * (mousePosition.X - o.Position.X) + (mousePosition.Y - o.Position.Y) * (mousePosition.Y - o.Position.Y) <= o.Radius * o.Radius)
                    {
                        o.Mass = o.Mass + 20;
                        //Debug.WriteLine(o.Mass);
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

                        //Debug.WriteLine(o.Mass);
                    }
                }
            }
        }
    }
}
