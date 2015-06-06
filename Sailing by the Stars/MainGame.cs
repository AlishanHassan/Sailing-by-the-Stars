using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
        UserInput userInput;
        Physics physics;
        //float gConst = 6.67384E-11F;
        internal Camera Camera = new Camera();
        HUD hud = new HUD();
        public enum GameState { MainMenu, InGame };
        GameState gameState;
        internal bool pause = false;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            Camera.WindowSize = new Vector2(1280, 720);
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
            gameState = GameState.MainMenu;
            this.IsMouseVisible = true;
            this.Window.Title = "Sailing by the Stars";
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here


            allGravObjects = new Object[5];
            userInput = new UserInput(this);
            physics = new Physics(allGravObjects);

            allGravObjects[0] = new Planet(150, 125, new Vector2(-300, 50));
            allGravObjects[1] = new Planet(60, 1250, new Vector2(2000, 1650));
            allGravObjects[2] = new Planet(150, 228, new Vector2(2100, -150));
            allGravObjects[3] = new Planet(60, 1250, new Vector2(6000, 2800));
            for (int i = 0; i < 4; i++)
            {
                int val = i + 1;
                allGravObjects[i].Sprite = Content.Load<Texture2D>("planet-" + val);
            }


            Vector2 initialVelocity = new Vector2(60, 0);//ship initial velocity
            allGravObjects[4] = new Ship(100, 75, new Vector2(-640, -360), initialVelocity); //increased the radius for the ship from 5 to 38 so it's easier to click for the demo

            for (int i = 0; i < 1; i++)
            {
                allGravObjects[4].Sprite = Content.Load<Texture2D>("ship-" + (i + 1));
            }

            arrow = Content.Load<Texture2D>("arrow");



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
            if (this.pause)
            {
                userInput.update();
                base.Update(gameTime);
                return;
            }

            // TODO: Add your update logic here
            userInput.update();

            physics.update(gameTime.ElapsedGameTime);
            
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

            base.Draw(gameTime);
        }

        public Vector3 GetScreenScale()
        {
            var scaleX = 1.0f; //loat)graphics.width / (float)_width;
            var scaleY = 1.0f; // (float)graphics.Viewport.Height / (float)_height;
            return new Vector3(scaleX, scaleY, 1.0f);
        }



    }
}
