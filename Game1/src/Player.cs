using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace Game1
{
    class Player
    {
        public PhysicalObject physicalObject;
        public float x = 0;
        public float y = 0;
        public float width;
        public float height;
        public float jumpCount = 0;

        public const float maxJump = 1;

        public Player(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.physicalObject = new PhysicalObject(x, y, width, height);
        }

        public void Update(float deltaTime, KeyboardState state, List<PhysicalObject> obstacles)
        {
            this.UpdateSpeedFromKeyboardState(state);
            this.physicalObject.body.CalculateSpeed();
            HandleCollision(deltaTime, obstacles);
            this.physicalObject.Update(deltaTime);
            this.x = this.physicalObject.x;
            this.y = this.physicalObject.y;
        }

        public void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            this.physicalObject.Draw(gameTime, graphicsDevice, spriteBatch);
        }

        public void ResetJump()
        {
            this.jumpCount = 0;
        }

        public void Log()
        {
            Debug.WriteLine("-------- DEBUG PLAYER -----------");
            Debug.WriteLine("Player pos: " + this.x + " - " + this.y);
            Debug.WriteLine("PO pos:" + this.physicalObject.x + " - " + this.physicalObject.y);
            Debug.WriteLine("Body pos: " + this.physicalObject.body.x + " - " + this.physicalObject.body.y);
            Debug.WriteLine("----------- END PLAYER -----------");
        }

        private void UpdateSpeedFromKeyboardState(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.Q))
                MoveLeft();
            if (state.IsKeyDown(Keys.D))
                MoveRight();
            if (state.IsKeyDown(Keys.Space))
                Jump();
        }

        private void MoveLeft()
        {
            this.physicalObject.MoveLeft();
        }

        private void MoveRight()
        {
            this.physicalObject.MoveRight();
        }

        private void Jump()
        {
            if (this.jumpCount < maxJump)
            {
                this.physicalObject.Jump();
                this.jumpCount++;
            }
        }

        private void HandleCollision(float deltaTime, List<PhysicalObject> obstacles)
        {
            Vector2 futurPosition = this.physicalObject.FuturPosition(deltaTime);

            // TODO: +50 ???
            Body futurBody = new Body(futurPosition.X, futurPosition.Y + 50, this.width, this.width);

            obstacles.ForEach(delegate (PhysicalObject obstacle)
            {
                if (AABB.IsColliding(futurBody, obstacle.body))
                {
                    Direction collisionDirection = AABB.CollisionDirection(futurBody, obstacle.body);

                    switch (collisionDirection)
                    {
                        case Direction.Bottom:
                            this.ResetJump();
                            if (this.physicalObject.body.speedY > 0)
                            {
                                this.physicalObject.body.ResetSpeedY();
                                this.physicalObject.body.SetY(obstacle.y);
                            }
                            break;

                    }
                }
            });
        }
    }
}
