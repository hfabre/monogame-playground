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
        Dictionary<string, SpriteFont> fonts;
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
            this.fonts = new Dictionary<string, SpriteFont>();
            

            // This set max fps to 30. Useful for debug
            // TargetElapsedTime = TimeSpan.FromSeconds(1d / 30d);
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D blankTexture = new Texture2D(GraphicsDevice, 1, 1);
            blankTexture.SetData(new[] { Color.White });

            this.textures.Add("player", Content.Load<Texture2D>("player"));
            this.textures.Add("bloc", Content.Load<Texture2D>("bloc_tile"));
            this.textures.Add("bullet", Content.Load<Texture2D>("bullet"));
            this.textures.Add("grass", Content.Load<Texture2D>("grass_tile"));
            this.textures.Add("background", Content.Load<Texture2D>("background"));
            this.textures.Add("blank", blankTexture);

            this.textures.Add("knight_run_01", Content.Load<Texture2D>("knight_run_01"));
            this.textures.Add("knight_run_02", Content.Load<Texture2D>("knight_run_02"));
            this.textures.Add("knight_run_03", Content.Load<Texture2D>("knight_run_03"));
            this.textures.Add("knight_run_04", Content.Load<Texture2D>("knight_run_04"));
            this.textures.Add("knight_run_05", Content.Load<Texture2D>("knight_run_05"));
            this.textures.Add("knight_run_06", Content.Load<Texture2D>("knight_run_06"));
            this.textures.Add("knight_run_07", Content.Load<Texture2D>("knight_run_07"));

            this.textures.Add("knight_idle_01", Content.Load<Texture2D>("knight_idle_01"));
            this.textures.Add("knight_idle_02", Content.Load<Texture2D>("knight_idle_02"));
            this.textures.Add("knight_idle_03", Content.Load<Texture2D>("knight_idle_03"));
            this.textures.Add("knight_idle_04", Content.Load<Texture2D>("knight_idle_04"));
            this.textures.Add("knight_idle_05", Content.Load<Texture2D>("knight_idle_05"));
            this.textures.Add("knight_idle_06", Content.Load<Texture2D>("knight_idle_06"));
            this.textures.Add("knight_idle_07", Content.Load<Texture2D>("knight_idle_07"));

            this.textures.Add("knight_attack_01", Content.Load<Texture2D>("knight_attack_01"));
            this.textures.Add("knight_attack_02", Content.Load<Texture2D>("knight_attack_02"));
            this.textures.Add("knight_attack_03", Content.Load<Texture2D>("knight_attack_03"));
            this.textures.Add("knight_attack_04", Content.Load<Texture2D>("knight_attack_04"));
            this.textures.Add("knight_attack_05", Content.Load<Texture2D>("knight_attack_05"));
            this.textures.Add("knight_attack_06", Content.Load<Texture2D>("knight_attack_06"));
            this.textures.Add("knight_attack_07", Content.Load<Texture2D>("knight_attack_07"));

            this.fonts.Add("debug", Content.Load<SpriteFont>("debug"));



            PhysicsEngine.GetInstance().Init();
            GraphicsEngine.GetInstance().Init(this.textures, this.fonts);
            
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
            
            this.world.Update(gameTime, deltaTime, state);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(transformMatrix: Matrix.CreateTranslation(-this.world.player.x + this.windowWidth / 2, -this.world.player.y / 2 + this.windowHeight / 2, 0f));

            this.world.Draw(gameTime, GraphicsDevice, spriteBatch);

            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
