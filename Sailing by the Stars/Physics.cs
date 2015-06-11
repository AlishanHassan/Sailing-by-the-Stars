using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Sailing_by_the_Stars
{
    class Physics
    {
        private Object[] objects;
        public Physics(Object[] allGravObjects)
        {
            this.objects = allGravObjects;
        }

        internal void update(TimeSpan deltaTime)
        {
            // update accelerations
            updateAcceleration();

            // move
            foreach (Object o in objects)
            {
                o.Move(deltaTime);
            }

            Laser.Update(deltaTime);
        }

        private const int closeDistance = 300;

        internal void updateAcceleration()
        {
            foreach (Object o1 in objects)
            {
                Vector2 netAcceleration = Vector2.Zero;
                foreach (Object o2 in objects)
                {

                    if (o1 != o2)
                    {
                        Vector2 rVector = Vector2.Subtract(o2.Position, o1.Position);
                        float r = rVector.Length();
                        Vector2 rNormalized = rVector / r;
                        double rSquared = r * r;

                        if (r < 25600) //  close to each other
                        {
                            Vector2 acc = 100000F * o2.Mass * rNormalized / (float)rSquared;
                            netAcceleration += acc;
                            Object.CheckCollision(o1, o2);

                            if (o1 is EnemyShip)
                            {
                                float surfaceDistance = r - o1.Radius - o2.Radius;
                                float diff = closeDistance - surfaceDistance;
                                if (diff > 0)
                                {
                                    ((EnemyShip)o1).creatThrust(diff / closeDistance * (-rVector));
                                }

                                if (o2 is Planet && rSquared < ((EnemyShip)o1).distanceToNearestPlanetSq)
                                {
                                    ((EnemyShip)o1).nearestPlanet = (Planet)o2;
                                }
                                else if (
                                    !(o2 is EnemyShip) &&
                                    o2 is Ship
                                    && r < Laser.range
                                    )
                                {
                                    ((EnemyShip)o1).shootLaser(o2);
                                }


                            }// end if enemy ship

                        } //end if near


                    }
                }// end foreach o2

                o1.Acceleration = netAcceleration;
            }
        }

    }
}
