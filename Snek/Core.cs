using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Snek
{
    public class Core : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Content management
        Texture2D bodyTexture;
        Texture2D pillTexture;

        GameManager gameManager;

        public Core()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Add our window settings
            Window.Title = "Snek v0.2";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;

            // Display the mouse cursor in-game
            this.IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Instantiate our game logic handler
            gameManager = new GameManager();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            bodyTexture = Content.Load<Texture2D>("textures/square");
            pillTexture = Content.Load<Texture2D>("textures/circle");

            gameManager.AddTextures(bodyTexture, pillTexture);
            gameManager.NewGame();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            gameManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(pillTexture, new Rectangle(gameManager.pillPosition.X * 32, gameManager.pillPosition.Y * 32, 32, 32), Color.Red);
            // Recursively dig through and render the whole snake
            for (SnakeBody body = gameManager.snake.childBody; body != null; body = body.childBody)
            {
                // Render the snake body
                spriteBatch.Draw(bodyTexture, new Rectangle(body.curPos.X * 32, body.curPos.Y * 32, 32, 32), Color.Green);
            }
            // Render the snake head
            if (gameManager.hasDied)
            {
                spriteBatch.Draw(bodyTexture, new Rectangle(gameManager.snake.curPos.X * 32, gameManager.snake.curPos.Y * 32, 32, 32), Color.Orange);
            }
            else
            {
                spriteBatch.Draw(bodyTexture, new Rectangle(gameManager.snake.curPos.X * 32, gameManager.snake.curPos.Y * 32, 32, 32), Color.Green);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
