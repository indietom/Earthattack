using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace ShmupSlut
{
    class objects
    {
        public Vector2 pos;

        //Make it easier to draw sprites
        public int imx;
        public int imy;
        public int width;
        public int height;
        public int hp;
        public int dm;
        public int direction;

        // make it easier to quickly make linear animations
        public int currentFrame;
        public int animationCount;

        // This way I can destroy an object in a list in the game1 class by writing destroy = true inside of the class, making game1 less cluterd  
        public bool destroy;

        // Variables used to move an object based on an angle
        public float angle2;
        public float angle;
        public float speed;
        public float scale_x;
        public float scale_y;
        public float veclocity_x;
        public float veclocity_y;

        // With this function I can get the distance between two objects. For example I used this to make one bossmore aggresive when the player comes close.
        public float DistanceTo(float x2, float y2)
        {
            return (float)Math.Sqrt((pos.X - x2) * (pos.X - x2) + (pos.Y - y2) * (pos.Y - y2));
        }

        // With this function I can move an object with an angel
        public void AngleMath()
        {
            // convert radian to degrees because it's eaiser to use degrees
            angle2 = (angle * (float)Math.PI / 180);
            scale_x = (float)Math.Cos(angle2);
            scale_y = (float)Math.Sin(angle2);
            veclocity_x = (speed * scale_x);
            veclocity_y = (speed * scale_y);
        }

        //This way I can make an object aim at a point and move thowards it
        public void MathAim(float x2, float y2)
        {
            angle = (float)Math.Atan2(y2 - pos.Y, x2 - pos.X);
            veclocity_x = (speed * (float)Math.Cos(angle));
            veclocity_y = (speed * (float)Math.Sin(angle));
        }

        // this makes it easy to pick a sprite without going into the spritesheet and zooming in to find the coordinats and it makes it easier to animate 
        public int Frame(int Frame2)
        {
            return Frame2 * 24 + Frame2 + 1;
        }

        public void CheckOnScreen()
        {
            if (pos.X >= 640 || pos.X <= 0 - width|| pos.Y >= 480 || pos.Y <= 0 - height)
            {
                destroy = true;
            }
        }
        // this way I only have to write two lines of code to give the object a sprite and size instead of four lines
        public void SetSpriteCoords(int imx2, int imy2)
        {
            imx = imx2;
            imy = imy2;
        }

        public void SetSize(int w2, int h2)
        {
            width = w2;
            height = h2;
        }
        // this way I don't have to fill out a bunch of parameters that are always the same
        public void DrawSprite(SpriteBatch spritebatch, Texture2D spritesheet)
        {
            spritebatch.Draw(spritesheet, pos, new Rectangle(imx, imy, width, height), Color.White);
        }
        public void DrawSprite(SpriteBatch spritebatch, Texture2D spritesheet, float size)
        {
            spritebatch.Draw(spritesheet, pos, new Rectangle(imx, imy, width, height), Color.White, angle, new Vector2(width / 2, height / 2), size, SpriteEffects.None, 0);
        }
    }
}
