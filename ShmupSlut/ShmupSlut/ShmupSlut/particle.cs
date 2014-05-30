using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace ShmupSlut
{
    class particle:objects
    {
        public int type;
        public float accel;
        public float deccre;

        // this constructor's main use is to makes a cololerd rectangle  
        public particle(Vector2 pos2, int type2, string color, float accel2, float deccre2, float ang, int size)
        {
            type = type2;
            pos = pos2;
            angle = ang;
            accel = accel2;
            deccre = deccre2;
            SetSize(size, size);
            switch (color)
            {
                case "red":
                    SetSpriteCoords(Frame(12), 1);
                    break;
                case "grey":
                    SetSpriteCoords(Frame(12), 6);
                    break;
                case "smoke":
                    SetSpriteCoords(Frame(12), 11);
                    break;
                case "fire smoke":
                    SetSpriteCoords(Frame(12), 16);
                    break;
                case "muzzle flash":
                    break;
            }
        }
        // this constructor's main use is to creats a sprite
        public particle(Vector2 pos2, int type2, string color, float accel2, float deccre2, float ang)
        {
            type = type2;
            pos = pos2;
            angle = ang;
            accel = accel2;
            deccre = deccre2;
            switch (color)
            {
                case "red":
                    SetSpriteCoords(Frame(12), 1);
                    break;
                case "grey":
                    SetSpriteCoords(Frame(12), 6);
                    break;
                case "smoke":
                    SetSpriteCoords(Frame(12), 11);
                    SetSize(4, 5);
                    break;
                case "fire smoke":
                    SetSpriteCoords(Frame(12), 16);
                    SetSize(4, 5);
                    break;
                case "muzzle flash":
                    SetSpriteCoords(306, 1);
                    SetSize(6, 5);
                    break;
            }
        }
        public void Movemnt()
        {
            switch (type)
            {
                case 1:
                    AngleMath();
                    speed = accel;
                    // slow the particle down
                    accel -= deccre;
                    pos.X += veclocity_x;
                    pos.Y += veclocity_y; 
                    // destory the particle ones it has stopped 
                    if (accel <= 0)
                    {
                        destroy = true;
                    }
                    break;
                case 2:
                    AngleMath();
                    speed = accel;
                    // slow the particle down
                    accel -= deccre;
                    pos.X -= veclocity_x;
                    pos.Y -= veclocity_y;
                    // destory the particle ones it has stopped 
                    if (accel <= 0)
                    {
                        destroy = true;
                    }
                    break;
            }
            CheckOnScreen();
        }
    }
}
