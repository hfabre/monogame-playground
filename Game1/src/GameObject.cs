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
    class GameObject
    {
        public enum Type { Player, Tile, Bullet, Sword, Grass, Hook };

        public World world;
        public Body body;
        public float x;
        public float y;
        public float width;
        public float height;
        public float angle;
        public Type type;
        public Direction currentDirection;

        public GameObject(float x, float y, float width, float height, float angle, World world, Type type, bool needsCollisionCheck = false)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.angle = angle;
            this.type = type;
            this.world = world;

            this.body = new Body(x, y, width, height, angle, this, needsCollisionCheck);
        }

        public virtual void Update(GameTime time, float deltaTime)
        {
            this.body.Update(deltaTime);
        }

        public virtual void SynchWithBody()
        {
            this.x = this.body.x;
            this.y = this.body.y;
            this.angle = this.body.angle;
        }

        public virtual void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {

        }

        public virtual void Hit(GameObject attacker)
        {

        }

        public virtual void Collide(Direction direction, GameObject collider)
        {

        }

        public void Log()
        {
            Debug.WriteLine("-------- DEBUG GAME OBJECT -----------");
            Debug.WriteLine(type + " pos: " + this.x + " - " + this.y);
            Debug.WriteLine("Body pos: " + this.body.x + " - " + this.body.y);
            Debug.WriteLine("----------- END GAME OBJECT -----------");
        }
    }
}
