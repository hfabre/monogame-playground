using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class Sword : GameObject
    {
        public World world;
        public GameObject parent;

        public Sword(float x, float y, float angle, World world, GameObject parent) : base(PositionFromParent(parent).X, PositionFromParent(parent).Y, 30, 70, angle, GameObject.Type.Sword, true)
        {
            this.world = world;
            this.parent = parent;
            this.world.Add(this);
        }

        public override void Update(float deltaTime)
        {
            SyncWithParent();
            this.body.Update(deltaTime);
            this.x = this.body.x;
            this.y = this.body.y;
        }

        public void SyncWithParent()
        {
            this.body.x = PositionFromParent(this.parent).X;
            this.body.y = PositionFromParent(this.parent).Y;
        }

        public static Vector2 PositionFromParent(GameObject parent)
        {
            return new Vector2(parent.currentDirection == Direction.Left ? parent.x - 30 : parent.x + parent.width, parent.y);
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
//            Rectangle destRectanle = new Rectangle(0, 0, (int)this.width, (int)this.height);
//
//            spriteBatch.Draw(GraphicsEngine.GetInstance().textures["sword"],
//                new Vector2(this.x, this.y),
//                destRectanle,
//                Color.White,
//                angle,
//                new Vector2(0, 0),
//                1, SpriteEffects.None,
//                1);
        }

        public override void Collide(Direction direction, GameObject collider)
        {
            collider.Hit(this);
        }
    }
}
