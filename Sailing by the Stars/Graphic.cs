﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Sailing_by_the_Stars
{
    class Graphic
    {
        public SpriteBatch spriteBatch;
        public Texture2D arrow;
        public Texture2D menuSprite;
        public Texture2D winSprite;
        public Texture2D lose1Sprite;
        public Texture2D lose2Sprite;
        public Texture2D titleSprite;
        public Texture2D storySprite;
        public Texture2D controlsSprite;
        public Texture2D[] planetSprites;
        public Texture2D[] shipSprites;
        public Texture2D finishLine;
        public Texture2D hudSprite;
        public Texture2D healthBar;
        public Texture2D bar1;
        public Texture2D laserSprite;
        public Texture2D hudDirNeedle;
        public Texture2D hudVelNeedle;
        public Texture2D message1;
        public Texture2D message2;
        public Texture2D message3;
        public Texture2D message4;
        public Texture2D message5;
        public Texture2D message6;
        public Texture2D message7;
        public Texture2D message8;
        public Texture2D message9;
        public Texture2D message10;
        public Texture2D message11;
        public Texture2D message12;
        private Microsoft.Xna.Framework.Content.ContentManager Content;
        private MainGame game;

        public Graphic(MainGame game)
        {
            this.game = game;
            this.Content = game.Content;
            this.spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this.menuSprite = Content.Load<Texture2D>("Texture/Screen/mainmenu");
            this.winSprite = Content.Load<Texture2D>("Texture/Screen/win");
            this.lose1Sprite = Content.Load<Texture2D>("Texture/Screen/loseNoHP");
            this.lose2Sprite = Content.Load<Texture2D>("Texture/Screen/loseDeepSpace");
            this.titleSprite = Content.Load<Texture2D>("Texture/Screen/titlescreen");
            this.controlsSprite = Content.Load<Texture2D>("Texture/Screen/controlsScreen");
            this.storySprite = Content.Load<Texture2D>("Texture/Screen/storyScreen");
            this.arrow = Content.Load<Texture2D>("Texture/Other/arrow");
            this.planetSprites = new Texture2D[15];
            this.shipSprites = new Texture2D[2];
            this.finishLine = Content.Load<Texture2D>("Texture/Other/finishline");
            this.hudSprite = Content.Load<Texture2D>("Texture/Other/hud");
            this.healthBar = Content.Load<Texture2D>("Texture/Other/healthbar");
            this.bar1 = Content.Load<Texture2D>("Texture/Other/bar1");
            this.laserSprite = Content.Load<Texture2D>("Texture/Other/laser");
            this.hudDirNeedle = Content.Load<Texture2D>("Texture/Other/hudDirNeedle");
            this.hudVelNeedle = Content.Load<Texture2D>("Texture/Other/hudVelNeedle");
            this.message1 = Content.Load<Texture2D>("Texture/Other/message1");
            this.message2 = Content.Load<Texture2D>("Texture/Other/message2");
            this.message3 = Content.Load<Texture2D>("Texture/Other/message3");
            this.message4 = Content.Load<Texture2D>("Texture/Other/message4");
            this.message5 = Content.Load<Texture2D>("Texture/Other/message5");
            this.message6 = Content.Load<Texture2D>("Texture/Other/message6");
            this.message7 = Content.Load<Texture2D>("Texture/Other/message7");
            this.message8 = Content.Load<Texture2D>("Texture/Other/message8");
            this.message9 = Content.Load<Texture2D>("Texture/Other/message9");
            this.message10 = Content.Load<Texture2D>("Texture/Other/message10");
            this.message11 = Content.Load<Texture2D>("Texture/Other/message11");
            this.message12 = Content.Load<Texture2D>("Texture/Other/message12");

        }

        internal void loadSprites(Object[] allGravObjects)
        {
            foreach (Object o in allGravObjects)
            {
                if (o is Planet)
                {
                    if (planetSprites[o.Id] == null)
                    {
                        planetSprites[o.Id] = Content.Load<Texture2D>("Texture/Planet/planet-" + o.Id);
                    }
                }
                else if (o is Ship)
                {
                    if (shipSprites[o.Id] == null)
                    {
                        shipSprites[o.Id] = Content.Load<Texture2D>("Texture/Ship/ship-" + o.Id);
                    }
                }
            }
        }

        internal void drawHUD(Ship s)
        {
            spriteBatch.Begin();
            //TODO: draw the HUD here
            //spriteBatch.DrawString(font, "Health", new Vector2(100, 700), Color.Yellow);  //this will ultimately be in the draw method in HUD
            //hud.Draw();
            Vector2 hudLocation = new Vector2(0, 630);
            spriteBatch.Draw(hudSprite, hudLocation, Color.White);
            spriteBatch.Draw(healthBar, new Rectangle((int)hudLocation.X + 1050, (int)hudLocation.Y + 36, 178, 38), Color.White);
            spriteBatch.Draw(healthBar, new Rectangle((int)hudLocation.X + 1050, (int)hudLocation.Y + 36, 178 * game.s.health / 100, 38), getHPColor(game.s.health));


            //needles

            Vector2 dirNeedleLocation = new Vector2((int)hudLocation.X + 950, (int)hudLocation.Y + 49);
            Vector2 velNeedleLocation = new Vector2((int)hudLocation.X + 820, (int)hudLocation.Y + 49);

            Vector2 dirOrigin = new Vector2(hudDirNeedle.Width / 2, hudDirNeedle.Height); //rotate with respect to lower point
            Vector2 velOrigin = new Vector2(hudVelNeedle.Width / 2, hudVelNeedle.Height); //rotate with respect to lower point

            Rectangle dirRectangle = new Rectangle(0, 0, hudDirNeedle.Width, hudDirNeedle.Height);
            Rectangle velRectangle = new Rectangle(0, 0, hudVelNeedle.Width, hudVelNeedle.Height);



            float velocity = s.Velocity.Length();
            float velGaugeAngle = velocity / 333 - 3; //this doesn't have a limit, but that would just add a cool effect when the ship goes wild


            spriteBatch.Draw(hudDirNeedle, dirNeedleLocation, dirRectangle, Color.White, s.VelAngle, dirOrigin, 1, SpriteEffects.None, 1);
            spriteBatch.Draw(hudVelNeedle, velNeedleLocation, velRectangle, Color.White, velGaugeAngle, velOrigin, 1, SpriteEffects.None, 1);

            //spriteBatch.Draw(laserSprite, location, sourceRectangle, laser.Color, laser.Angle, origin, 3f, SpriteEffects.None, 1);
            //spriteBatch.Draw(hudVelNeedle, (int)hudLocation.X + 80, (int)hudLocation.Y + 80, velRectangle, Color.White, s.VelAngle, velOrigin, 1, SpriteEffects.None, 1);


            //communications bar
            drawCommunicationsWindow(s, hudLocation);



            //Debug.WriteLine(s.Position.X);
            //spriteBatch.Draw(message1, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
            spriteBatch.End();
        }

        internal void drawCommunicationsWindow(Ship s, Vector2 hudPos)
        {

            Vector2 hudLocation = hudPos;
            float check = s.Position.X;

            if (0 < check && check < 3000)
            {
                int messageCount = game.randomMessages[0];                
                switch(messageCount)
                {
                    case 1:
                        spriteBatch.Draw(message1, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 2:
                        spriteBatch.Draw(message2, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 3:
                        spriteBatch.Draw(message3, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 4:
                        spriteBatch.Draw(message4, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 5:
                        spriteBatch.Draw(message5, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 6:
                        spriteBatch.Draw(message6, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 7:
                        spriteBatch.Draw(message7, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 8:
                        spriteBatch.Draw(message8, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 9:
                        spriteBatch.Draw(message9, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 10:
                        spriteBatch.Draw(message10, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 11:
                        spriteBatch.Draw(message11, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 12:
                        spriteBatch.Draw(message12, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    default:
                        break;
                }

            }
            else if (3000 < check && check < 6000)
            {
                int messageCount = game.randomMessages[1];
                switch (messageCount)
                {
                    case 1:
                        spriteBatch.Draw(message1, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 2:
                        spriteBatch.Draw(message2, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 3:
                        spriteBatch.Draw(message3, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 4:
                        spriteBatch.Draw(message4, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 5:
                        spriteBatch.Draw(message5, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 6:
                        spriteBatch.Draw(message6, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 7:
                        spriteBatch.Draw(message7, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 8:
                        spriteBatch.Draw(message8, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 9:
                        spriteBatch.Draw(message9, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 10:
                        spriteBatch.Draw(message10, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 11:
                        spriteBatch.Draw(message11, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 12:
                        spriteBatch.Draw(message12, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    default:
                        break;
                }
            }
            else if (6000 < check && check < 9000)
            {
                int messageCount = game.randomMessages[2];
                switch (messageCount)
                {
                    case 1:
                        spriteBatch.Draw(message1, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 2:
                        spriteBatch.Draw(message2, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 3:
                        spriteBatch.Draw(message3, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 4:
                        spriteBatch.Draw(message4, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 5:
                        spriteBatch.Draw(message5, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 6:
                        spriteBatch.Draw(message6, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 7:
                        spriteBatch.Draw(message7, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 8:
                        spriteBatch.Draw(message8, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 9:
                        spriteBatch.Draw(message9, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 10:
                        spriteBatch.Draw(message10, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 11:
                        spriteBatch.Draw(message11, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 12:
                        spriteBatch.Draw(message12, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    default:
                        break;
                }
            }
            else if (9000 < check && check < 12000)
            {
                int messageCount = game.randomMessages[3];
                switch (messageCount)
                {
                    case 1:
                        spriteBatch.Draw(message1, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 2:
                        spriteBatch.Draw(message2, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 3:
                        spriteBatch.Draw(message3, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 4:
                        spriteBatch.Draw(message4, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 5:
                        spriteBatch.Draw(message5, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 6:
                        spriteBatch.Draw(message6, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 7:
                        spriteBatch.Draw(message7, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 8:
                        spriteBatch.Draw(message8, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 9:
                        spriteBatch.Draw(message9, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 10:
                        spriteBatch.Draw(message10, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 11:
                        spriteBatch.Draw(message11, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 12:
                        spriteBatch.Draw(message12, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    default:
                        break;
                }
            }
            else if (12000 < check && check < 15000)
            {
                int messageCount = game.randomMessages[4];
                switch (messageCount)
                {
                    case 1:
                        spriteBatch.Draw(message1, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 2:
                        spriteBatch.Draw(message2, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 3:
                        spriteBatch.Draw(message3, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 4:
                        spriteBatch.Draw(message4, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 5:
                        spriteBatch.Draw(message5, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 6:
                        spriteBatch.Draw(message6, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 7:
                        spriteBatch.Draw(message7, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 8:
                        spriteBatch.Draw(message8, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 9:
                        spriteBatch.Draw(message9, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 10:
                        spriteBatch.Draw(message10, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 11:
                        spriteBatch.Draw(message11, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 12:
                        spriteBatch.Draw(message12, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    default:
                        break;
                }
            }
            else if (15000 < check && check < 18000)
            {
                int messageCount = game.randomMessages[5];
                switch (messageCount)
                {
                    case 1:
                        spriteBatch.Draw(message1, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 2:
                        spriteBatch.Draw(message2, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 3:
                        spriteBatch.Draw(message3, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 4:
                        spriteBatch.Draw(message4, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 5:
                        spriteBatch.Draw(message5, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 6:
                        spriteBatch.Draw(message6, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 7:
                        spriteBatch.Draw(message7, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 8:
                        spriteBatch.Draw(message8, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 9:
                        spriteBatch.Draw(message9, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 10:
                        spriteBatch.Draw(message10, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 11:
                        spriteBatch.Draw(message11, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 12:
                        spriteBatch.Draw(message12, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    default:
                        break;
                }
            }
            else if (18000 < check && check < 21000)
            {
                int messageCount = game.randomMessages[6];
                switch (messageCount)
                {
                    case 1:
                        spriteBatch.Draw(message1, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 2:
                        spriteBatch.Draw(message2, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 3:
                        spriteBatch.Draw(message3, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 4:
                        spriteBatch.Draw(message4, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 5:
                        spriteBatch.Draw(message5, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 6:
                        spriteBatch.Draw(message6, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 7:
                        spriteBatch.Draw(message7, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 8:
                        spriteBatch.Draw(message8, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 9:
                        spriteBatch.Draw(message9, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 10:
                        spriteBatch.Draw(message10, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 11:
                        spriteBatch.Draw(message11, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 12:
                        spriteBatch.Draw(message12, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    default:
                        break;
                }
            }
            else if (21000 < check && check < 24000)
            {
                int messageCount = game.randomMessages[7];
                switch (messageCount)
                {
                    case 1:
                        spriteBatch.Draw(message1, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 2:
                        spriteBatch.Draw(message2, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 3:
                        spriteBatch.Draw(message3, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 4:
                        spriteBatch.Draw(message4, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 5:
                        spriteBatch.Draw(message5, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 6:
                        spriteBatch.Draw(message6, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 7:
                        spriteBatch.Draw(message7, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 8:
                        spriteBatch.Draw(message8, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 9:
                        spriteBatch.Draw(message9, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 10:
                        spriteBatch.Draw(message10, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 11:
                        spriteBatch.Draw(message11, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 12:
                        spriteBatch.Draw(message12, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    default:
                        break;
                }
            }
            else if (24000 < check && check < 27000)
            {
                int messageCount = game.randomMessages[8];
                switch (messageCount)
                {
                    case 1:
                        spriteBatch.Draw(message1, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 2:
                        spriteBatch.Draw(message2, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 3:
                        spriteBatch.Draw(message3, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 4:
                        spriteBatch.Draw(message4, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 5:
                        spriteBatch.Draw(message5, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 6:
                        spriteBatch.Draw(message6, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 7:
                        spriteBatch.Draw(message7, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 8:
                        spriteBatch.Draw(message8, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 9:
                        spriteBatch.Draw(message9, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 10:
                        spriteBatch.Draw(message10, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 11:
                        spriteBatch.Draw(message11, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 12:
                        spriteBatch.Draw(message12, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    default:
                        break;
                }
            }
            else if (27000 < check && check < 30000)
            {
                int messageCount = game.randomMessages[9];
                switch (messageCount)
                {
                    case 1:
                        spriteBatch.Draw(message1, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 2:
                        spriteBatch.Draw(message2, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 3:
                        spriteBatch.Draw(message3, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 4:
                        spriteBatch.Draw(message4, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 5:
                        spriteBatch.Draw(message5, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 6:
                        spriteBatch.Draw(message6, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 7:
                        spriteBatch.Draw(message7, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 8:
                        spriteBatch.Draw(message8, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 9:
                        spriteBatch.Draw(message9, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 10:
                        spriteBatch.Draw(message10, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 11:
                        spriteBatch.Draw(message11, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 12:
                        spriteBatch.Draw(message12, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    default:
                        break;
                }
            }
            else if (30000 < check && check < 33000)
            {
                int messageCount = game.randomMessages[10];
                switch (messageCount)
                {
                    case 1:
                        spriteBatch.Draw(message1, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 2:
                        spriteBatch.Draw(message2, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 3:
                        spriteBatch.Draw(message3, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 4:
                        spriteBatch.Draw(message4, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 5:
                        spriteBatch.Draw(message5, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 6:
                        spriteBatch.Draw(message6, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 7:
                        spriteBatch.Draw(message7, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 8:
                        spriteBatch.Draw(message8, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 9:
                        spriteBatch.Draw(message9, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 10:
                        spriteBatch.Draw(message10, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 11:
                        spriteBatch.Draw(message11, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 12:
                        spriteBatch.Draw(message12, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    default:
                        break;
                }
            }
            else if (33000 < check && check < 36000)
            {
                int messageCount = game.randomMessages[11];
                switch (messageCount)
                {
                    case 1:
                        spriteBatch.Draw(message1, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 2:
                        spriteBatch.Draw(message2, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 3:
                        spriteBatch.Draw(message3, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 4:
                        spriteBatch.Draw(message4, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 5:
                        spriteBatch.Draw(message5, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 6:
                        spriteBatch.Draw(message6, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 7:
                        spriteBatch.Draw(message7, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 8:
                        spriteBatch.Draw(message8, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 9:
                        spriteBatch.Draw(message9, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 10:
                        spriteBatch.Draw(message10, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 11:
                        spriteBatch.Draw(message11, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 12:
                        spriteBatch.Draw(message12, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    default:
                        break;
                }
            }
            else if (36000 < check && check < 39000)
            {
                int messageCount = game.randomMessages[12];
                switch (messageCount)
                {
                    case 1:
                        spriteBatch.Draw(message1, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 2:
                        spriteBatch.Draw(message2, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 3:
                        spriteBatch.Draw(message3, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 4:
                        spriteBatch.Draw(message4, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 5:
                        spriteBatch.Draw(message5, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 6:
                        spriteBatch.Draw(message6, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 7:
                        spriteBatch.Draw(message7, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 8:
                        spriteBatch.Draw(message8, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 9:
                        spriteBatch.Draw(message9, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 10:
                        spriteBatch.Draw(message10, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 11:
                        spriteBatch.Draw(message11, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 12:
                        spriteBatch.Draw(message12, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    default:
                        break;
                }
            }
            else if (39000 < check && check < 42000)
            {
                int messageCount = game.randomMessages[13];
                switch (messageCount)
                {
                    case 1:
                        spriteBatch.Draw(message1, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 2:
                        spriteBatch.Draw(message2, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 3:
                        spriteBatch.Draw(message3, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 4:
                        spriteBatch.Draw(message4, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 5:
                        spriteBatch.Draw(message5, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 6:
                        spriteBatch.Draw(message6, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 7:
                        spriteBatch.Draw(message7, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 8:
                        spriteBatch.Draw(message8, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 9:
                        spriteBatch.Draw(message9, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 10:
                        spriteBatch.Draw(message10, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 11:
                        spriteBatch.Draw(message11, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    case 12:
                        spriteBatch.Draw(message12, new Rectangle((int)hudLocation.X + 41, (int)hudLocation.Y + 20, 510, 55), Color.White);
                        break;
                    default:
                        break;
                }
            }
        }

        internal void drawAllObj(Object[] allGravObjects)
        {
            var screenScale = game.GetScreenScale();
            var viewMatrix = game.Camera.GetTransform();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied,
                                           null, null, null, null, viewMatrix * Matrix.CreateScale(screenScale));
            foreach (Object o in allGravObjects)
            {
                if (o.IsDead())
                {
                    continue;
                }
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
            Rectangle rec1 = new Rectangle((int)s.Position.X - 80, (int)s.Position.Y - 120, 160 * s.health / 100, 20);
            spriteBatch.Draw(bar1, rec1, getHPColor(s.health));
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
                spriteBatch.Draw(laserSprite, location, sourceRectangle, laser.Color, laser.Angle, origin, Laser.laserScale, SpriteEffects.None, 1);
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

        internal void drawTitleScreen()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(titleSprite, new Vector2(0, 0));
            spriteBatch.End();
        }
        internal void drawControls()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(controlsSprite, new Vector2(0, 0));
            spriteBatch.End();
        }
        internal void drawStory()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(storySprite, new Vector2(0, 0));
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

        int lose1Alpha = 1;
        int lose1FadeIncrement = 6;
        double lose1FadeDelay = .035;
        internal void setFadeInLoseNoHP()
        {
            lose1Alpha = 1;
            lose1FadeIncrement = 6;
        }
        internal void setFadeOutLoseNoHP()
        {
            lose1FadeIncrement = -12;
        }
        internal void drawGameLoseNoHP(TimeSpan elapsedTime)
        {
            if (lose1Alpha < 0)
            {
                return;
            }

            lose1FadeDelay -= elapsedTime.TotalSeconds;
            if (lose1FadeDelay <= 0)
            {
                lose1FadeDelay = .035;
                lose1Alpha += lose1FadeIncrement;
            }
            spriteBatch.Begin();
            Color color = Color.White * (lose1Alpha / 255f);
            spriteBatch.Draw(lose1Sprite, new Vector2(0, 0), color);
            spriteBatch.End();
        }

        int lose2Alpha = 1;
        int lose2FadeIncrement = 6;
        double lose2FadeDelay = .035;
        internal void setFadeInLoseDeepSpace()
        {
            lose2Alpha = 1;
            lose2FadeIncrement = 6;
        }
        internal void setFadeOutLoseDeepSpace()
        {
            lose2FadeIncrement = -12;
        }
        internal void drawGameLoseDeepSpace(TimeSpan elapsedTime)
        {
            if (lose2Alpha < 0)
            {
                return;
            }

            lose2FadeDelay -= elapsedTime.TotalSeconds;
            if (lose2FadeDelay <= 0)
            {
                lose2FadeDelay = .035;
                lose2Alpha += lose2FadeIncrement;
            }
            spriteBatch.Begin();
            Color color = Color.White * (lose2Alpha / 255f);
            spriteBatch.Draw(lose2Sprite, new Vector2(0, 0), color);
            spriteBatch.End();
        }

    }
}
