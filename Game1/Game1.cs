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
        World world;
        Dictionary<string, Texture2D> textures;
        public int windowWidth = 1200;
        public int windowHeight = 900;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = windowWidth;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = windowHeight;   // set this value to the desired height of your window
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";

            this.textures = new Dictionary<string, Texture2D>();
            

            // This set max fps to 30. Useful for debug
            // TargetElapsedTime = TimeSpan.FromSeconds(1d / 30d);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D blankTexture = new Texture2D(GraphicsDevice, 1, 1);
            blankTexture.SetData(new[] { Color.White });

            this.textures.Add("player", Content.Load<Texture2D>("player"));
            this.textures.Add("bloc", Content.Load<Texture2D>("bloc"));
            this.textures.Add("bullet", Content.Load<Texture2D>("bullet"));
            this.textures.Add("blank", blankTexture);

            PhysicsEngine.GetInstance().Init();
            GraphicsEngine.GetInstance().Init(this.textures);
            
            this.world = new World();
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
            
            this.world.Update(deltaTime, state);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(transformMatrix: Matrix.CreateTranslation(-this.world.player.x + this.windowWidth / 2, -this.world.player.y + this.windowHeight / 2, 0f));

            this.world.Draw(gameTime, GraphicsDevice, spriteBatch);

            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
