using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Sprite
    {
        private Texture2D texture;
        private float scale;
        private SpriteEffects flip;

        public Sprite(string assetName, float scale, bool flip)
        {
            this.texture = GraphicsEngine.GetInstance().textures[assetName];
            this.scale = scale;
            this.flip = flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }

        // public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth);
        public void Draw(float x, float y, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, new Vector2(x, y), null, Color.White, 0f, new Vector2(0, 0), new Vector2(this.scale, this.scale), this.flip, 0f);
        }
    }
}
