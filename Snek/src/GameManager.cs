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
        double gameSpeed;

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
            gameSpeed = 0.1f;
            hasDied = false;
            // Create our snake
            snake = new SnakeHead();
            // Set the initial pill location
            pillPosition = new Point(rand.Next(1, 16), rand.Next(1, 16));

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

        public bool IsSnakeInBounds(SnakeHead snake)
        {
            return true;
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

                if (Keyboard.GetState().IsKeyDown(Keys.OemPlus) && lastKey.IsKeyUp(Keys.OemPlus))
                {
                    gameSpeed -= 0.01f;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.OemMinus) && lastKey.IsKeyUp(Keys.OemMinus))
                {
                    gameSpeed += 0.01f;
                }

                // Check if the snake needs to move
                if (moveTimer >= gameSpeed)
                {
                    // Calculate the position the snake will move to
                    Point predictedPos = snake.ApplyDirection(snake.curPos);

                    if ((predictedPos.X < 1 || predictedPos.X > 16) || (predictedPos.Y < 1 || predictedPos.Y > 16))
                    {
                        // Snake has left the map
                        hasDied = true;
                        Console.WriteLine("Snake has hit the wall");
                    }
                    else
                    {
                        // Reset our moveTimer
                        moveTimer = 0;
                        // Update the snakes position
                        snake.UpdatePosition();

                        // Check if the snake has collided with itself
                        if (snake.HasCollided())
                        {
                            hasDied = true;
                            Console.WriteLine("Snake has eaten itself");
                        }
                        // Check if the snake has eaten a pill and will grow
                        if (snake.curPos == pillPosition)
                        {
                            snake.EatPill();
                            pillPosition = new Point(rand.Next(1, 16), rand.Next(1, 16));
                        }
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
