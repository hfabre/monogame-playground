using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class World
    {
        public Player player;
        public Map map;
        public List<GameObject> gameObjects;
        public List<GameObject> toRemove;

        public World()
        {
            this.gameObjects = new List<GameObject>();
            this.toRemove = new List<GameObject>();

            this.map = new Map();
            this.player = new Player(50, 50, 30, 70, 0, this);
            this.Add(this.player);

            for (int y = 0; y < map.board.GetLength(0); y++)
            {
                for (int x = 0; x < map.board.GetLength(1); x++)
                {
                    if (map.board[y, x] == 1)
                    {
                        this.Add(new Tile(x * 32f, y * 32f, 32f, 32f, true));
                    } else if (map.board[y, x] == 2)
                    {
                        this.Add(new Grass(x * 32f + 17, y * 32f + 17, 32f, 32f));
                    }
                }
            }
        }

        public void Update(float deltaTime, KeyboardState state)
        {
            this.toRemove.ForEach(go => this.gameObjects.Remove(go));

            //this.player.Log();

            this.player.CalculateSpeed(state);
            PhysicsEngine.GetInstance().Update(this.gameObjects, deltaTime);
        }

        public void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            GraphicsEngine.GetInstance().Draw(this.gameObjects, gameTime, graphicsDevice, spriteBatch);
        }

        public void Add(GameObject go)
        {
            this.gameObjects.Add(go);
        }

        public void Remove(GameObject go)
        {
            this.toRemove.Add(go);
        }
    }
}
