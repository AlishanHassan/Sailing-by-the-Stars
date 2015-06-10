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
        }

        private const int useThrustDistance = 300;
        //private const int innerOrbitDist = 150;
        //private const int outerOrbitDist = 450;

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

                        if (rSquared < 500000000) //  close to each other
                        {
                            Vector2 acc = 100000F * o2.Mass * rNormalized / (float)rSquared;
                            netAcceleration += acc;
                            Object.CheckCollision(o1, o2);

                            if (o1 is EnemyShip && o2 is Planet)
                            {

                                float surfaceDistance = r - o1.Radius - o2.Radius;
                                float diff = useThrustDistance - surfaceDistance;
                                if (diff > 0)
                                {
                                    ((EnemyShip)o1).creatThrust(diff / useThrustDistance * (-rVector));
                                }

                                if (rSquared < ((EnemyShip)o1).distanceToNearestPlanetSq)
                                {
                                    ((EnemyShip)o1).nearestPlanet = (Planet)o2;
                                }

                                //if (diff > useThrustDistance / 2)
                                //{
                                //    ((EnemyShip)o1).creatThrust(diff * diff / useThrustDistance * (-rVector));
                                //}

                                //((EnemyShip)o1).creatThrust(diff / useThrustDistance * (-acc));

                                //float surfaceDistance = r - o1.Radius - o2.Radius;
                                //float t = 0;
                                //if (surfaceDistance < innerOrbitDist) //  too close
                                //{
                                //    t = (innerOrbitDist - surfaceDistance)/surfaceDistance;
                                //}
                                //else if (surfaceDistance > outerOrbitDist) //  too far
                                //{
                                //    t = (outerOrbitDist-surfaceDistance)/surfaceDistance;
                                //}
                                //((EnemyShip)o1).creatThrust(t * (-rVector));



                                //if (ratioDisToClosestPlanet > r / o2.Radius)
                                //{
                                //    ratioDisToClosestPlanet = r / o2.Radius;
                                //    closestPlanet = (Planet)o2;
                                //} //end if

                            }// end if

                        } //end if


                    }
                }// end foreach o2

                //if (o1 is EnemyShip)
                //{
                //    float diff = useThrustDistance - surfaceDistance;
                //    if (diff < -useThrustDistance / 2)
                //    {
                //        ((EnemyShip)o1).creatThrust(diff / useThrustDistance / 100 * (-rVector));
                //    }



                //}
                o1.Acceleration = netAcceleration;
            }
        }

    }
}
