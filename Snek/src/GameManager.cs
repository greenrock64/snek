using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Snek
{
    class GameManager
    {
        // Game management
        public SnakeHead snake;
        public Point pillPosition;
        double moveTimer;

        // Game state
        public bool hasDied;

        // Utility
        Random rand;

        // Input management
        KeyboardState lastKey;

        // TEMP Asset Management
        Texture2D bodyTexture;
        Texture2D pillTexture;

        public GameManager()
        {
            // Track our keyboard state
            lastKey = Keyboard.GetState();

            // Set-up the initial gamestate
            hasDied = false;
            rand = new Random();
        }

        // Temporary set-up function to add textures
        public void AddTextures(Texture2D body, Texture2D pill)
        {
            bodyTexture = body;
            pillTexture = pill;
        }

        public void NewGame()
        {
            hasDied = false;
            // Create our snake
            snake = new SnakeHead();
            // Set the initial pill location
            pillPosition = new Point(rand.Next(0, 16), rand.Next(0, 16));

            moveTimer = 0f;
        }

        public void SetSnakeDirection()
        {
            // Re-adjust the snakes heading
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && lastKey.IsKeyUp(Keys.Up))
            {
                if (!(snake.curPos + new Point(0, -1) == snake.lastPos))
                {
                    snake.SetDirection(SnakeDirections.Up);
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && lastKey.IsKeyUp(Keys.Right))
            {
                if (!(snake.curPos + new Point(1, 0) == snake.lastPos))
                {
                    snake.SetDirection(SnakeDirections.Right);
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) && lastKey.IsKeyUp(Keys.Down))
            {
                if (!(snake.curPos + new Point(0, 1) == snake.lastPos))
                {
                    snake.SetDirection(SnakeDirections.Down);
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) && lastKey.IsKeyUp(Keys.Left))
            {
                if (!(snake.curPos + new Point(-1, 0) == snake.lastPos))
                {
                    snake.SetDirection(SnakeDirections.Left);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            // Increment the move timer
            moveTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (!hasDied)
            {
                SetSnakeDirection();
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && lastKey.IsKeyUp(Keys.Space))
                {
                    snake.EatPill();
                }

                // Check if the snake needs to move
                if (moveTimer >= 0.1f)
                {
                    moveTimer = 0;
                    snake.UpdatePosition();
                    if (snake.HasCollided())
                    {
                        hasDied = true;
                        Console.WriteLine("Snake has eaten itself");
                    }
                    if (snake.curPos == pillPosition)
                    {
                        snake.EatPill();
                        pillPosition = new Point(rand.Next(0, 16), rand.Next(0, 16));
                    }
                }
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && lastKey.IsKeyUp(Keys.Space))
                {
                    NewGame();
                }
            }
            lastKey = Keyboard.GetState();
        }
    }
}
