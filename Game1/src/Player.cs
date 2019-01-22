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
            this.body = new Body(x, y, width, height, true, this);
            PhysicsEngine.GetInstance().Add(this.body);
        }

        public void CalculateSpeed(KeyboardState state)
        {
            this.UpdateSpeedFromKeyboardState(state);
            this.body.CalculateSpeed();
        }

        public void ApplySpeed(float deltaTime)
        {
            this.body.Update(deltaTime);
            this.x = this.body.x;
            this.y = this.body.y;
        }

        public void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(this.x, this.y), Color.White);
        }

        public void ResetJump()
        {
            this.jumpCount = 0;
        }

        public void Collide(Direction direction, Body collider)
        {
            switch (direction)
            {
                case Direction.Bottom:
                    this.ResetJump();
                    if (this.body.speedY > 0)
                    {
                        this.body.ResetSpeedY();
                        this.body.SetY(collider.y - this.height);
                    }
                    break;
                case Direction.Left:
                    if (this.body.speedX > 0)
                    {
                        this.body.ResetSpeedX();
                        this.body.SetX(collider.x - this.width);
                    }
                    break;
                case Direction.Right:
                    if (this.body.speedX < 0)
                    {
                        this.body.ResetSpeedX();
                        this.body.SetX(collider.x + collider.width);
                    }
                    break;
                case Direction.Top:
                    if (this.body.speedY < 0)
                    {
                        this.body.ResetSpeedY();
                        this.body.SetY(collider.y + collider.height);
                    }
                    break;
            }
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
    }
}
