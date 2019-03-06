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
        public Dictionary<string, SpriteFont> fonts;
        public float windowHeight;
        public float windowWidth;

        private GraphicsEngine()
        {

        }

        private static readonly GraphicsEngine instance = new GraphicsEngine();

        public static GraphicsEngine GetInstance()
        {
            return instance;
        }

        public void Init(Dictionary<string, Texture2D> textures, Dictionary<string, SpriteFont> fonts, float windowHeight, float windowWidth)
        {
            this.textures = textures;
            this.fonts = fonts;
            this.windowHeight = windowHeight;
            this.windowWidth = windowWidth;
        }
            
        public void Draw(List<GameObject> gameObjects, GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            GameObject player = gameObjects.Find(go => go.type == GameObject.Type.Player);
            Texture2D background = this.textures["background"];
            Rectangle source = new Rectangle((int)player.x / 2, 0, background.Width, background.Height);
            Vector2 destination = new Vector2(-600 + player.x, -300);
            spriteBatch.Draw(background, destination, source, Color.White);

            foreach (GameObject go in gameObjects)
            {
                go.Draw(gameTime, graphicsDevice, spriteBatch);
                go.body.Draw(gameTime, graphicsDevice, spriteBatch);
            }
        }
    }
}
