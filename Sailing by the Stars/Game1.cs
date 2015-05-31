using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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
        Planet[] planets;
        Ship[] ships;
        Texture2D arrow;
        //float gConst = 6.67384E-11F;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            
            planets = new Planet[2];
            planets[0] = new Planet(650, 112, 20, 112);
            planets[1] = new Planet(100, 300, 20, 85);
            for (int i = 0; i < 2; i++)
            {
                planets[i].Sprite = Content.Load<Texture2D>("planet-" + (i + 1));
            }
             
            ships = new Ship[1];
            ships[0] = new Ship(-1000, 100, 5, 50);
            for (int i = 0; i < 1; i++)
            {
                ships[i].Sprite = Content.Load<Texture2D>("ship-" + (i + 1));
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            
            foreach (Planet p in planets)
            {
                Vector2 force = new Vector2(0, 0);
                foreach (Planet p2 in planets)
                {
                    if (!p2.Equals(p))
                    {
                        Vector2 r = Vector2.Subtract(p2.Position, p.Position);
                        if (r.Length() > p.Radius+p2.Radius)
                        {
                            force += 10000F * p2.Mass * p.Mass * Vector2.Normalize(r) / r.LengthSquared();
                        }
                        else
                        {
                            p.Collide(p2);
                        }

                    }
                }

                p.Update(force, gameTime.ElapsedGameTime);
            }
            Ship s = ships[0];
            Vector2 right = new Vector2(5000, 0);
            s.Update(right,gameTime.ElapsedGameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            foreach (Planet p in planets)
            {
                spriteBatch.Draw(p.Sprite, p.TopLeftCorner, Color.White);
                p.drawNetForce(spriteBatch, arrow);
            }

            foreach (Ship s in ships)
            {
                spriteBatch.Draw(s.Sprite, s.TopLeftCorner, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
