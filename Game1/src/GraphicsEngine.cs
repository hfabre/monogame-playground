using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class GraphicsEngine
    {
        public Dictionary<string, Texture2D> textures;

        private GraphicsEngine()
        {

        }

        private static readonly GraphicsEngine instance = new GraphicsEngine();

        public static GraphicsEngine GetInstance()
        {
            return instance;
        }

        public void Init(Dictionary<string, Texture2D> textures)
        {
            this.textures = textures;
        }
            
        public void Draw(List<GameObject> gameObjects, GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            foreach (GameObject go in gameObjects)
            {
                go.Draw(gameTime, graphicsDevice, spriteBatch);
                go.body.Draw(gameTime, graphicsDevice, spriteBatch);
            }
        }
    }
}
