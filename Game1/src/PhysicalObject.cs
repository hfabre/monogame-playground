using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1
{
    class PhysicalObject
    {
        public float x;
        public float y;
        public float width;
        public float height;
        public Body body;

        public PhysicalObject(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.body = new Body(x, y, width, height);
        }

        public void Update(float deltaTime)
        {
            this.body.Update(deltaTime);
            this.x = this.body.x;
            this.y = this.body.y;
        }

        public void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            DrawUtils.DrawEmptyRectangle(spriteBatch, graphicsDevice,(int)x, (int)y, (int)width, (int)height);
        }

        public Vector2 FuturPosition(float deltaTime)
        {
            return this.body.FuturPosition(deltaTime);
        }

        public void MoveLeft()
        {
            this.body.MoveLeft();
        }

        public void MoveRight()
        {
            this.body.MoveRight();
        }

        public void Jump()
        {
            this.body.Jump();
        }
    }
}
