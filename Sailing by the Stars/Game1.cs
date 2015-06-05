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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Object[] allGravObjects;
        Texture2D arrow;
        DensityControl densityControl;
        //float gConst = 6.67384E-11F;
        Camera Camera = new Camera();
        HUD hud = new HUD();
        public enum GameState { MainMenu, InGame };
        GameState gameState;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            Camera.ScreenSize = new Vector2(1280, 720);
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
            densityControl = new DensityControl(allGravObjects, Camera);

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


            // TODO: Add your update logic here
            densityControl.update();



            //basic camera control
            //might want to clean this up with its own class for the final build

            var mainMouseState = Mouse.GetState();
            //var mousePosition = new Point(mouseState.X, mouseState.Y);
            if (mainMouseState.X > 1152 && mainMouseState.X < 1280 && mainMouseState.Y > 0 && mainMouseState.Y < 720)
            {
                Vector2 pan = new Vector2(-50, 0);
                Camera.Move(pan);
            }
            if (mainMouseState.X < 128 && mainMouseState.X > 0 && mainMouseState.Y > 0 && mainMouseState.Y < 720)
            {
                Vector2 pan = new Vector2(50, 0);
                Camera.Move(pan);
            }
            if (mainMouseState.Y > 648 && mainMouseState.Y < 720 && mainMouseState.X > 0 && mainMouseState.X < 1280)
            {
                Vector2 pan = new Vector2(0, -50);
                Camera.Move(pan);
            }
            if (mainMouseState.Y < 72 && mainMouseState.Y > 0 && mainMouseState.X > 0 && mainMouseState.X < 1280)
            {
                Vector2 pan = new Vector2(0, 50);
                Camera.Move(pan);
            }

            /*
            if (mainMouseState.ScrollWheelValue > 0)
            {
                Camera.SetZoom(.01f);
            }
             */

            Debug.WriteLine(mainMouseState.ScrollWheelValue);

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                //Debug.WriteLine("Ding dong left");
                Vector2 pan = new Vector2(50, 0);
                Camera.Move(pan);
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                //Debug.WriteLine("Ding dong right");
                Vector2 pan = new Vector2(-50, 0);
                Camera.Move(pan);
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                //Debug.WriteLine("Ding dong up");
                Vector2 pan = new Vector2(0, 50);
                Camera.Move(pan);
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                //Debug.WriteLine("Ding dong down");
                Vector2 pan = new Vector2(0, -50);
                Camera.Move(pan);
            }

            if (keyboardState.IsKeyDown(Keys.O))
            {
                //Debug.WriteLine("Zoom out");
                Camera.SetZoom(-.01f);
            }
            if (keyboardState.IsKeyDown(Keys.P))
            {
                //Debug.WriteLine("Zoom in");
                Camera.SetZoom(.01f);
            }




            // a second version for calculation the acceleration - using center of mass
            // altough the runtime for acceleration calculation is only 2n, the collision detection still takes n^2.
            // so I'm not sure which one we use
            Vector2 centerOfMass = new Vector2(0, 0);
            float totalMass = 0;
            foreach (Object obj in allGravObjects)
            {
                centerOfMass += obj.Mass * obj.Position;
                totalMass += obj.Mass;
            }
            centerOfMass /= totalMass;

            foreach (Object obj in allGravObjects)
            {
                obj.Update(centerOfMass, totalMass, gameTime.ElapsedGameTime);
            }

            for (int i = 0; i < allGravObjects.Length; i++)
            {
                var o1 = allGravObjects[i];
                for (int j = i + 1; j < allGravObjects.Length; j++)
                {
                    var o2 = allGravObjects[j];
                    Object.CheckCollision(o1, o2, gameTime.ElapsedGameTime);
                }
            }



            //foreach (Object p in allGravObjects)
            //{
            //    Vector2 force = new Vector2(0, 0);
            //    foreach (Object p2 in allGravObjects)
            //    {
            //        if (!p2.Equals(p))
            //        {
            //            Vector2 r = Vector2.Subtract(p2.Position, p.Position);
            //            if (r.Length() > p.Radius + p2.Radius)
            //            {
            //                force += 5000F * p2.Mass * p.Mass * Vector2.Normalize(r) / r.LengthSquared(); //decreased this to 5000 from 10000 to slow it down for analysis
            //            }
            //            else
            //            {
            //                p.Collide(p2);
            //            }

            //        }
            //    }

            //    p.Move(force, gameTime.ElapsedGameTime);
            //}
            var mouseState = Mouse.GetState();
            Console.WriteLine(mouseState.X);
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
