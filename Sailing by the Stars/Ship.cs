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
        public int health;

        public Ship(float m = 100, float r = 100, Vector2? pos = null, Vector2? vel = null, int health = 100)
            : base(m, r, pos, vel)
        {
            this.health = health;
            this.justDie = false;
        }

        public override string ToString()
        {
            return "1," + base.ToString();
        }

        protected override bool DecreaseHealth(int damage)
        {
            this.health -= damage;
            if (this.health <= 0 && this.health + damage > 0)
            {
                justDie = true;
            }
            else
            {
                justDie = false;
            }
            return this.health > 0;
        }

        private bool justDie;
        public override bool IsDead()
        {
            return this.health <= 0;
        }
        public override bool explode()
        {
            if (justDie == true)
            {
                justDie = false;
                return true;
            }
            return false;
        }

        public void setInfinite()
        {
            this.health = int.MaxValue;
        }

    }

}
