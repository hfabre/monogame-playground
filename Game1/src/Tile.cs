using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Tile
    {
        public bool collidable = true;
        public Texture2D texture;

        public Tile(bool collidable, Texture2D texture)
        {
            this.collidable = collidable;
            this.texture = texture;
        }

        public void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, float x, float y)
        {
            spriteBatch.Draw(texture, new Vector2(x, y), Color.White);
        }
    }
}
