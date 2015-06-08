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

        internal void updateAcceleration()
        {
            foreach (Object o1 in objects)
            {
                Vector2 netAcceleration = Vector2.Zero;
                foreach (Object o2 in objects)
                {
                    if (o1 != o2)
                    {
                        Vector2 r = Vector2.Subtract(o2.Position, o1.Position);
                        double rSquared = r.LengthSquared();
                        if (rSquared < 500000000) //  close to each other
                        {
                            netAcceleration += 100000F * o2.Mass * Vector2.Normalize(r) / (float)rSquared;
                        }
                        Object.CheckCollision(o1, o2);
                    }
                }
                o1.Acceleration = netAcceleration;
            }
        }


        //internal void update(TimeSpan deltaTime)
        //{
        //    // a second version for calculation the acceleration - using center of mass
        //    // although the runtime for acceleration calculation is only 2n, the collision detection still takes n^2.
        //    Vector2 centerOfMass = new Vector2(0, 0);
        //    float totalMass = 0;
        //    foreach (Object obj in allGravObjects)
        //    {
        //        centerOfMass += obj.Mass * obj.Position;
        //        totalMass += obj.Mass;
        //    }
        //    centerOfMass /= totalMass;

        //    foreach (Object obj in allGravObjects)
        //    {
        //        obj.Update(centerOfMass, totalMass, deltaTime);
        //    }

        //    for (int i = 0; i < allGravObjects.Length; i++)
        //    {
        //        var o1 = allGravObjects[i];
        //        for (int j = i + 1; j < allGravObjects.Length; j++)
        //        {
        //            var o2 = allGravObjects[j];
        //            Object.CheckCollision(o1, o2, deltaTime);
        //        }
        //    }
        //}


    }
}
