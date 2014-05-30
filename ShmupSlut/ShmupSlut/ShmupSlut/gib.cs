using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShmupSlut
{
    class gib:objects
    {
        public int graphicsType;

        // with this variable I can decide if I want to spawn metalic debry or guts
        public string type;

        public float accel;

        public float rotation;

        public gib(Vector2 pos2, float spe, float ang, string type2, int graphicsType2)
        {
            // set the position, speed, angle and type 
            pos = pos2;
            accel = spe;
            type = type2;
            angle = ang;
            graphicsType = graphicsType2;
            // Set the sprite based on the graphics type
            switch (graphicsType)
            { 
                case 1:
                    SetSpriteCoords(326, 1);
                    SetSize(5, 6);
                    break;
                case 2:
                    SetSpriteCoords(337, 1);
                    SetSize(6, 3);
                    break;
                case 3:
                    SetSpriteCoords(347, 1);
                    SetSize(3, 4);
                    break;
                case 4:
                    SetSpriteCoords(326, 8);
                    SetSize(5, 6);
                    break;
                case 5:
                    SetSpriteCoords(326, 15);
                    SetSize(8, 5);
                    break;
                case 6:
                    SetSpriteCoords(336, 19);
                    SetSize(5, 6);
                    break;
                case 7:
                    SetSpriteCoords(337, 8);
                    SetSize(5, 10);
                    break;
                case 8:
                    SetSpriteCoords(345, 6);
                    SetSize(5, 7);
                    break;
                case 9:
                    SetSpriteCoords(345, 15);
                    SetSize(5, 7);
                    break;
                case 10:
                    SetSpriteCoords(351, 1);
                    SetSize(4, 5);
                    break;
                case 11:
                    SetSpriteCoords(351, 8);
                    SetSize(4, 3);
                    break;
                case 12:
                    SetSpriteCoords(351, 13);
                    SetSize(4, 4);
                    break;
                case 13:
                    SetSpriteCoords(351, 19);
                    SetSize(6, 5);
                    break;
                case 14:
                    SetSpriteCoords(358, 20);
                    SetSize(8, 5);
                    break;
                case 15:
                    SetSpriteCoords(359, 13);
                    SetSize(3, 3);
                    break;
                case 16:
                    SetSpriteCoords(360, 6);
                    SetSize(5, 5);
                    break;
                case 17:
                    SetSpriteCoords(361, 1);
                    SetSize(4, 3);
                    break;
                case 18:
                    SetSpriteCoords(371, 1);
                    SetSize(4, 7);
                    break;
                case 19:
                    SetSpriteCoords(369, 10);
                    SetSpriteCoords(6, 8);
                    break;
                case 20:
                    SetSpriteCoords(368, 19);
                    SetSize(5, 5);
                    break;
            }
        }
        public void Movment()
        {
            AngleMath();
            pos.X += veclocity_x;
            pos.Y += veclocity_y;
            speed = accel;
            // The gib spins faster the faster it's moving
            rotation += 0.1f * speed * 1;
            CheckOnScreen();
        }
    }
}
