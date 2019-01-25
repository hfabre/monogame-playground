using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Map map;
        Texture2D blocTexture;
        Texture2D playerTexture;
        Texture2D bulletTexture;
        public int windowWidth = 1200;
        public int windowHeight = 900;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = windowWidth;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = windowHeight;   // set this value to the desired height of your window
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";

            // This set max fps to 30. Useful for debug
            // TargetElapsedTime = TimeSpan.FromSeconds(1d / 30d);
        }

        protected override void Initialize()
        {
            base.Initialize();
            PhysicsEngine.GetInstance().Init();
            this.player = new Player(50, 50, 30, 70, playerTexture, bulletTexture);
            this.map = new Map(blocTexture);

            for (int y = 0; y < map.tileBoard.GetLength(0); y++)
            {
                for (int x = 0; x < map.tileBoard.GetLength(1); x++)
                {
                    if (map.tileBoard[y, x].collidable)
                    {
                        PhysicsEngine.GetInstance().Add(new GameObject(x * 32, y * 32, 32, 32, GameObject.Type.Tile));
                    }
                }
            }
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            blocTexture = Content.Load<Texture2D>("bloc");
            playerTexture = Content.Load<Texture2D>("player");
            bulletTexture = Content.Load<Texture2D>("bullet");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState state = Keyboard.GetState();

            //this.player.Log();

            this.player.CalculateSpeed(state);
            PhysicsEngine.GetInstance().Update(deltaTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(transformMatrix: Matrix.CreateTranslation(-this.player.x + this.windowWidth / 2, -this.player.y + this.windowHeight / 2, 0f));

            this.player.Draw(gameTime, GraphicsDevice, spriteBatch);

            for (int y = 0; y < map.tileBoard.GetLength(0); y++)
            {
                for (int x = 0; x < map.tileBoard.GetLength(1); x++)
                {
                    if (map.tileBoard[y, x].collidable)
                    {
                        map.tileBoard[y, x].Draw(gameTime, GraphicsDevice, spriteBatch, x * 32, y * 32);
                    }
                }
            }

            PhysicsEngine.GetInstance().Draw(gameTime, GraphicsDevice, spriteBatch);
            base.Draw(gameTime);

            spriteBatch.End();
        }
    }
}
