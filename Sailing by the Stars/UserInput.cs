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
        private MainGame game;
        private object[] objects;
        private Camera camera;
        private Vector2 windowSize;
        public UserInput(MainGame game)
        {
            this.game = game;
            this.objects = game.allGravObjects;
            this.camera = game.Camera;
            this.windowSize = camera.WindowSize;
        }

        public void update()
        {
            if (game.gameState == MainGame.GameState.InGamePlay)
            {
                keyboardShortcut();
                cameraControl();
                densityControl();
            }
            else if (game.gameState == MainGame.GameState.InGamePause)
            {
                keyboardShortcut();
                cameraControl();
                densityControl();
            }
            else if (game.gameState == MainGame.GameState.MainMenu)
            {
                keyboardShortcut();
            }
        }

        private void cameraControl()
        {
            keyboardCameraControl();
            mouseCameraControl();
        }

        private int mousePanSpeed = 30;
        private void mouseCameraControl()
        {
            //basic camera control
            //might want to clean this up with its own class for the final build

            var mainMouseState = Mouse.GetState();
            //var mousePosition = new Point(mouseState.X, mouseState.Y);
            if (mainMouseState.X < 0 || mainMouseState.X > windowSize.X || mainMouseState.Y < 0 || mainMouseState.Y > windowSize.Y)
            {
                return;
            }

            if (mainMouseState.X > windowSize.X * 0.9)
            {
                Vector2 pan = new Vector2(-mousePanSpeed, 0);
                camera.Move(pan);
            }
            if (mainMouseState.X < windowSize.X * 0.1)
            {
                Vector2 pan = new Vector2(mousePanSpeed, 0);
                camera.Move(pan);
            }
            if (mainMouseState.Y > windowSize.Y * 0.9)
            {
                Vector2 pan = new Vector2(0, -mousePanSpeed);
                camera.Move(pan);
            }
            if (mainMouseState.Y < windowSize.Y * 0.1)
            {
                Vector2 pan = new Vector2(0, mousePanSpeed);
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
        private int keyboardPanSpeed = 50;
        private float keyboardZoomSpeed = 0.01f;
        private void keyboardCameraControl()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                Vector2 pan = new Vector2(keyboardPanSpeed, 0);
                camera.Move(pan);
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                Vector2 pan = new Vector2(-keyboardPanSpeed, 0);
                camera.Move(pan);
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                Vector2 pan = new Vector2(0, keyboardPanSpeed);
                camera.Move(pan);
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                Vector2 pan = new Vector2(0, -keyboardPanSpeed);
                camera.Move(pan);
            }

            if (keyboardState.IsKeyDown(Keys.OemMinus))
            {
                camera.SetZoom(-keyboardZoomSpeed);
            }
            if (keyboardState.IsKeyDown(Keys.OemPlus))
            {
                camera.SetZoom(keyboardZoomSpeed);
            }

        }

        private KeyboardState oldKeyState;
        private void keyboardShortcut()
        {
            KeyboardState newKeyState = Keyboard.GetState();

            // P for pause and resume
            if (oldKeyState.IsKeyUp(Keys.P) && newKeyState.IsKeyDown(Keys.P))
            {
                if (game.gameState == MainGame.GameState.InGamePlay)
                {
                    game.gameState = MainGame.GameState.InGamePause;
                }
                else if (game.gameState == MainGame.GameState.InGamePause)
                {
                    game.gameState = MainGame.GameState.InGamePlay;
                }
            }

            // Ctrl + S for saving game
            if ((newKeyState.IsKeyDown(Keys.LeftControl) || newKeyState.IsKeyDown(Keys.RightControl)) && newKeyState.IsKeyDown(Keys.S) && oldKeyState.IsKeyUp(Keys.S))
            {
                game.SaveGameToFile(@"C:\Users\Public\SavedGame");
            }

            // Ctrl + L for loading game
            if ((newKeyState.IsKeyDown(Keys.LeftControl) || newKeyState.IsKeyDown(Keys.RightControl)) && newKeyState.IsKeyDown(Keys.L) && oldKeyState.IsKeyUp(Keys.L))
            {
                game.LoadGame(@"C:\Users\Public\SavedGame");
                game.gameState = MainGame.GameState.InGamePlay;
            }

            // Ctrl + N for new game
            if ((newKeyState.IsKeyDown(Keys.LeftControl) || newKeyState.IsKeyDown(Keys.RightControl)) && newKeyState.IsKeyDown(Keys.N) && oldKeyState.IsKeyUp(Keys.N))
            {
                game.LoadGame(@"C:\Users\Public\InitGame");
                game.gameState = MainGame.GameState.InGamePlay;
            }

            // Ctrl + M for main menu
            if ((newKeyState.IsKeyDown(Keys.LeftControl) || newKeyState.IsKeyDown(Keys.RightControl)) && newKeyState.IsKeyDown(Keys.M) && oldKeyState.IsKeyUp(Keys.M))
            {
                if (game.gameState == MainGame.GameState.InGamePlay)
                {
                    game.gameState = MainGame.GameState.MainMenu;
                }
                else if (game.gameState == MainGame.GameState.MainMenu)
                {
                    game.gameState = MainGame.GameState.InGamePlay;
                }
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
                        o.Mass = o.Mass + 100;
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
                        if (o.Mass > 100)
                        {
                            o.Mass = o.Mass - 100;
                        }

                        //Debug.WriteLine(o.Mass);
                    }
                }
            }
        }


    }
}
