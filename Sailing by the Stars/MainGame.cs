﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using System.Diagnostics;


namespace Sailing_by_the_Stars
{
    /// <summary>
    /// This is the main type for your game.
    /// Test2
    /// </summary>
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        internal Object[] allGravObjects;
        Texture2D arrow;
        Texture2D menuSprite;
        UserInput userInput;
        Physics physics;
        internal Camera Camera;
        HUD hud;
        public enum GameState { MainMenu, InGamePlay, InGamePause };
        internal GameState gameState;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            Content.RootDirectory = "Content";
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
            gameState = GameState.MainMenu;
            Camera = new Camera();
            Camera.WindowSize = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            hud = new HUD();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        //protected void LoadContent1()
        //{
        //    // Create a new SpriteBatch, which can be used to draw textures.
        //    spriteBatch = new SpriteBatch(GraphicsDevice);

        //    // TODO: use this.Content to load your game content here
        //    menuSprite = Content.Load<Texture2D>("mainmenu");

        //    allGravObjects = new Object[5];
        //    userInput = new UserInput(this);
        //    physics = new Physics(allGravObjects);

        //    allGravObjects[0] = new Planet(150, 125, new Vector2(-300, 50));
        //    allGravObjects[1] = new Planet(60, 1250, new Vector2(2000, 1650));
        //    allGravObjects[2] = new Planet(150, 228, new Vector2(2100, -150));
        //    allGravObjects[3] = new Planet(60, 1250, new Vector2(6000, 2800));
        //    for (int i = 0; i < 4; i++)
        //    {
        //        int val = i + 1;
        //        allGravObjects[i].Id = val;
        //        allGravObjects[i].Sprite = Content.Load<Texture2D>("planet-" + val);
        //    }


        //    Vector2 initialVelocity = new Vector2(60, 0);//ship initial velocity
        //    allGravObjects[4] = new Ship(100, 75, new Vector2(-640, -360), initialVelocity); //increased the radius for the ship from 5 to 38 so it's easier to click for the demo

        //    for (int i = 0; i < 1; i++)
        //    {
        //        allGravObjects[4].Id = (i + 1);
        //        allGravObjects[4].Sprite = Content.Load<Texture2D>("ship-" + (i + 1));
        //    }

        //    arrow = Content.Load<Texture2D>("arrow");
        //}

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            menuSprite = Content.Load<Texture2D>("mainmenu");

            ReadGameFromFile();
            userInput = new UserInput(this);
            physics = new Physics(allGravObjects);

            updateSprites(); // want to use it somewhere else

            arrow = Content.Load<Texture2D>("arrow");
        }

        private void updateSprites()
        {
            foreach (Object o in allGravObjects)
            {
                if (o is Planet)
                {
                    o.Sprite = Content.Load<Texture2D>("planet-" + o.Id);
                }
                else if (o is Ship)
                {
                    o.Sprite = Content.Load<Texture2D>("ship-" + o.Id);
                }
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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

            if (gameState == GameState.InGamePlay)
            {
                physics.update(gameTime.ElapsedGameTime);
            }

            base.Update(gameTime);
        }

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

                spriteBatch.Begin();
                //TODO: draw the HUD here
                //spriteBatch.DrawString(font, "Health", new Vector2(100, 700), Color.Yellow);  //this will ultimately be in the draw method in HUD
                //hud.Draw();
                spriteBatch.End();

                var screenScale = GetScreenScale();
                var viewMatrix = Camera.GetTransform();

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied,
                                           null, null, null, null, viewMatrix * Matrix.CreateScale(screenScale));
                foreach (Object p in allGravObjects)
                {
                    p.draw(spriteBatch);
                    p.drawNetForce(spriteBatch, arrow);
                }
                spriteBatch.End();
            }
            else if (gameState == GameState.MainMenu)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(menuSprite, new Vector2(0, 0));
                spriteBatch.End();
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
            physics = new Physics(allGravObjects);
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
                string p2 = "0,2,150,1250,";
                string p3 = "0,3,500,150,";
                string p4 = "0,4,500,1500,";
                string p5 = "0,5,500,1250,";
                string p6 = "0,6,500,1250,";
                string p7 = "0,7,500,1250,";
                lines = new string[] { cameraSetting, "1,1,1000,10,-2000,-360,150,0", 
                //Tutorial part
                p2+"0,1500,0,0", p2+"0,-2500,0,0",p2+"2750,1500,0,0", p2+"2750,-2500,0,0",p2+"5500,1500,0,0", p2+"5500,-2500,0,0",
                //Moons around a larger planet navigation
                p4+"10500,-1400,0,0", p3+"9000,-1900,0,0", p3+"11000,-2900,0,0", p3+"13000,-1900,0,0",p3+"13000, 100,0,0" ,p3+"11000,900,0,0" ,p3+"9000,100,0,0",
                //Other planets
                p7+"750,5500,0,0",p5+"6600,6000,0,0",p6+"11000,5500,0,0",
                p6+"1500,-6000,0,0",p7+"7500,-7000,0,0",p5+"12000,-6500,0,0",
                p6+"15500,-5000,0,0",p7+"17500,-800,0,0",p5+"21000,-300,0,0",
                
                //"0,2,150,125,-100,700,0,0", "0,3,150,125,300,-800,0,0",  "0,4,150,125,3000,0,0,0", "0,5,150,125,7000,500,0,0"
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
                    allGravObjects[i - 1] = new Ship(line[2], line[3], new Vector2(line[4], line[5]), new Vector2(line[6], line[7]));
                    allGravObjects[i - 1].Id = (int)(line[1]);
                }
                else if (line[0] == -1) // EnemyShip
                {
                    allGravObjects[i - 1] = new EnemyShip(line[2], line[3], new Vector2(line[4], line[5]), new Vector2(line[6], line[7]));
                    allGravObjects[i - 1].Id = (int)(line[1]);
                }

            }

            updateSprites(); // have to update sprites right after reading data, otherwise the Sprites are null

        }

    }
}
