﻿using Microsoft.Xna.Framework;
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
        private KeyboardState newKeyState;
        private KeyboardState oldKeyState;
        private MouseState mouseState;
        private MouseState oldMouseState;
        private Boolean cheatCode = false;
        private int num = 0;

        public UserInput(MainGame game)
        {
            this.game = game;
            this.objects = game.allGravObjects;
            this.camera = game.Camera;
            this.windowSize = camera.WindowSize;
        }

        public void update()
        {
            newKeyState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            if (game.gameState == MainGame.GameState.InGamePlay)
            {
                InGameKeyboardShortcuts();
                cameraControl();
                densityControl();
            }
            else if (game.gameState == MainGame.GameState.InGamePause)
            {
                InGameKeyboardShortcuts();
                Shortcut_Ctrl_C_toggleControlsPaused();
                cameraControl();
                densityControl();
            }
            else if (game.gameState == MainGame.GameState.MainMenu)
            {
                MainMenuKeyboardShortcuts();
                MainMenuMouseInput();
                CheatCode();
            }
            else if (game.gameState == MainGame.GameState.TitleScreen)
            {
                clickToContinueStory();
            }
            else if (game.gameState == MainGame.GameState.Story)
            {
                clickToContinueControls();
            }
            else if (game.gameState == MainGame.GameState.Controls)
            {
                clickToContinue();
            }
            else if (game.gameState == MainGame.GameState.ControlsPause)
            {
                Shortcut_Ctrl_C_toggleControlsPaused();
                clickToContinuePause();
            }
            else if (game.gameState == MainGame.GameState.GameWin || game.gameState == MainGame.GameState.GameLoseNoHP || game.gameState == MainGame.GameState.GameLoseDeepSpace)
            {
                GameOverKeyboardShortcuts();
                clickIfWinLose();
            }

            oldKeyState = newKeyState;
            oldMouseState = mouseState;
        }

        private void MainMenuMouseInput()
        {
            if (mouseState.X > 425 && mouseState.X < 775)
            {
                if (mouseState.Y > 325 && mouseState.Y < 375 && mouseState.LeftButton == ButtonState.Pressed)
                {
                    game.LoadGame(@"C:\Users\Public\InitGame");
                    game.gameState = MainGame.GameState.InGamePlay;
                }
                if (mouseState.Y > 400 && mouseState.Y < 440 && mouseState.LeftButton == ButtonState.Pressed)
                {
                    game.LoadGame(@"C:\Users\Public\SavedGame");
                    game.gameState = MainGame.GameState.InGamePlay;
                }

            }
        }

        private void CheatCode()
        {
            
            if (cheatCode == false && num == 0 && newKeyState.IsKeyDown(Keys.Up))
            {
                num += 1;
            }
            if (num == 1 && newKeyState.IsKeyDown(Keys.Up))
            {
                num += 1;
            }
            if (num == 2 && newKeyState.IsKeyDown(Keys.Down))
            {
                num += 1;
            }
            if (num == 3 && newKeyState.IsKeyDown(Keys.Down))
            {
                num += 1;
            }
            if (num == 4 && newKeyState.IsKeyDown(Keys.Left))
            {
                num += 1;
            }
            if (num == 5 && newKeyState.IsKeyDown(Keys.Right))
            {
                num += 1;
            }
            if (num == 6 && newKeyState.IsKeyDown(Keys.Left))
            {
                num += 1;
            }
            if (num == 7 && newKeyState.IsKeyDown(Keys.Right))
            {
                num += 1;
            }
            if (num == 8 && newKeyState.IsKeyDown(Keys.B))
            {
                num += 1;
            }
            if (num == 9 && newKeyState.IsKeyDown(Keys.A))
            {
                num += 1;
            }
            if (num == 10 && newKeyState.IsKeyDown(Keys.Enter))
            {
                num += 1;
                cheatCode = true;
            }
            if (cheatCode == true)
            {
                Debug.WriteLine("cheat code is on");
                game.cheatCode = true;
                cheatCode = false;
            }
        }
        private void InGameKeyboardShortcuts()
        {
            Shortcut_P_pause();
            Shortcut_Ctrl_S_saveGame();
            Shortcut_Ctrl_L_loadGame();
            Shortcut_Ctrl_N_newGame();
            Shortcut_Ctrl_M_toggleMenu();
            Shortcut_Ctrl_H_toggleHUD();
            Shortcut_Ctrl_F_focusOnPlayerShip();
        }
        private void MainMenuKeyboardShortcuts()
        {
            Shortcut_Ctrl_L_loadGame();
            Shortcut_Ctrl_N_newGame();
            Shortcut_Ctrl_M_toggleMenu();
            Shortcut_Ctrl_C_toggleControls();
        }

        private void GameOverKeyboardShortcuts()
        {
            Shortcut_Ctrl_M_toggleMenu();
            Shortcut_Ctrl_N_newGame();
        }
        private void clickToContinue()
        {
            if (mouseState.X > 0 && mouseState.X < windowSize.X && mouseState.Y > 0 && mouseState.Y < windowSize.Y && (mouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed))
            {
                game.gameState = MainGame.GameState.MainMenu;
            }
        }
        private void clickToContinuePause()
        {
            if (mouseState.X > 0 && mouseState.X < windowSize.X && mouseState.Y > 0 && mouseState.Y < windowSize.Y && (mouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed))
            {
                game.gameState = MainGame.GameState.InGamePause;
            }
        }
        private void clickIfWinLose()
        {
            if (mouseState.X > 0 && mouseState.X < windowSize.X && mouseState.Y > 0 && mouseState.Y < windowSize.Y && (mouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed))
            {
                game.LoadGame(@"C:\Users\Public\InitGame");
                game.gameState = MainGame.GameState.TitleScreen;
            }
        }
        private void clickToContinueStory()
        {
            if (mouseState.X > 0 && mouseState.X < windowSize.X && mouseState.Y > 0 && mouseState.Y < windowSize.Y && (mouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed))
            {
                game.gameState = MainGame.GameState.Story;
            }
        }
        private void clickToContinueControls()
        {
            if (mouseState.X > 0 && mouseState.X < windowSize.X && mouseState.Y > 0 && mouseState.Y < windowSize.Y && (mouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed))
            {
                game.gameState = MainGame.GameState.Controls;
            }
        }

        private void cameraControl()
        {
            keyboardCameraControl();
            mouseCameraControl();
        }

        private int mousePanSpeed = 30;
        private float mouseZoomSpeed = 0.01f;
        private MouseState prevMouseState;
        private void mouseCameraControl()
        {
            //basic camera control
            //might want to clean this up with its own class for the final build

            MouseState mainMouseState = Mouse.GetState();
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
            if (mainMouseState.ScrollWheelValue > prevMouseState.ScrollWheelValue)
            {
                camera.SetZoom(mouseZoomSpeed);
            }
            if (mainMouseState.ScrollWheelValue < prevMouseState.ScrollWheelValue)
            {
                camera.SetZoom(-mouseZoomSpeed);
            }
            if (mainMouseState.MiddleButton == ButtonState.Pressed)
            {
                camera.Focus(game.s.Position);
                camera.DefaultZoom();
            }

            prevMouseState = mainMouseState;

        }

        //Keyboard camera control, both arrow keys and WASD for panning; +/- and Q/E for zoom

        private int keyboardPanSpeed = 50;
        private float keyboardZoomSpeed = 0.005f;
        private void keyboardCameraControl()
        {
            if (newKeyState.IsKeyDown(Keys.LeftControl) || newKeyState.IsKeyDown(Keys.RightControl))
            {
                return;
            }
            if (newKeyState.IsKeyDown(Keys.Left) || newKeyState.IsKeyDown(Keys.A))
            {
                Vector2 pan = new Vector2(keyboardPanSpeed, 0);
                camera.Move(pan);
            }
            if (newKeyState.IsKeyDown(Keys.Right) || newKeyState.IsKeyDown(Keys.D))
            {
                Vector2 pan = new Vector2(-keyboardPanSpeed, 0);
                camera.Move(pan);
            }
            if (newKeyState.IsKeyDown(Keys.Up) || newKeyState.IsKeyDown(Keys.W))
            {
                Vector2 pan = new Vector2(0, keyboardPanSpeed);
                camera.Move(pan);
            }
            if (newKeyState.IsKeyDown(Keys.Down) || newKeyState.IsKeyDown(Keys.S))
            {
                Vector2 pan = new Vector2(0, -keyboardPanSpeed);
                camera.Move(pan);
            }

            if (newKeyState.IsKeyDown(Keys.OemMinus) || newKeyState.IsKeyDown(Keys.Q))
            {
                camera.SetZoom(-keyboardZoomSpeed);
            }
            if (newKeyState.IsKeyDown(Keys.OemPlus) || newKeyState.IsKeyDown(Keys.E))
            {
                camera.SetZoom(keyboardZoomSpeed);
            }
        }

        private void Shortcut_Ctrl_F_focusOnPlayerShip()
        {
            if ((newKeyState.IsKeyDown(Keys.LeftControl) || newKeyState.IsKeyDown(Keys.RightControl)) && newKeyState.IsKeyDown(Keys.F) && oldKeyState.IsKeyUp(Keys.F))
            {
                camera.Focus(game.s.Position);
                camera.DefaultZoom();
            }
        }

        private void Shortcut_Ctrl_H_toggleHUD()
        {
            // Ctrl + H to toggle HUD
            if ((newKeyState.IsKeyDown(Keys.LeftControl) || newKeyState.IsKeyDown(Keys.RightControl)) && newKeyState.IsKeyDown(Keys.H) && oldKeyState.IsKeyUp(Keys.H))
            {
                game.hudToggle();
            }
        }

        private void Shortcut_Ctrl_M_toggleMenu()
        {
            // Ctrl + M for main menu
            if ((newKeyState.IsKeyDown(Keys.LeftControl) || newKeyState.IsKeyDown(Keys.RightControl)) && newKeyState.IsKeyDown(Keys.M) && oldKeyState.IsKeyUp(Keys.M))
            {
                if (game.gameState != MainGame.GameState.MainMenu) { game.gameState = MainGame.GameState.MainMenu; }
                else if (game.gameState == MainGame.GameState.MainMenu) { game.gameState = MainGame.GameState.InGamePlay; }
            }
        }
        private void Shortcut_Ctrl_C_toggleControls()
        {
            // Ctrl + C for contorls
            if ((newKeyState.IsKeyDown(Keys.LeftControl) || newKeyState.IsKeyDown(Keys.RightControl)) && newKeyState.IsKeyDown(Keys.C) && oldKeyState.IsKeyUp(Keys.C))
            {
                if (game.gameState != MainGame.GameState.Controls)
                { game.gameState = MainGame.GameState.Controls; }
            }
        }
        private void Shortcut_Ctrl_C_toggleControlsPaused()
        {
            // Ctrl + C for contorls
            if ((newKeyState.IsKeyDown(Keys.LeftControl) || newKeyState.IsKeyDown(Keys.RightControl)) && newKeyState.IsKeyDown(Keys.C) && oldKeyState.IsKeyUp(Keys.C))
            {
                if (game.gameState != MainGame.GameState.ControlsPause)
                { game.gameState = MainGame.GameState.ControlsPause; }
                else if (game.gameState == MainGame.GameState.ControlsPause) { game.gameState = MainGame.GameState.InGamePause; }
            }
        }

        private void Shortcut_Ctrl_N_newGame()
        {
            // Ctrl + N for new game
            if ((newKeyState.IsKeyDown(Keys.LeftControl) || newKeyState.IsKeyDown(Keys.RightControl)) && newKeyState.IsKeyDown(Keys.N) && oldKeyState.IsKeyUp(Keys.N))
            {
                game.LoadGame(@"C:\Users\Public\InitGame");
                game.gameState = MainGame.GameState.InGamePlay;
            }
        }

        private void Shortcut_Ctrl_L_loadGame()
        {
            // Ctrl + L to load game
            if ((newKeyState.IsKeyDown(Keys.LeftControl) || newKeyState.IsKeyDown(Keys.RightControl)) && newKeyState.IsKeyDown(Keys.L) && oldKeyState.IsKeyUp(Keys.L))
            {
                game.LoadGame(@"C:\Users\Public\SavedGame");
                game.gameState = MainGame.GameState.InGamePlay;
            }
        }

        private void Shortcut_Ctrl_S_saveGame()
        {
            // Ctrl + S to save game
            if ((newKeyState.IsKeyDown(Keys.LeftControl) || newKeyState.IsKeyDown(Keys.RightControl)) && newKeyState.IsKeyDown(Keys.S) && oldKeyState.IsKeyUp(Keys.S))
            {
                game.SaveGameToFile(@"C:\Users\Public\SavedGame");
            }
        }

        private void Shortcut_P_pause()
        {
            // P to pause and resume
            if (oldKeyState.IsKeyUp(Keys.P) && newKeyState.IsKeyDown(Keys.P))
            {
                if (game.gameState == MainGame.GameState.InGamePlay)
                {
                    game.gameState = MainGame.GameState.InGamePause;
                    game.pauseGameBGM();
                }
                else if (game.gameState == MainGame.GameState.InGamePause)
                {
                    game.gameState = MainGame.GameState.InGamePlay;
                    game.resumeGameBGM();
                }
            }
        }

        private void densityControl()
        {
            Vector2 mousePosition = new Vector2(mouseState.Position.X, mouseState.Position.Y);
            mousePosition = Vector2.Transform(mousePosition, Matrix.Invert(camera.GetTransform()));
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                foreach (Object o in objects)
                {
                    if (o.Mass < o.OriginalMass * 8 &&
                        (mousePosition.X - o.Position.X) * (mousePosition.X - o.Position.X) + (mousePosition.Y - o.Position.Y) * (mousePosition.Y - o.Position.Y) <= o.Radius * o.Radius)
                    {
                        o.Mass *= 1.05f;
                    }
                }
            }
            if (mouseState.RightButton == ButtonState.Pressed)
            {
                foreach (Object o in objects)
                {
                    if (o.Mass > .125 * o.OriginalMass &&
                        (mousePosition.X - o.Position.X) * (mousePosition.X - o.Position.X) + (mousePosition.Y - o.Position.Y) * (mousePosition.Y - o.Position.Y) <= o.Radius * o.Radius)
                    {
                        o.Mass /= 1.05f;
                    }

                }
            }
        }


    }
}
