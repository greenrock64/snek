using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snek
{
    class SnakeBody
    {
        // Attributes
        public Texture2D bodyTexture;
        public int size;
        public Point curPos;

        // Logic
        SnakeBody parentBody;
        Vector2 lastDirection;

        public SnakeBody(SnakeBody parentBody, Texture2D bodyTexture, int size)
        {
            if (parentBody == null) {
                // Head of the snake, so randomise a starting position
                Random rand = new Random();
                this.curPos = new Point(rand.Next(20, 780), rand.Next(20, 580));
            }
            else
            {
                // Body part
                this.parentBody = parentBody;
            }
            this.bodyTexture = bodyTexture;
            this.size = size;
            lastDirection = new Vector2();
        }

        // Calculate a new position based on the parent SnakeBody position
        public void updatePosition() {

        }

        // updatePosition override to allow the head to follow a given point (e.g. the cursor)
        public void updatePosition(Point gotoLocation)
        {
            // Calculate deltaX as the intended destination less the current snake position (taking into account the snake body size)
            float deltaX = (gotoLocation.X - (curPos.X) - size / 2);
            // Slow the speed over distance, while keeping a smooth speed gradient
            deltaX /= 10f;
            // Calculate the same delta for Y
            float deltaY = (gotoLocation.Y - (curPos.Y) - size / 2);
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
            curPos += new Point(MathHelper.Clamp((int)deltaX, -speedCap, speedCap), MathHelper.Clamp((int)deltaY, -speedCap, speedCap));
        }
    }
}
