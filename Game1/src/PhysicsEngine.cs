using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class PhysicsEngine
    {
        public List<Body> bodies;

        private PhysicsEngine()
        {

        }

        private static readonly PhysicsEngine instance = new PhysicsEngine();

        public static PhysicsEngine GetInstance()
        {
            return instance;
        }

        public void Init()
        {
            this.bodies = new List<Body>();
        }

        public void Add(Body newBody)
        {
            this.bodies.Add(newBody);
        }

        public void Update(float deltaTime)
        {
            List<Body> bodiesWithCollisionToCheck = this.bodies.FindAll(body => body.needsCollisionCheck);

            foreach (Body mainBody in bodiesWithCollisionToCheck)
            {
                foreach (Body otherBody in bodies)
                {
                    if (mainBody != otherBody)
                    {
                        if (AABB.IsColliding(mainBody, otherBody))
                        {
                            Direction collisionDirection = AABB.CollisionDirection(mainBody, otherBody);
                            mainBody.parent.Collide(collisionDirection, otherBody);
                        }
                    }
                }
            }
        }
    }
}
