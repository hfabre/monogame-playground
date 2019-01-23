using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Game1
{
    class Player : GameObject
    {
        public float jumpCount = 0;
        public Texture2D texture;
        public const float maxJump = 1;

        public Player(float x, float y, float width, float height, Texture2D texture) : base(x, y, width, height, GameObject.Type.Player, true)
        {
            this.texture = texture;
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

        public override void Collide(Direction direction, GameObject collider)
        {
            Debug.WriteLine("in player collide");
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
