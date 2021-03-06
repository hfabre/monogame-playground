﻿using System;
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

        public float absolute_x;
        public float absolute_y;

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

        public int hookTimer = 0;
        public bool isHooking = false;
        public bool canHook = true;
        public const int frameBetweenHooks = 100;
        public Hook hook = null;
        public const int hookSpeed = 450;

        public Animator animator = new Animator();

        public Player(float x, float y, float width, float height, float angle, World world) : base(x, y, width, height, angle, world, GameObject.Type.Player, true)
        {
            this.currentDirection = Direction.Left;

            this.absolute_x = GraphicsEngine.GetInstance().windowWidth / 2;
            this.absolute_y = GraphicsEngine.GetInstance().windowWidth / 2;

            string[] runAssets = new string[] { "knight_run_01", "knight_run_02", "knight_run_03", "knight_run_04", "knight_run_05", "knight_run_06", "knight_run_07" };
            AnimatedSprite runRightSprite = new AnimatedSprite(runAssets, .06f, true, false);
            this.animator.AddAnimation("runRight", runRightSprite);

            AnimatedSprite runLeftSprite = new AnimatedSprite(runAssets, .06f, true, true, -30);
            this.animator.AddAnimation("runLeft", runLeftSprite);

            string[] idleAssets = new string[] { "knight_idle_01", "knight_idle_02", "knight_idle_03", "knight_idle_04", "knight_idle_05", "knight_idle_06", "knight_idle_07" };
            AnimatedSprite idleRightSprite = new AnimatedSprite(idleAssets, .06f, true, false);
            this.animator.AddAnimation("idleRight", idleRightSprite);

            AnimatedSprite idleLeftSprite = new AnimatedSprite(idleAssets, .06f, true, true, -30);
            this.animator.AddAnimation("idleLeft", idleLeftSprite);

            string[] attackAssets = new string[] { "knight_attack_01", "knight_attack_02", "knight_attack_03", "knight_attack_04", "knight_attack_05", "knight_attack_06", "knight_attack_07" };
            AnimatedSprite attackRightSprite = new AnimatedSprite(attackAssets, .06f, false, false);
            this.animator.AddAnimation("attackRight", attackRightSprite);

            AnimatedSprite attackLeftSprite = new AnimatedSprite(attackAssets, .06f, false, true, -30);
            this.animator.AddAnimation("attackLeft", attackLeftSprite);

            this.animator.SetCurrentAnimation("idleRight");
        }

        public void CalculateSpeed(KeyboardState state)
        {
            this.UpdateSpeedFromKeyboardState(state);

            if (isHooking && this.hook != null && this.hook.hooked)
            {
                Vector2 hookPosition = new Vector2(this.hook.x, this.hook.y) - new Vector2(this.x, this.y);
                hookPosition.Normalize();
                Vector2 velocity = hookPosition * hookSpeed;

                this.body.speedX = velocity.X;
                this.body.speedY = velocity.Y;
            }
            else
            {
                this.body.CalculateSpeed();
            }
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

            if (isHooking)
            {
                hookTimer++;

                if (hookTimer >= frameBetweenHooks)
                {
                    this.hookTimer = 0;
                    this.canHook = true;
                }
            }

            this.body.Update(deltaTime);
            this.SelectAnimation();
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
            switch (collider.type)
            {
                case Type.Tile:
                    HandleTileCollision(direction, collider);
                    break;
                default:
                    break;
            }
        }

        private void SelectAnimation()
        {
            if (hasUsedSword && this.animator.currentAnimation != "attackRight" && this.animator.currentAnimation != "attackRight")
            {
                if (this.currentDirection == Direction.Left)
                    this.animator.SetCurrentAnimation("attackLeft");
                else
                    this.animator.SetCurrentAnimation("attackRight");
            }
            else if (!hasUsedSword)
            {
                if (this.body.speedX > 1)
                {
                    this.currentDirection = Direction.Right;
                    this.animator.SetCurrentAnimation("runRight");
                }
                else if (this.body.speedX < -1)
                {
                    this.currentDirection = Direction.Left;
                    this.animator.SetCurrentAnimation("runLeft");
                }
                else
                {
                    if (this.currentDirection == Direction.Left)
                        this.animator.SetCurrentAnimation("idleLeft");
                    else
                        this.animator.SetCurrentAnimation("idleRight");
                }
            }
        }

        private void SpawnBullet()
        {
            if (canLaunchBullet)
            {
                this.hasLaunchBullet = true;
                this.canLaunchBullet = false;
                this.world.Add(new Bullet(this.x, this.y, 0, this.world, this.currentDirection));
            }
        }

        private void UseSword()
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

        private void Hook()
        {
            if (canHook)
            {
                this.isHooking = true;
                this.canHook = false;
                if (this.hook != null)
                {
                    this.world.Remove(this.hook);
                    this.hook = null;
                }
                this.hook = new Hook(0, this.world, this);
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
                SpawnBullet();
            if (state.IsKeyDown(Keys.A))
                UseSword();
            if (state.IsKeyDown(Keys.X))
                Hook();

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
            if (isHooking)
            {
                isHooking = false;
                this.world.Remove(hook);
                this.hook = null;
                this.canHook = true;
            }

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
                    if (this.body.speedY > 0)
                    {
                        this.body.ResetSpeedY();
                        this.body.SetY(collider.body.y - this.body.height);
                        this.ResetJump();
                    }
                    break;
                case Direction.Left:
                    if (this.body.speedX > 0)
                    {
                        this.body.ResetSpeedX();
                        this.body.SetX(collider.body.x - this.body.width);
                    }
                    break;
                case Direction.Right:
                    if (this.body.speedX < 0)
                    {
                        this.body.ResetSpeedX();
                        this.body.SetX(collider.body.x + collider.body.width);
                    }
                    break;
                case Direction.Top:
                    if (this.body.speedY < 0)
                    {
                        this.body.ResetSpeedY();
                        this.body.SetY(collider.body.y + collider.body.height);
                    }
                    break;
            }
        }
    }
}
