using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        PhysicalObject ground;
        List<PhysicalObject> obstacles = new List<PhysicalObject>();

        public const int windowWidth = 1200;
        public const int windowHeight = 900;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = windowWidth;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = windowHeight;   // set this value to the desired height of your window
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.player = new Player(50, 50, 30, 70);
            PhysicalObject platform = new PhysicalObject(0, 650, 300, 32);
            PhysicalObject step = new PhysicalObject(600, 800, 150, 50);
            this.ground = new PhysicalObject(0, 850, 1200, 50);

            this.obstacles.Add(this.ground);
            this.obstacles.Add(platform);
            this.obstacles.Add(step);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
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

            this.player.Log();

            this.player.Update(deltaTime, state, this.obstacles);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            this.player.Draw(gameTime, GraphicsDevice, spriteBatch);


            obstacles.ForEach(delegate (PhysicalObject obstacle)
            {
                obstacle.Draw(gameTime, GraphicsDevice, spriteBatch);
            });

            base.Draw(gameTime);

            spriteBatch.End();
        }
    }
}
