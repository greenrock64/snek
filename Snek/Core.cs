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

        SnakeHead snake;
        KeyboardState lastKey;
        double moveTimer;


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
            lastKey = Keyboard.GetState();

            moveTimer = 0f;
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            bodyTexture = Content.Load<Texture2D>("textures/square");
            
            // Create our snake
            snake = new SnakeHead(bodyTexture);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Increment the move timer
            moveTimer += gameTime.ElapsedGameTime.TotalSeconds;

            // Re-adjust the snakes heading
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && lastKey.IsKeyUp(Keys.Up))
            {
                snake.setDirection(SnakeDirections.Up);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && lastKey.IsKeyUp(Keys.Right))
            {
                snake.setDirection(SnakeDirections.Right);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) && lastKey.IsKeyUp(Keys.Down))
            {
                snake.setDirection(SnakeDirections.Down);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) && lastKey.IsKeyUp(Keys.Left))
            {
                snake.setDirection(SnakeDirections.Left);
            }

            // Check if the snake needs to move
            if (moveTimer >= 1f)
            {   
                moveTimer = 0;
                snake.updatePosition();
                // TODO - Move snek
            }

            lastKey = Keyboard.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(snake.bodyTexture, new Rectangle(snake.curPos.X*32, snake.curPos.Y*32, 32, 32), Color.Green);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
