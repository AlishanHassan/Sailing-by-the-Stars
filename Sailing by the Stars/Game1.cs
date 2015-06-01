﻿using Microsoft.Xna.Framework;
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
        //float gConst = 6.67384E-11F;

        public Game1()
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

            allGravObjects = new Object[3];
            allGravObjects[0] = new Object(100, 100, 25, 112);
            allGravObjects[1] = new Object(1100, 200, 24, 85);
            for (int i = 0; i < 2; i++)
            {
                int val = (i % 2)+1;
                allGravObjects[i].Sprite = Content.Load<Texture2D>("planet-" + val);
            }

            allGravObjects[2] = new Object(600, 700, 50, 50);
            for (int i = 0; i < 1; i++)
            {
                allGravObjects[2].Sprite = Content.Load<Texture2D>("ship-" + (i + 1));
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
            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
               foreach(Object o in allGravObjects)
               {
                   if ((mouseState.X >= (o.Position.X - o.Radius)) && (mouseState.X <= (o.Position.X + o.Radius)) && (mouseState.Y >= (o.Position.Y - o.Radius)) && (mouseState.Y <= (o.Position.Y + o.Radius)))
                   {
                       o.Mass = o.Mass + 100;
                       Debug.WriteLine(o.Mass);
                   }
               }
            }
            if (mouseState.RightButton == ButtonState.Pressed)
            {
                foreach (Object o in allGravObjects)
                {
                    if ((mouseState.X >= (o.Position.X - o.Radius)) && (mouseState.X <= (o.Position.X + o.Radius)) && (mouseState.Y >= (o.Position.Y - o.Radius)) && (mouseState.Y <= (o.Position.Y + o.Radius)))
                    {
                        o.Mass = o.Mass - 100;
                        Debug.WriteLine(o.Mass);
                    }
                }
            }
            foreach (Object p in allGravObjects)
            {
                Vector2 force = new Vector2(0, 0);
                foreach (Object p2 in allGravObjects)
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
            foreach (Object p in allGravObjects)
            {
                spriteBatch.Draw(p.Sprite, p.TopLeftCorner, Color.White);
                p.drawNetForce(spriteBatch, arrow);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
