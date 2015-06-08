using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sailing_by_the_Stars
{
    class Graphic
    {
        public SpriteBatch spriteBatch;
        public Texture2D arrow;
        public Texture2D menuSprite;
        public Texture2D[] planetSprites;
        public Texture2D[] shipSprites;
        private Microsoft.Xna.Framework.Content.ContentManager Content;
        private MainGame game;

        public Graphic(MainGame game)
        {
            this.game = game;
            this.Content = game.Content;
            this.spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this.menuSprite = Content.Load<Texture2D>("mainmenu");
            this.arrow = Content.Load<Texture2D>("arrow");
            this.planetSprites = new Texture2D[8];
            this.shipSprites = new Texture2D[2];
        }

        internal void loadSprites(Object[] allGravObjects)
        {
            foreach (Object o in allGravObjects)
            {
                if (o is Planet)
                {
                    if (planetSprites[o.Id] == null)
                    {
                        planetSprites[o.Id] = Content.Load<Texture2D>("planet-" + o.Id);
                    }
                }
                else if (o is Ship)
                {
                    if (shipSprites[o.Id] == null)
                    {
                        shipSprites[o.Id] = Content.Load<Texture2D>("ship-" + o.Id);
                    }
                }
            }
        }

        internal void drawHUB()
        {
            spriteBatch.Begin();
            //TODO: draw the HUD here
            //spriteBatch.DrawString(font, "Health", new Vector2(100, 700), Color.Yellow);  //this will ultimately be in the draw method in HUD
            //hud.Draw();
            spriteBatch.End();
        }

        internal void drawAllObj(Object[] allGravObjects)
        {
            var screenScale = game.GetScreenScale();
            var viewMatrix = game.Camera.GetTransform();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied,
                                           null, null, null, null, viewMatrix * Matrix.CreateScale(screenScale));
            foreach (Object o in allGravObjects)
            {
                if (o is Planet)
                {
                    drawPlanet((Planet)o);
                }
                else if (o is Ship)
                {
                    drawShip((Ship)o);
                }
                //o.draw(spriteBatch);
                drawNetAcceleration(o);
            }
            spriteBatch.End();
        }

        private void drawPlanet(Planet p)
        {
            var sprite = planetSprites[p.Id];
            Vector2 topLeftCorner = p.Position - new Vector2(sprite.Width / 2, sprite.Height / 2);
            spriteBatch.Draw(sprite, topLeftCorner, Color.White);
        }

        private void drawShip(Ship s)
        {
            var sprite = shipSprites[s.Id];
            Vector2 location = s.Position;
            Rectangle sourceRectangle = new Rectangle(0, 0, sprite.Width, sprite.Height);
            Vector2 origin = new Vector2(sprite.Width / 2, sprite.Height / 2); //rotate with respect to the center point

            spriteBatch.Draw(sprite, location, sourceRectangle, Color.White, s.VelAngle, origin, 1, SpriteEffects.None, 1);

        }

        private void drawNetAcceleration(Object o)
        {
            Vector2 location = o.Position;
            Rectangle sourceRectangle = new Rectangle(0, 0, arrow.Width, arrow.Height);
            Vector2 origin = new Vector2(arrow.Width / 2, arrow.Height); //rotate with respect to the bottom-middle point
            float size = Math.Min((float)o.Acceleration.Length() / 10, 30);
            spriteBatch.Draw(arrow, location, sourceRectangle, Color.White, o.AccAngle, origin, size, SpriteEffects.None, 1);
        }

        internal void drawMainMenu()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(menuSprite, new Vector2(0, 0));
            spriteBatch.End();
        }
    }
}
