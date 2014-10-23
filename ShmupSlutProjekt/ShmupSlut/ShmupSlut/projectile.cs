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
    class projectile:objects
    {
        public int type;
        public int smokeEffectCount;

        public float accel;

        public Rectangle projectileC;

        // I can chose the angle in the constructor, it makes the flamethrower and shotgun easier to make 
        public projectile(int type2, Vector2 pos2, float ang2)
        {
            Random random = new Random();
            pos = pos2;
            type = type2;
            angle = ang2;
            switch (type)
            {
                case 1:
                    speed = 7;
                    SetSize(6, 11);
                    SetSpriteCoords(Frame(5), 1);
                    dm = 1;
                    break;
                case 2:
                    // I set it to -1 so that it looks that the missile is dropped before the jet engine starts
                    accel = -1;
                    SetSize(5, 13);
                    SetSpriteCoords(106, 26);
                    dm = 3;
                    break;
                case 3:
                    speed = 10;
                    SetSize(6, 10);
                    SetSpriteCoords(Frame(5), Frame(1));
                    dm = 4;
                    break;
                case 4:
                    speed = random.Next(3, 6);
                    SetSize(16, 16);
                    SetSpriteCoords(Frame(4), Frame(0));
                    dm = 1;
                    break;
                case 5:
                    speed = 5;
                    SetSize(11, 15);
                    SetSpriteCoords(Frame(6), Frame(0));
                    dm = 3;
                    break;
            }
        }
        public void Update(List<particle> particles)
        {
            Random random = new Random();
            projectileC = new Rectangle((int)pos.X, (int)pos.Y, width, height);
            switch (type)
            { 
                case 2:
                    // spawns smoke clouds every 5:th tick
                    smokeEffectCount += 1;
                    if (smokeEffectCount >= 5)
                    { 
                        particles.Add(new particle(new Vector2(pos.X, pos.Y+6), 2, "smoke", random.Next(5, 9), 0.01f, angle));
                        smokeEffectCount = 0;
                    }
                    break;
            }
        }
        public void Movment()
        {
            switch (type)
            { 
                case 1:
                    AngleMath();
                    pos.X += veclocity_x;
                    pos.Y += veclocity_y;
                    break;

                case 2:
                    AngleMath();
                    // gains acceleration 
                    accel += 0.1f;
                    speed = accel;
                    pos.X += veclocity_x;
                    pos.Y += veclocity_y;
                    break;
                case 3:
                    AngleMath();
                    pos.X += veclocity_x;
                    pos.Y += veclocity_y;
                    break;
                case 4:
                    AngleMath();
                    pos.X += veclocity_x;
                    pos.Y += veclocity_y;
                    break;
                case 5:
                    AngleMath();
                    pos.X += veclocity_x;
                    pos.Y += veclocity_y;
                    break;
            }
            CheckOnScreen();
        }
    }
}









