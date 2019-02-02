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
        public const float maxJump = 1;
        public Direction currentDirection;
        public World world;

        public Player(float x, float y, float width, float height, World world) : base(x, y, width, height, GameObject.Type.Player, true)
        {
            this.currentDirection = Direction.Left;
            this.world = world;
        }

        public void CalculateSpeed(KeyboardState state)
        {
            this.UpdateSpeedFromKeyboardState(state);
            this.body.CalculateSpeed();
        }

        public override void Update(float deltaTime)
        {
            this.body.Update(deltaTime);
            this.x = this.body.x;
            this.y = this.body.y;

            if (this.body.speedX > 0)
                this.currentDirection = Direction.Right;
            else
                this.currentDirection = Direction.Left;
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GraphicsEngine.GetInstance().textures["player"], new Vector2(this.x, this.y), Color.White);
        }

        public void ResetJump()
        {
            this.jumpCount = 0;
        }

        public override void Collide(Direction direction, GameObject collider)
        {
            switch(collider.type)
            {
                case Type.Tile:
                    handleTileCollision(direction, collider);
                    break;
                case Type.Bullet:
                    //PhysicsEngine.GetInstance().Remove(collider);
                    //collider = null;
                    Debug.WriteLine("collide with bullet");
                    break;
            }
        }

        private void spawnBullet()
        {
            this.world.Add(new Bullet(this.x, this.y, this.currentDirection, this.world));
        }

        private void UpdateSpeedFromKeyboardState(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.Q))
                MoveLeft();
            if (state.IsKeyDown(Keys.D))
                MoveRight();
            if (state.IsKeyDown(Keys.Space))
                Jump();
            if (state.IsKeyDown(Keys.E))
                spawnBullet();
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

        private void handleTileCollision(Direction direction, GameObject collider)
        {
            switch (direction)
            {
                case Direction.Bottom:
                    this.ResetJump();
                    break;
            }
        }
    }
}
