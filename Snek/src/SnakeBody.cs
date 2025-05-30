﻿using System;
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

        // X/Y Coordinates for directions
        public static Point UpDir = new Point(0, -1);
        public static Point RightDir = new Point(1, 0);
        public static Point DownDir = new Point(0, 1);
        public static Point LeftDir = new Point(-1, 0);
    }
    
    class SnakeBody
    {
        // Position
        public Point curPos;
        public Point lastPos;
        // Connected bodies
        public SnakeBody parentBody;
        public SnakeBody childBody;

        public SnakeBody() {}

        public SnakeBody(SnakeBody parentBody)
        {
            // Body part
            this.parentBody = parentBody;
            this.curPos = parentBody.curPos;
        }

        public void EatPill()
        {
            if (childBody != null)
            {
                childBody.EatPill();
            }
            else
            {
                childBody = new SnakeBody(this);
            }
        }

        // Calculate a new position based on the parent SnakeBody position
        public void UpdatePosition() {
            lastPos = curPos;
            curPos = parentBody.lastPos;
            if (childBody != null)
            {
                childBody.UpdatePosition();
            }
        }
        public bool HasCollided(Point headPos)
        {
            if (headPos == curPos)
            {
                return true;
            } 
            else if (childBody != null) {
                return childBody.HasCollided(headPos);
            }
            else
            {
                return false;
            }
        }
    }
}
