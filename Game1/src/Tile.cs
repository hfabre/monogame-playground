using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Tile : GameObject
    {
        public bool collidable = true;

        public Tile(float x, float y, float width, float height, bool collidable) : base(x, y, width, height, 0, GameObject.Type.Tile)
        {
            this.collidable = collidable;
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GraphicsEngine.GetInstance().textures["bloc"], new Vector2(x, y), Color.White);
        }
    }
}
