using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snek
{
    class SnakeDirections
    {
        // Directional constants
        public const int Up = 1;
        public const int Right = 2;
        public const int Down = 3;
        public const int Left = 4;
    }
    
    class SnakeBody
    {
        // Texture
        public Texture2D bodyTexture {get;set;}
        // Position
        public Point curPos;
        public Point lastPos;
        // Connected bodies
        public SnakeBody parentBody;
        public SnakeBody childBody;

        public SnakeBody() {}

        public SnakeBody(SnakeBody parentBody, Texture2D bodyTexture)
        {
            // Body part
            this.parentBody = parentBody;
            this.curPos = parentBody.curPos;
            this.bodyTexture = bodyTexture;
        }

        public void eatPill()
        {
            if (childBody != null)
            {
                childBody.eatPill();
            }
            else
            {
                childBody = new SnakeBody(this, this.bodyTexture);
            }
        }

        // Calculate a new position based on the parent SnakeBody position
        public void updatePosition() {
            lastPos = curPos;
            curPos = parentBody.lastPos;
            if (childBody != null)
            {
                childBody.updatePosition();
            }
        }
    }
}
