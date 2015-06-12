﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;


namespace Sailing_by_the_Stars
{
    /// <summary>
    /// This is the main type for your game.
    /// Test2
    /// </summary>
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        Graphic g;
        Audio audio;
        UserInput userInput;
        Physics physics;
        internal Camera Camera;
        HUD hud;
        Boolean hudOn = true;
        public enum GameState { TitleScreen, MainMenu, InGamePlay, InGamePause, GameWin };
        internal GameState gameState;
        internal Object[] allGravObjects;
        internal Ship s;
        int finishline = 20000;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            Window.Position = Point.Zero;
            Content.RootDirectory = "Content";
        }

        public void hudToggle()
        {
            hudOn = !hudOn;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            this.Window.Title = "Sailing by the Stars";
            gameState = GameState.TitleScreen;
            Camera = new Camera();
            Camera.WindowSize = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            hud = new HUD();
            base.Initialize();
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            g = new Graphic(this);
            ReadGameFromFile(null);
            g.loadSprites(allGravObjects);
            audio = new Audio(this);
            userInput = new UserInput(this);
            physics = new Physics(allGravObjects, audio);
        }



        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        public void playAudio()
        {
            //Debug.WriteLine(" audio is being called");
            //Debug.WriteLine(gameState);
            if (gameState == GameState.InGamePlay)
            {
                audio.stopBGM();
                audio.playGameBGM();
                //Debug.WriteLine("gamestate audio is being called");
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            userInput.update();

            if (s.checkIfWon(finishline) == true)
            {
                gameState = GameState.GameWin;
                LoadGame();
            }

            if (gameState == GameState.InGamePlay)
            {
                physics.update(gameTime.ElapsedGameTime);
            }
            else if (gameState == GameState.InGamePause)
            {
                physics.updateAcceleration();
            }

            if (oldGameState != GameState.MainMenu && gameState == GameState.MainMenu)
            {
                g.setFadeInMenu();
            }
            else if (oldGameState == GameState.MainMenu && gameState != GameState.MainMenu)
            {
                g.setFadeOutMenu();
            }
            // fade effect for win
            if (oldGameState != GameState.GameWin && gameState == GameState.GameWin)
            {
                g.setFadeInWin();
            }
            else if (oldGameState == GameState.GameWin && gameState != GameState.GameWin)
            {
                g.setFadeOutWin();
            }


            oldGameState = gameState;

            base.Update(gameTime);
        }

        private GameState oldGameState;

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            if (gameState == GameState.InGamePlay || gameState == GameState.InGamePause)
            {
                g.drawAllObj(allGravObjects);
                g.drawFinishLine(gameTime.ElapsedGameTime, finishline);
                if (hudOn == true)
                {
                    g.drawHUD(); //draw this last so it's on top of the objects
                }
                g.drawMainMenu(gameTime.ElapsedGameTime);

            }
            else if (gameState == GameState.MainMenu)
            {
                g.drawMainMenu(gameTime.ElapsedGameTime);
            }
            else if (gameState == GameState.GameWin)
            {
                g.drawGameWin(gameTime.ElapsedGameTime);
            }
            else if (gameState == GameState.TitleScreen)
            {
                g.drawTitleScreen();
            }
            base.Draw(gameTime);
        }

        public Vector3 GetScreenScale()
        {
            var scaleX = 1.0f; //loat)graphics.width / (float)_width;
            var scaleY = 1.0f; // (float)graphics.Viewport.Height / (float)_height;
            return new Vector3(scaleX, scaleY, 1.0f);
        }

        public void SaveGameToFile(string path = @"C:\Users\Public\SavedGame")
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
            {
                file.WriteLine(Camera.ToString());
                foreach (object o in allGravObjects)
                {
                    file.WriteLine(o.ToString());
                }
            }
        }

        public void LoadGame(string path = @"C:\Users\Public\InitGame")
        {
            ReadGameFromFile(path);
            physics = new Physics(allGravObjects, audio);
            userInput = new UserInput(this);
        }

        private void ReadGameFromFile(string path = null)
        // use null to always raise exception for a default game level so that changes made here can be reflected in the InitGame file
        // later we can change it so that different files are for different levels
        {
            string[] lines;
            try
            {
                lines = System.IO.File.ReadAllLines(path);
            }
            catch (Exception) // when there is no path specified or the file doesn't exist, create a new game file "InitGame"
            {
                string cameraSetting = "1280,720,0,0,0,0,0,.3";
                string p1 = "0,8,1000,1500";
                string p2 = "0,2,1000,1250,";
                string p3 = "0,3,100,150,";
                string p4 = "0,4,1000,1500,";
                string p5 = "0,5,1000,1250,";
                string p6 = "0,6,1000,1250,";
                string p7 = "0,7,1000,1250,";
                string p8 = "0,8,1000,1500,";
                string es = "-1,1,100,10,";
                lines = new string[] { cameraSetting, "1,0,100,10,-2000,-360,450,0", 
                //Tutorial part
                p2+"0,1500,0,0", p2+"0,-2500,0,0",p2+"2750,1500,0,0", p2+"2750,-2500,0,0",p2+"5500,1500,0,0", p2+"5500,-2500,0,0",
                //Moons around a larger planet navigation
                p4+"11000,-1000,0,0", p3+"9000,-1900,0,0", p3+"11000,-2900,0,0", p3+"13000,-1900,0,0",p3+"13000, 100,0,0" ,p3+"11000,900,0,0" ,p3+"9000,100,0,0",
                //Other planets
                p7+"750,5500,0,0",p5+"6600,6000,0,0",p6+"11000,5500,0,0",
                p6+"1500,-6000,0,0",p7+"7500,-7000,0,0",p5+"12000,-6500,0,0",
                p6+"15500,-5000,0,0",p7+"17500,-800,0,0",p5+"21000,-300,0,0",
                //p8+"20500,8000,0,0",
                //"0,2,150,125,-100,700,0,0", "0,3,150,125,300,-800,0,0",  "0,4,150,125,3000,0,0,0", "0,5,150,125,7000,500,0,0"
                
                //Enemy ships
                es+"11000,-3600,-300,0", es+"12000,-8500,300,0",es+"12000,7000,250,0"
                };
                System.IO.File.WriteAllLines(@"C:\Users\Public\InitGame", lines);
            }

            float[] cam = lines[0].Split(',').Select(float.Parse).ToArray();
            Camera = new Camera(new Vector2(cam[0], cam[1]), new Vector2(cam[2], cam[3]), new Vector2(cam[4], cam[5]), cam[6], cam[7]);

            allGravObjects = new Object[lines.Length - 1];
            for (int i = 1; i < lines.Length; i++)
            {
                float[] line = lines[i].Split(',').Select(float.Parse).ToArray();
                if (line[0] == 0) // Planet
                {
                    allGravObjects[i - 1] = new Planet(line[2], line[3], new Vector2(line[4], line[5]), new Vector2(line[6], line[7]));
                    allGravObjects[i - 1].Id = (int)(line[1]);
                }
                else if (line[0] == 1) // Ship
                {
                    s = new Ship(line[2], line[3], new Vector2(line[4], line[5]), new Vector2(line[6], line[7]));
                    allGravObjects[i - 1] = s;
                    allGravObjects[i - 1].Id = (int)(line[1]);
                }
                else if (line[0] == -1) // EnemyShip
                {
                    allGravObjects[i - 1] = new EnemyShip(line[2], line[3], new Vector2(line[4], line[5]), new Vector2(line[6], line[7]));
                    allGravObjects[i - 1].Id = (int)(line[1]);
                }
            }


        }

    }
}
