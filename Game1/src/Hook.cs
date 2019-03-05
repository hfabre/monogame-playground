using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class Hook : GameObject
    {
        public GameObject parent;
        public bool hooked;
        public float goal_x;
        public float goal_y;

        public Hook(float goal_x, float goal_y, float angle, World world, GameObject parent) : base(parent.x, parent.y, 10, 10, angle, world, GameObject.Type.Hook, true)
        {
            this.goal_x = goal_x;
            this.goal_y = goal_y;
            this.parent = parent;
            this.hooked = false;
            this.world.Add(this);

            this.body.speedX = 100;
            this.body.speedY = 100;
        }

        public override void Update(GameTime time, float deltaTime)
        {
            this.body.Update(deltaTime);
            this.x = this.body.x;
            this.y = this.body.y;
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {

        }

        public override void Collide(Direction direction, GameObject collider)
        {
            if (collider != this.parent)
            {
                this.body.speedX = 0;
                this.body.speedY = 0;
                this.hooked = true;
            }
        }
    }
}

