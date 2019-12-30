using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snek
{
    class SnakeHead : SnakeBody
    {
        int direction;

        public SnakeHead()
        {
            curPos = new Point(1,1);
            direction = SnakeDirections.Right;
        }

        // UpdatePosition updates the curPos value with the stored direction
        new public void UpdatePosition() {
            // Store the last pos for future calculations
            lastPos = curPos;

            // Calculate the new position
            curPos = ApplyDirection(curPos);
            if (childBody != null)
            {
                // Update child components
                childBody.UpdatePosition();
            }
        }

        // Helper function to apply directional movement to the current position
        public Point ApplyDirection(Point position)
        {
            switch (direction)
            {
                case 1: // Up
                    return position + SnakeDirections.UpDir;
                case 2: // Right
                    return position + SnakeDirections.RightDir;
                case 3: // Down
                    return position + SnakeDirections.DownDir;
                case 4: // Left
                    return position + SnakeDirections.LeftDir;
                default:
                    return position;
            }
        }

        // HasCollided returns true if the head position overlaps with any childBody component positions
        public bool HasCollided()
        {
            if (childBody != null)
            {
                return childBody.HasCollided(curPos);
            }
            else
            {
                return false;
            }
        }

        // SetDirection updates the snake direction with a given value
        public void SetDirection(int newDirection)
        {
            switch (direction)
            {
                case 1: // Up
                    if (curPos + SnakeDirections.UpDir == lastPos)
                    {
                        return;
                    }
                    break;
                case 2: // Right
                    if (curPos + SnakeDirections.RightDir == lastPos)
                    {
                        return;
                    }
                    break;
                case 3: // Down
                    if (curPos + SnakeDirections.DownDir == lastPos)
                    {
                        return;
                    }
                    break;
                case 4: // Left
                    if (curPos + SnakeDirections.LeftDir == lastPos)
                    {
                        return;
                    }
                    break;
            }
            this.direction = newDirection;
        }
    }
}