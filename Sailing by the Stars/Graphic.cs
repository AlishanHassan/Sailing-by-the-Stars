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
        public Texture2D winSprite;
        public Texture2D titleSprite;
        public Texture2D[] planetSprites;
        public Texture2D[] shipSprites;
        public Texture2D finishLine;
        public Texture2D hudSprite;
        public Texture2D healthBar;
        public Texture2D bar1;
        public Texture2D bar2;
        public Texture2D laserSprite;
        private Microsoft.Xna.Framework.Content.ContentManager Content;
        private MainGame game;

        public Graphic(MainGame game)
        {
            this.game = game;
            this.Content = game.Content;
            this.spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this.menuSprite = Content.Load<Texture2D>("mainmenu");
            this.winSprite = Content.Load<Texture2D>("win");
            this.titleSprite = Content.Load<Texture2D>("titlescreen");
            this.arrow = Content.Load<Texture2D>("arrow");
            this.planetSprites = new Texture2D[10];
            this.shipSprites = new Texture2D[2];
            this.finishLine = Content.Load<Texture2D>("finishline");
            this.hudSprite = Content.Load<Texture2D>("hud");
            this.healthBar = Content.Load<Texture2D>("healthbar");
            this.bar1 = Content.Load<Texture2D>("bar1");// this was intended to be used as the background of bar2 but no longer
            //this.bar2 = Content.Load<Texture2D>("bar2");
            this.laserSprite = Content.Load<Texture2D>("laser");
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

        internal void drawHUD()
        {
            spriteBatch.Begin();
            //TODO: draw the HUD here
            //spriteBatch.DrawString(font, "Health", new Vector2(100, 700), Color.Yellow);  //this will ultimately be in the draw method in HUD
            //hud.Draw();
            Vector2 hudLocation = new Vector2(0, 630);
            spriteBatch.Draw(hudSprite, hudLocation, Color.White);

            spriteBatch.Draw(bar1, new Rectangle((int)hudLocation.X + 370, (int)hudLocation.Y + 30, 178 * game.s.health / 100, 38), getHPColor(game.s.health));

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
            drawLasers();
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

            // health bar
            //Rectangle rec2 = new Rectangle((int)s.Position.X - 80, (int)s.Position.Y - 120, 160 * s.health / 100, 20);
            Rectangle rec1 = new Rectangle((int)s.Position.X - 80, (int)s.Position.Y - 120, 160 * s.health / 100, 20);
            spriteBatch.Draw(bar1, rec1, getHPColor(s.health));

            //spriteBatch.Draw(bar2, rec2, Color.White);
        }

        private Color getHPColor(int hp)
        {
            if (hp > 50)
            {
                return Color.Green;
            }
            else if (hp > 20)
            {
                return Color.Yellow;
            }
            else
            {
                return Color.Red;
            }
        }

        private void drawNetAcceleration(Object o)
        {
            Vector2 location = o.Position;
            Rectangle sourceRectangle = new Rectangle(0, 0, arrow.Width, arrow.Height);
            Vector2 origin = new Vector2(arrow.Width / 2, arrow.Height); //rotate with respect to the bottom-middle point
            float size = Math.Min((float)o.Acceleration.Length() / 10, 30);
            spriteBatch.Draw(arrow, location, sourceRectangle, Color.White, o.AccAngle, origin, size, SpriteEffects.None, 1);
        }

        private void drawLasers()
        {
            foreach (Laser laser in Laser.lasers)
            {
                Vector2 location = laser.Position;
                Rectangle sourceRectangle = new Rectangle(0, 0, laserSprite.Width, laserSprite.Height);
                Vector2 origin = new Vector2(laserSprite.Width / 2, laserSprite.Height); //rotate with respect to the bottom-middle point
                spriteBatch.Draw(laserSprite, location, sourceRectangle, Color.White, laser.Angle, origin, 3f, SpriteEffects.None, 1);
            }
        }

        int menuAlpha = 1;
        int menuFadeIncrement = 6;
        double menuFadeDelay = .035;
        internal void setFadeInMenu()
        {
            menuAlpha = 1;
            menuFadeIncrement = 6;
        }
        internal void setFadeOutMenu()
        {
            menuFadeIncrement = -12;
        }
        internal void drawMainMenu(TimeSpan elapsedTime)
        {
            //if (menuAlpha < 0) { menuAlpha = 0; }
            //else if (menuAlpha > 255) { menuAlpha = 255; }
            if (menuAlpha < 0)
            {
                return;
            }

            menuFadeDelay -= elapsedTime.TotalSeconds;
            if (menuFadeDelay <= 0)
            {
                menuFadeDelay = .035;
                menuAlpha += menuFadeIncrement;
            }
            spriteBatch.Begin();
            Color color = Color.White * (menuAlpha / 255f);
            spriteBatch.Draw(menuSprite, new Vector2(0, 0), color);
            spriteBatch.End();
        }

        int winAlpha = 1;
        int winFadeIncrement = 6;
        double winFadeDelay = .035;
        internal void setFadeInWin()
        {
            winAlpha = 1;
            winFadeIncrement = 6;
        }
        internal void setFadeOutWin()
        {
            winFadeIncrement = -12;
        }
        internal void drawGameWin(TimeSpan elapsedTime)
        {
            if (winAlpha < 0)
            {
                return;
            }

            winFadeDelay -= elapsedTime.TotalSeconds;
            if (winFadeDelay <= 0)
            {
                winFadeDelay = .035;
                winAlpha += winFadeIncrement;
            }
            spriteBatch.Begin();
            Color color = Color.White * (winAlpha / 255f);
            spriteBatch.Draw(winSprite, new Vector2(0, 0), color);
            spriteBatch.End();
        }

        internal void drawTitleScreen()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(titleSprite, new Vector2(0, 0));
            spriteBatch.End();
        }


        int lineAlpha = 1;
        int lineFadeIncrement = 12;
        double lineFadeDelay = .035;
        internal void drawFinishLine(TimeSpan elapsedTime, int linePosition)
        {

            lineFadeDelay -= elapsedTime.TotalSeconds;
            if (lineFadeDelay <= 0)
            {
                lineFadeDelay = .035;
                lineAlpha += lineFadeIncrement;
                if (lineAlpha < 0 || lineAlpha > 255) { lineFadeIncrement *= -1; }
            }

            var screenScale = game.GetScreenScale();
            var viewMatrix = game.Camera.GetTransform();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied,
                                           null, null, null, null, viewMatrix * Matrix.CreateScale(screenScale));
            Color color = Color.White * (lineAlpha / 255f);
            spriteBatch.Draw(finishLine, new Rectangle(linePosition - 150, -12000, 300, 24000), color);
            spriteBatch.End();
        }

    }
}
