using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Bullet : GameObject
    {
        public Texture2D texture;
        public Direction direction;

        public Bullet(float x, float y, Direction direction, Texture2D texture) : base(x, y, 20, 10, GameObject.Type.Bullet, true)
        {
            this.texture = texture;
            this.direction = direction;

            if (this.direction == Direction.Left)
            {
                this.body.x -= 5;
                this.x -= 5;
            }
            else
            {
                this.body.x += 35;
                this.x += 35;
            }
        }

        public override void Update(float deltaTime)
        {
            if (this.direction == Direction.Left)
                this.body.MoveLeft();
            if (this.direction == Direction.Right)
                this.body.MoveRight();
            this.body.Update(deltaTime);
            this.x = this.body.x;
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(this.x, this.y), Color.White);
        }

        public override void Collide(Direction direction, GameObject collider)
        {

        }
    }
}
