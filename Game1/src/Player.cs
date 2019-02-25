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
        public const float maxJump = 2;
        public int jumpTimer = 0;
        public bool canJump = true;
        public bool hasJumped = false;
        public const int frameBetweenJumps = 30;

        public int bulletTimer = 0;
        public bool hasLaunchBullet = false;
        public bool canLaunchBullet = true;
        public const int frameBetweenBullets = 30;


        public int swordTimer = 0;
        public bool hasUsedSword = false;
        public bool canUseSword = true;
        public const int frameBetweenSwords = 30;
        public Sword sword = null;

        public Animator animator = new Animator();

        public Player(float x, float y, float width, float height, float angle, World world) : base(x, y, width, height, angle, world, GameObject.Type.Player, true)
        {
            this.currentDirection = Direction.Left;

            string[] runAssets = new string[] { "knight_run_01", "knight_run_02", "knight_run_03", "knight_run_04", "knight_run_05", "knight_run_06", "knight_run_07" };
            AnimatedSprite runRightSprite = new AnimatedSprite(runAssets, .06f, true, false);
            this.animator.AddAnimation("runRight", runRightSprite);

            AnimatedSprite runLeftSprite = new AnimatedSprite(runAssets, .06f, true, true);
            this.animator.AddAnimation("runLeft", runLeftSprite);

            string[] idleAssets = new string[] { "knight_idle_01", "knight_idle_02", "knight_idle_03", "knight_idle_04", "knight_idle_05", "knight_idle_06", "knight_idle_07" };
            AnimatedSprite idleSprite = new AnimatedSprite(idleAssets, .06f, true, false);
            this.animator.AddAnimation("idle", idleSprite);

            this.animator.SetCurrentAnimation("idle");
        }

        public void CalculateSpeed(KeyboardState state)
        {
            this.UpdateSpeedFromKeyboardState(state);
            this.body.CalculateSpeed();
        }

        public override void Update(GameTime time, float deltaTime)
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

            if (hasJumped)
            {
                jumpTimer++;

                if (jumpTimer >= frameBetweenJumps)
                {
                    this.jumpTimer = 0;
                    this.hasJumped = false;
                    this.canJump = true;
                }
            }

            this.body.Update(deltaTime);

            if (this.body.speedX > 0.001)
            {
                this.currentDirection = Direction.Right;
                this.animator.SetCurrentAnimation("runRight");
            } else if (this.body.speedX < 0.001)
            {
                this.currentDirection = Direction.Left;
                this.animator.SetCurrentAnimation("runLeft");
            }
            else
            {
                this.animator.SetCurrentAnimation("idle");
            }

            this.animator.Update(time);
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
           // Rectangle destRectanle = new Rectangle(0, 0, (int)this.width, (int)this.height);

//            spriteBatch.Draw(GraphicsEngine.GetInstance().textures["player"], 
//                new Vector2(this.x, this.y),
//                destRectanle,
//                Color.White, 
//                angle, 
//                new Vector2(0, 0),
//                1, SpriteEffects.None,
//                1);

            this.animator.Draw(gameTime, this.x, this.y, spriteBatch);


            spriteBatch.DrawString(GraphicsEngine.GetInstance().fonts["debug"], "Player information", new Vector2(this.x - 100, this.y - 100), Color.Black);
            spriteBatch.DrawString(GraphicsEngine.GetInstance().fonts["debug"], "Position: " + this.x + " - " + this.y, new Vector2(this.x - 100, this.y - 75), Color.Black);
            spriteBatch.DrawString(GraphicsEngine.GetInstance().fonts["debug"], "Velocity: " + this.body.speedX + " - " + this.body.speedY, new Vector2(this.x - 100, this.y - 50), Color.Black);
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
            if (this.jumpCount < maxJump && canJump)
            {
                this.body.Jump();
                this.jumpCount++;
                this.hasJumped = true;
                this.canJump = false;
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
