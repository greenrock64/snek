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

        // Game state management
        Point gotoLocation;
        Point curMousePos;

        SnakeBody[] bodyParts;

        public Core()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Add our window settings
            Window.Title = "Snek v0.1";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;

            // Display the mouse cursor in-game
            this.IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Random rand = new Random();
            gotoLocation = new Point();

            // Create our snake
            bodyParts = new SnakeBody[20];

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            bodyTexture = Content.Load<Texture2D>("textures/circle");

            bodyParts[0] = new SnakeBody(null, bodyTexture, 20);
            bodyParts[1] = new SnakeBody(bodyParts[0], bodyTexture, 20);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Store the current cursor position for easy access
            curMousePos = Mouse.GetState().Position;

            // Guard condition for closing the window
            if (Window != null)
            {
                // Check that the mouse is within the game window
                if ((curMousePos.X > 0 && curMousePos.X <= Window.ClientBounds.Width) && (curMousePos.Y > 0 && curMousePos.Y <= Window.ClientBounds.Height))
                {
                    gotoLocation = new Point(curMousePos.X, curMousePos.Y);
                }
                else
                {
                    gotoLocation = new Point(bodyParts[0].curPos.X, bodyParts[0].curPos.Y);
                }

                // Update the head position if we need to
                if(gotoLocation != bodyParts[0].curPos) {
                    bodyParts[0].updatePosition(gotoLocation);
                }

                foreach (SnakeBody body in bodyParts)
                {
                    if (body != null)
                    {
                        body.updatePosition();
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            foreach (SnakeBody body in bodyParts)
            {
                if (body != null)
                {
                    spriteBatch.Draw(body.bodyTexture, new Rectangle(body.curPos.X, body.curPos.Y, body.size, body.size), Color.Red);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
