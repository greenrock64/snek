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
            switch (direction)
            {
                case 1: // Up
                    curPos += new Point(0, -1);
                    break;
                case 2: // Right
                    curPos += new Point(1, 0);
                    break;
                case 3: // Down
                    curPos += new Point(0, 1);
                    break;
                case 4: // Left
                    curPos += new Point(-1, 0);
                    break;
            }
            if (childBody != null)
            {
                childBody.updatePosition();
            }
        }

        public void setDirection(int newDirection)
        {
            this.direction = newDirection;
        }
    }
}
