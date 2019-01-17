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

        public void Update(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.body.x = x;
            this.body.y = y;
        }

        public void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            DrawUtils.DrawEmptyRectangle(spriteBatch, graphicsDevice,(int)x, (int)y, (int)width, (int)height);
        }
    }
}
