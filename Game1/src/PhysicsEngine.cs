﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class PhysicsEngine
    {
        public List<GameObject> bodies;

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
            this.bodies = new List<GameObject>();
        }

        public void Add(GameObject newBody)
        {
            this.bodies.Add(newBody);
        }

        public void Update(float deltaTime)
        {
            ResolveCollision(deltaTime);
            foreach (GameObject go in bodies)
            {
                go.Update(deltaTime);
            }
        }

        private void ResolveCollision(float deltaTime)
        {
            List<GameObject> bodiesWithCollisionToCheck = this.bodies.FindAll(body => body.body.needsCollisionCheck);

            // TODO: If this become performances bottleneck
            // We should filter the list to calculacte collisions
            // from nearest objects only.
            foreach (GameObject mainBody in bodiesWithCollisionToCheck)
            {
                foreach (GameObject otherBody in bodies)
                {
                    if (mainBody != otherBody)
                    {
                        if (AABB.IsColliding(mainBody.body, otherBody.body))
                        {
                            Direction collisionDirection = AABB.CollisionDirection(mainBody.body, otherBody.body);
                            mainBody.body.parent.Collide(collisionDirection, otherBody);
                        }
                    }
                }
            }
        }
    }
}
