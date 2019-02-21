using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace Game1
{
    class Player : GameObject
    {
        enum Status
        {
            Idle,
            Run,
            Attack,
            Throw,
            Jump,
            Fall
        };

        public float jumpCount = 0;
        public const float maxJump = 1;

        public int bulletTimer = 0;
        public bool hasLaunchBullet = false;
        public bool canLaunchBullet = true;
        public const int frameBetweenBullets = 30;


        public int swordTimer = 0;
        public bool hasUsedSword = false;
        public bool canUseSword = true;
        public const int frameBetweenSwords = 30;
        public Sword sword = null;

        public Player(float x, float y, float width, float height, float angle, World world) : base(x, y, width, height, angle, world, GameObject.Type.Player, true)
        {
            this.currentDirection = Direction.Left;
        }

        public void CalculateSpeed(KeyboardState state)
        {
            this.UpdateSpeedFromKeyboardState(state);
            this.body.CalculateSpeed();
        }

        public override void Update(float deltaTime)
        {
            if (hasLaunchBullet)
            {
                bulletTimer++;

                if (bulletTimer >= frameBetweenBullets)
                {
                    this.bulletTimer = 0;
                    this.hasLaunchBullet = false;
                    this.canLaunchBullet = true;
                }
            }

            if (hasUsedSword)
            {
                swordTimer++;

                if (swordTimer >= frameBetweenSwords)
                {
                    this.swordTimer = 0;
                    this.hasUsedSword = false;
                    this.canUseSword = true;
                    if (this.sword != null)
                        this.world.Remove(this.sword);
                }
            }

            this.body.Update(deltaTime);

            if (this.body.speedX > 0)
                this.currentDirection = Direction.Right;
            else if (this.body.speedX < 0)
                this.currentDirection = Direction.Left;
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            Rectangle destRectanle = new Rectangle(0, 0, (int)this.width, (int)this.height);

            spriteBatch.Draw(GraphicsEngine.GetInstance().textures["player"], 
                new Vector2(this.x, this.y),
                destRectanle,
                Color.White, 
                angle, 
                new Vector2(0, 0),
                1, SpriteEffects.None,
                1);
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
                    HandleTileCollision(direction, collider);
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
            if (canLaunchBullet)
            {
                this.hasLaunchBullet = true;
                this.canLaunchBullet = false;
                this.world.Add(new Bullet(this.x, this.y, 0, this.world, this.currentDirection));
            }
        }

        private void useSword()
        {
            if (canUseSword)
            {
                this.hasUsedSword = true;
                this.canUseSword = false;
                float swordX = this.currentDirection == Direction.Left ? this.x - 30 : this.x + this.width;
                float swordY = this.y + 10;
                this.sword = new Sword(swordX, swordY, 0, this.world, this);
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
            if (state.IsKeyDown(Keys.E))
                spawnBullet();
            if (state.IsKeyDown(Keys.A))
                useSword();
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

        private void HandleTileCollision(Direction direction, GameObject collider)
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
