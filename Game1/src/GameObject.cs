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
        public enum Type { Player, Tile, Bullet };

        public Body body;
        public float x;
        public float y;
        public float width;
        public float height;
        public Type type;

        public GameObject(float x, float y, float width, float height, Type type, bool needsCollisionCheck = false)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.type = type;

            this.body = new Body(x, y, width, height, this, needsCollisionCheck);
            PhysicsEngine.GetInstance().Add(this);
        }

        public virtual void Update(float deltaTime)
        {
            this.body.Update(deltaTime);
            this.x = this.body.x;
            this.y = this.body.y;
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
