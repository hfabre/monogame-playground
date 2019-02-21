using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Bullet : GameObject
    {
        public Direction direction;
        public int speed = 400;

        public Bullet(float x, float y, float angle, World world, Direction direction) : base(x, y, 20, 10, angle, world, GameObject.Type.Bullet, true)
        {
            this.direction = direction;

            if (this.direction == Direction.Left)
            {
                this.body.x -= 25;
                this.x -= 25;
                this.body.speedX -= this.speed;
            }
            else
            {
                this.body.x += 35;
                this.x += 35;
                this.body.speedX += this.speed;
            }
        }

        public override void Update(float deltaTime)
        {
            this.body.Update(deltaTime);
            this.x = this.body.x;
            this.y = this.body.y;
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            Rectangle destRectanle = new Rectangle(0, 0, (int)this.width, (int)this.height);

            spriteBatch.Draw(GraphicsEngine.GetInstance().textures["bullet"],
                new Vector2(this.x, this.y),
                destRectanle,
                Color.White,
                angle,
                new Vector2(0, 0),
                1, SpriteEffects.None,
                1);
        }

        public override void Collide(Direction direction, GameObject collider)
        {
            collider.Hit(this);
            this.world.Remove(this);
        }
    }
}
