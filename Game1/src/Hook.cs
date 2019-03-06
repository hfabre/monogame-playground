using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    class Hook : GameObject
    {
        public GameObject parent;
        public bool hooked;
        public float goal_x;
        public float goal_y;
        public const float force = 750;

        public Hook(float angle, World world, Player parent) : base(parent.x, parent.y, 10, 10, angle, world, GameObject.Type.Hook, true)
        {
            this.goal_x = Mouse.GetState().Position.X;
            this.goal_y = Mouse.GetState().Position.Y;
            this.parent = parent;
            this.hooked = false;
            this.world.Add(this);

            Vector2 hookPosition = new Vector2(goal_x, goal_y) - new Vector2(parent.absolute_x, parent.absolute_y);
            hookPosition.Normalize();
            Vector2 velocity = hookPosition * force;

            this.body.speedX = velocity.X;
            this.body.speedY = velocity.Y;
        }

        public override void Update(GameTime time, float deltaTime)
        {
            this.body.Update(deltaTime);
            this.x = this.body.x;
            this.y = this.body.y;
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

