﻿using Microsoft.Xna.Framework;
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

        public Ship(float m = 100, float r = 100, Vector2? pos = null, Vector2? vel = null)
            : base(m, r, pos, vel)
        {
            health = 100;
        }

        public override string ToString()
        {
            return "1," + base.ToString();
        }

        protected override bool DecreaseHealth(int damage)
        {
            this.health -= damage;
            return this.health > 0;
        }

        public override bool IsDead()
        {
            return this.health <= 0;
        }
        public void setInfinite()
        {
            this.health = int.MaxValue;
            Console.WriteLine("HI");
        }

    }

}
