using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Game1
{
    class Player
    {
        public PhysicalObject physicalObject;
        public float x = 0;
        public float y = 0;
        public float width;
        public float height = 0;
        public float speedX = 0;
        public float speedY = 0;
        public float jumpCount = 0;

        public const float moveSpeed = 80;
        public const float friction = .80f;
        public const float gravity = 40;
        public const float jumpSpeed = 350;
        public const float maxJump = 1;

        public Player(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.physicalObject = new PhysicalObject(x, y, width, height);
        }

        public void CalculateSpeed(KeyboardState state)
        {
            this.UpdateSpeedFromKeyboardState(state);
            this.UpdateSpeedFromNaturalForces();
        }

        public void Update(float deltaTime)
        {
            this.ApplySpeed(deltaTime);
            this.physicalObject.Update(this.x, this.y);
        }

        public void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            this.physicalObject.Draw(gameTime, graphicsDevice, spriteBatch);
        }

        public void ResetSpeedX()
        {
            this.speedX = 0;
        }

        public void ResetSpeedY()
        {
            this.speedY = 0;
        }

        public void SetY(float value)
        {
            this.y = value - this.height;
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

        private void Jump()
        {
            if (this.jumpCount <= maxJump)
            {
                this.speedY = this.speedY - jumpSpeed;
                this.jumpCount++;
            }
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

        private void UpdateSpeedFromNaturalForces()
        {
            this.speedX = this.speedX * friction;
            this.speedY = this.speedY + gravity;
        }

        private void ApplySpeed(float deltaTime)
        {
            this.y = this.y + this.speedY * deltaTime;
            this.x = this.x + this.speedX * deltaTime;
        }

        private void MoveLeft()
        {
            this.speedX = this.speedX - moveSpeed;
        }

        private void MoveRight()
        {
            this.speedX = this.speedX + moveSpeed;
        }
    }
}
