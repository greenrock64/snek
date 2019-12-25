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
        Texture2D bodyPart;

        // Game state management
        Point gotoLocation;
        Point curSnakePos;
        Point curMousePos;

        int snakeBodySize;

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
            curSnakePos = new Point(rand.Next(20, 780), rand.Next(20, 580));
            gotoLocation = new Point(curSnakePos.X, curSnakePos.Y);
            snakeBodySize = 20;


            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            bodyPart = Content.Load<Texture2D>("textures/circle");
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
                    gotoLocation = new Point(curSnakePos.X, curSnakePos.Y);
                }

                if(gotoLocation != curSnakePos) {
                    // Calculate deltaX as the intended destination less the current snake position (taking into account the snake body size)
                    float deltaX = (gotoLocation.X - (curSnakePos.X) - snakeBodySize / 2);
                    // Slow the speed over distance, while keeping a smooth speed gradient
                    deltaX /= 10f;
                    // Calculate the same delta for Y
                    float deltaY = (gotoLocation.Y - (curSnakePos.Y) - snakeBodySize / 2);
                    deltaY /= 10f;

                    Console.WriteLine("Deltas - X: {0}, Y: {1}", deltaX, deltaY);

                    // Handle near distance travel
                    if (deltaX > 0 && deltaX < 1)
                    {
                        deltaX = 1;
                    }
                    else if (deltaX < 0 && deltaX > -1)
                    {
                        deltaX = -1;
                    }
                    if (deltaY > 0 && deltaY < 1)
                    {
                        deltaY = 1;
                    }
                    else if (deltaY < 0 && deltaY > -1)
                    {
                        deltaY = -1;
                    }

                    int speedCap = 25;

                    // Move the snake towards the cursor
                    curSnakePos += new Point(MathHelper.Clamp((int)deltaX, -speedCap, speedCap), MathHelper.Clamp((int)deltaY, -speedCap, speedCap));
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(bodyPart, new Rectangle(curSnakePos.X, curSnakePos.Y, snakeBodySize, snakeBodySize), Color.Red);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
