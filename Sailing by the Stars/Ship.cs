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
        private int health;
        public int Health
        {
            get
            {
                return Math.Min(100, health);
            }
            set
            {
                health = value;
            }
        }

        public Ship(float m = 100, float r = 100, Vector2? pos = null, Vector2? vel = null, int health = 100)
            : base(m, r, pos, vel)
        {
            this.Health = health;
            this.justDie = false;
        }

        public override string ToString()
        {
            return "1," + base.ToString() + "," + Health;
        }

        protected override bool DecreaseHealth(int damage)
        {
            this.Health -= damage;
            if (this.Health <= 0 && this.Health + damage > 0)
            {
                justDie = true;
            }
            else
            {
                justDie = false;
            }
            return this.Health > 0;
        }

        private bool justDie;
        public override bool IsDead()
        {
            return this.Health <= 0;
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
            this.Health = int.MaxValue;
        }

    }

}
