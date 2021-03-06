﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sailing_by_the_Stars
{
    class EnemyShip : Ship
    {
        private Vector2 thrust;
        public Planet nearestPlanet;
        public float distanceToNearestPlanetSq
        {
            get
            {
                if (nearestPlanet == null)
                {
                    return float.PositiveInfinity;
                }
                return (this.Position - nearestPlanet.Position).LengthSquared();
            }
        }

        public EnemyShip(float m = 100, float r = 100, Vector2? pos = null, Vector2? vel = null, int health = 100, float laserCoolDown = 0)
            : base(m, r, pos, vel, health)
        {
            this.thrust = Vector2.Zero;
            this.laserCoolDown = laserCoolDown;
        }

        internal override void Move(TimeSpan deltaTime)
        {
            // apply thrust
            this.Acceleration += thrust;
            thrust = Vector2.Zero;

            // come back if strayed
            if (nearestPlanet != null)
            {
                this.Acceleration += (nearestPlanet.Position - this.Position) / 256;
            }

            // laser cool down
            if (laserCoolDown > 0)
            {
                laserCoolDown -= (float)deltaTime.TotalSeconds;
            }

            base.Move(deltaTime);
        }

        public void creatThrust(Vector2 t)
        {
            // at one moment the ship can be close to multiple planets
            // so this method might be used more than once in a single update()
            // so use += instead of =
            thrust += t;
        }

        public override string ToString()
        {
            return "-" + base.ToString() + "," + laserCoolDown;
        }

        private static float laserGunCoolDownTime = 3;
        private float laserCoolDown = 0;
        internal bool shootLaser(Object o2)
        {
            if (this.laserCoolDown > 0) // still cooling down
            {
                return false;
            }
            laserCoolDown = laserGunCoolDownTime;
            Laser laser = new Laser(this.Position, o2.Position);
            Laser.lasers.Add(laser);
            return true;
        }
    }
}
