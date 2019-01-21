using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Game1
{
    class Player
    {
        public Body body;
        public float x = 0;
        public float y = 0;
        public float width;
        public float height;
        public float jumpCount = 0;
        public Texture2D texture;
        public const float maxJump = 1;

        public Player(float x, float y, float width, float height, Texture2D texture)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.texture = texture;
            this.body = new Body(x, y, width, height);
        }

        public void Update(float deltaTime, KeyboardState state, List<Body> obstacles)
        {
            this.UpdateSpeedFromKeyboardState(state);
            this.body.CalculateSpeed();
            HandleCollision(deltaTime, obstacles);
            this.body.Update(deltaTime);
            this.x = this.body.x;
            this.y = this.body.y;
        }

        public void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            this.body.Draw(gameTime, graphicsDevice, spriteBatch);
            spriteBatch.Draw(texture, new Vector2(this.x, this.y), Color.White);
        }

        public void ResetJump()
        {
            this.jumpCount = 0;
        }

        public void Log()
        {
            Debug.WriteLine("-------- DEBUG PLAYER -----------");
            Debug.WriteLine("Player pos: " + this.x + " - " + this.y);
            Debug.WriteLine("Body pos: " + this.body.x + " - " + this.body.y);
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
            this.body.MoveLeft();
        }

        private void MoveRight()
        {
            this.body.MoveRight();
        }

        private void Jump()
        {
            if (this.jumpCount < maxJump)
            {
                this.body.Jump();
                this.jumpCount++;
            }
        }

        private void HandleCollision(float deltaTime, List<Body> obstacles)
        {
            Vector2 futurPosition = this.body.FuturPosition(deltaTime);

            // TODO: +50 ???
            // This save bottom collision but break the top collision
            Body futurBody = new Body(futurPosition.X, futurPosition.Y + 50, this.width, this.width);
            futurBody = this.body;

            var query =
                from obstacle in obstacles
                where body.x - obstacle.x < 128 && body.x - obstacle.x > -128 &&
                    body.y - obstacle.y < 128 && body.y - obstacle.y > -128
                select obstacle;

            //query = obstacles;
            foreach (Body obstacle in query)
            {
                if (AABB.IsColliding(futurBody, obstacle))
                {
                    Direction collisionDirection = AABB.CollisionDirection(futurBody, obstacle);

                    switch (collisionDirection)
                    {
                        case Direction.Bottom:
                            this.ResetJump();
                            if (this.body.speedY > 0)
                            {
                                this.body.ResetSpeedY();
                                this.body.SetY(obstacle.y - this.height);
                            }
                            break;
                        case Direction.Left:
                            if (this.body.speedX > 0)
                            {
                                this.body.ResetSpeedX();
                                this.body.SetX(obstacle.x - this.width);
                            }
                            break;
                        case Direction.Right:
                            if (this.body.speedX < 0)
                            {
                                this.body.ResetSpeedX();
                                this.body.SetX(obstacle.x + obstacle.width);
                            }
                            break;
                        case Direction.Top:
                            if (this.body.speedY < 0)
                            {
                                this.body.ResetSpeedY();
                                this.body.SetY(obstacle.y + obstacle.height);
                            }
                            break;
                    }
                }
            }
        }
    }
}
