using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class PhysicsEngine
    {
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

        }

        public void Update(List<GameObject> gameObjects, GameTime time, float deltaTime)
        {
            foreach (GameObject go in gameObjects)
            {
                go.Update(time, deltaTime);
            }
            ResolveCollision(gameObjects, deltaTime);
            foreach (GameObject go in gameObjects)
            {
                go.SynchWithBody();
            }
        }

        private void ResolveCollision(List<GameObject> gameObjects, float deltaTime)
        {
            List<GameObject> gameObjectWithCOllisionToCheck = gameObjects.FindAll(go => go.body.needsCollisionCheck);

            // TODO: If this become performances bottleneck
            // We should filter the list to calculacte collisions
            // from nearest objects only.
            foreach (GameObject mainGo in gameObjectWithCOllisionToCheck)
            {
                foreach (GameObject otherGo in gameObjects)
                {
                    
                    if (mainGo != otherGo)
                    {
                        if (AABB.IsColliding(mainGo.body, otherGo.body))
                        {
                            Direction collisionDirection = AABB.CollisionDirection(mainGo.body, otherGo.body);
                             
                            mainGo.Collide(collisionDirection, otherGo);
                        }
                    }
                }
            }
        }
    }
}
