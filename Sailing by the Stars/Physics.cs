using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sailing_by_the_Stars
{
    class Physics
    {
        private Object[] allGravObjects;
        public Physics(Object[] allGravObjects)
        {
            this.allGravObjects = allGravObjects;
        }
        internal void update(TimeSpan deltaTime)
        {
            // a second version for calculation the acceleration - using center of mass
            // altough the runtime for acceleration calculation is only 2n, the collision detection still takes n^2.
            Vector2 centerOfMass = new Vector2(0, 0);
            float totalMass = 0;
            foreach (Object obj in allGravObjects)
            {
                centerOfMass += obj.Mass * obj.Position;
                totalMass += obj.Mass;
            }
            centerOfMass /= totalMass;

            foreach (Object obj in allGravObjects)
            {
                obj.Update(centerOfMass, totalMass, deltaTime);
            }

            for (int i = 0; i < allGravObjects.Length; i++)
            {
                var o1 = allGravObjects[i];
                for (int j = i + 1; j < allGravObjects.Length; j++)
                {
                    var o2 = allGravObjects[j];
                    Object.CheckCollision(o1, o2, deltaTime);
                }
            }
        }
    }

}
