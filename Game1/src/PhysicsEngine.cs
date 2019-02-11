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

        public void Update(List<GameObject> gameObjects, float deltaTime)
        {
            
            foreach (GameObject go in gameObjects)
            {
                go.Update(deltaTime);
            }
            ResolveCollision(gameObjects, deltaTime);
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

                            switch (collisionDirection)
                            {
                                case Direction.Bottom:
                                    if (mainGo.body.speedY > 0)
                                    {
                                        mainGo.body.ResetSpeedY();
                                        mainGo.body.SetY(otherGo.body.y - mainGo.body.height);
                                    }
                                    break;
                                case Direction.Left:
                                    if (mainGo.body.speedX > 0)
                                    {
                                        mainGo.body.ResetSpeedX();
                                        mainGo.body.SetX(otherGo.body.x - mainGo.body.width);
                                    }
                                    break;
                                case Direction.Right:
                                    if (mainGo.body.speedX < 0)
                                    {
                                        mainGo.body.ResetSpeedX();
                                        mainGo.body.SetX(otherGo.body.x + otherGo.body.width);
                                    }
                                    break;
                                case Direction.Top:
                                    if (mainGo.body.speedY < 0)
                                    {
                                        mainGo.body.ResetSpeedY();
                                        mainGo.body.SetY(otherGo.body.y + otherGo.body.height);
                                    }
                                    break;
                            }
                             
                            mainGo.body.parent.Collide(collisionDirection, otherGo);
                        }
                    }
                }
            }
        }
    }
}
