using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Grass : GameObject
    {
        public bool collidable = true;

        public Grass(float x, float y, float width, float height, World world) : base(x, y, width, height, 0, world, GameObject.Type.Grass)
        {
            this.collidable = false;
        }

        public override void Hit(GameObject attacker)
        {
            if (attacker.type == GameObject.Type.Sword || attacker.type == GameObject.Type.Bullet)
            {
                this.world.Remove(this);
            }
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GraphicsEngine.GetInstance().textures["grass"], new Vector2(x, y), Color.White);
        }
    }
}
