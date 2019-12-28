using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snek
{
    class SnakeHead : SnakeBody
    {
        int direction;

        public SnakeHead(Texture2D bodyTexture)
        {
            this.bodyTexture = bodyTexture;
            curPos = new Point(0,0);
            direction = SnakeDirections.Right;
        }
        new public void updatePosition() {
            lastPos = curPos;
            switch (direction)
            {
                case 1: // Up
                    curPos += SnakeDirections.UpDir;
                    break;
                case 2: // Right
                    curPos += SnakeDirections.RightDir;
                    break;
                case 3: // Down
                    curPos += SnakeDirections.DownDir;
                    break;
                case 4: // Left
                    curPos += SnakeDirections.LeftDir;
                    break;
            }
            if (childBody != null)
            {
                childBody.updatePosition();
            }
        }

        public bool hasCollided()
        {
            if (childBody != null)
            {
                return childBody.hasCollided(curPos);
            }
            else
            {
                return false;
            }
        }

        public void setDirection(int newDirection)
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