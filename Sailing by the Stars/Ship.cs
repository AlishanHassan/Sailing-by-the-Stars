using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sailing_by_the_Stars
{
    class Ship : Object
    {

        public Ship(float m = 100, float r = 100, Vector2? pos = null, Vector2? vel = null)
            : base(m, r, pos, vel)
        {

        }

        internal override void draw(SpriteBatch spriteBatch)
        {
            Vector2 location = this.Position;
            Rectangle sourceRectangle = new Rectangle(0, 0, Sprite.Width, Sprite.Height);
            Vector2 origin = new Vector2(Sprite.Width / 2, Sprite.Height / 2); //rotate with respect to the center point

            spriteBatch.Draw(Sprite, location, sourceRectangle, Color.White, this.VelAngle, origin, 1, SpriteEffects.None, 1);

        }

        public override string ToString()
        {
            return "1," + base.ToString();
        }
    }

}
