using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sailing_by_the_Stars
{
    class HUD
    {
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private GraphicsDevice graphicsDevice;

        private Vector2 position;

        private String textLabel;
        private String textValue;
        private Color textColor;

        private bool enabled;

        ContentManager Content;
        

        int health;
        Vector2 direction;
        int density;
        int message;



        public HUD()
        {
            health = 100;
            direction = new Vector2(400, 0);
            density = 50;
            
        }

        public void communications(int m)
        {
            message = m;
            

        }

        public void loadContent(ContentManager c)
        {
            Content = c;
        }

        public void setHealth(int h)
        {
            health = h; 
        }

        public void setDirection(Vector2 dir)
        {
            direction = dir;
        }

        public void setDensity(int dens)
        {
            density = dens;
        }

        public void TextComponent(String textLabel, Vector2 position, SpriteBatch spriteBatch, SpriteFont spriteFont, GraphicsDevice graphicsDevice)
        {
        this.textLabel = textLabel.ToUpper();
        this.position = position;
 
        this.spriteBatch = spriteBatch;
        this.spriteFont = spriteFont;
        this.graphicsDevice = graphicsDevice;
        }


        public void Update()
        {

        }
        public void Draw()
        {
            /*
            Content.RootDirectory = "Content";
            Content.Load<Texture2D>("hud");
            */
            spriteFont = Content.Load<SpriteFont>("Font");
            Color myTransparentColor = new Color(0, 0, 0, 127);
            /*
            Vector2 stringDimensions = spriteFont.MeasureString(textLabel + ": " + textValue);
            float width = stringDimensions.X;
            float height = stringDimensions.Y;
            */
            Rectangle backgroundRectangle = new Rectangle();
            //backgroundRectangle.Width = (int)width + 10;
            //backgroundRectangle.Height = (int)height + 10;
            //backgroundRectangle.X = (int)position.X - 5;
            //backgroundRectangle.Y = (int)position.Y - 5;

            backgroundRectangle.Width = 50;
            backgroundRectangle.Height = 30;
            backgroundRectangle.X = 20;
            backgroundRectangle.Y = 600;

            //Texture2D dummyTexture = new Texture2D(graphicsDevice, 1, 1);
           //dummyTexture.SetData(new Color[] { myTransparentColor });

            //spriteBatch.Draw(dummyTexture, backgroundRectangle, myTransparentColor);
            spriteBatch.DrawString(spriteFont, "hi there" + ": " + "blah", position, textColor);

        }
    }
}
