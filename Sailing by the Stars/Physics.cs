using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sailing_by_the_Stars
{
    class Physics
    {
        private Object[] objects;
        private Audio audio;
        public Physics(Object[] allGravObjects, Audio audio)
        {
            this.objects = allGravObjects;
            this.audio = audio;
        }

        internal void update(TimeSpan deltaTime)
        {
            // update accelerations
            updateAcceleration();

            // move
            foreach (Object o in objects)
            {
                if (o.IsDead())
                {
                    continue;
                }
                o.Move(deltaTime);
            }

            Laser.Update(deltaTime);
        }

        private const int closeDistance = 300;

        internal void updateAcceleration()
        {

            foreach (Object o1 in objects)
            {
                if (o1.IsDead()) // esp. the enemy ship is gone
                {
                    continue;
                }

                if ((o1).CheckHitByLaser())
                {
                    audio.playExplosionFX();
                }

                Vector2 netAcceleration = Vector2.Zero;
                foreach (Object o2 in objects)
                {
                    if (o2.IsDead()) // the enemy ship is gone
                    {
                        continue;
                    }
                    if (o1 == o2) // same obj
                    {
                        continue;
                    }

                    // gravity force
                    Vector2 rVector = Vector2.Subtract(o2.Position, o1.Position);
                    float r = rVector.Length();
                    Vector2 rNormalized = rVector / r;
                    double rSquared = r * r;

                    if (r < 25600) //  close to each other
                    {
                        Vector2 acc = 100000F * o2.Mass * rNormalized / (float)rSquared;
                        netAcceleration += acc;
                        if (Object.CheckCollision(o1, o2))
                        {
                            audio.playCollideFX();
                        }

                        // AI for enemy ship
                        if (o1 is EnemyShip)
                        {
                            float surfaceDistance = r - o1.Radius - o2.Radius;
                            float diff = closeDistance - surfaceDistance;

                            // keep away from planets
                            if (diff > 0)
                            {
                                ((EnemyShip)o1).creatThrust(diff / closeDistance * (-rVector));
                            }

                            // go to the nearest planet
                            if (o2 is Planet && rSquared < ((EnemyShip)o1).distanceToNearestPlanetSq) // o2 is nearest planet
                            {
                                ((EnemyShip)o1).nearestPlanet = (Planet)o2;
                            }

                            // attack the player ship
                            else if (!(o2 is EnemyShip) && o2 is Ship && r < Laser.range) // o2 is player ship in range
                            {
                                if (((EnemyShip)o1).shootLaser(o2))
                                {
                                    audio.playLaserFX();
                                }
                            }


                        }// end if enemy ship

                    } //end if near


                }// end foreach o2

                o1.Acceleration = netAcceleration;
            }
        }
        internal static float distanceSq(Vector2 pos1, Vector2 pos2)
        {
            return (pos1.X - pos2.X) * (pos1.X - pos2.X) + (pos1.Y - pos2.Y) * (pos1.Y - pos2.Y);
        }

    }
}
