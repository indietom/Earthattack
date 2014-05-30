using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShmupSlut
{
    class hitEffect:objects
    {
        public int lifeTime;
        public int type;

        public hitEffect(Vector2 pos2)
        {
            Random random = new Random();
            // chocse between one of the four sprites
            type = random.Next(1, 5);
            pos = pos2;
            SetSize(24, 24);
            switch (type)
            {
                case 1:
                    SetSpriteCoords(Frame(13), Frame(1));
                    break;
                case 2:
                    SetSpriteCoords(Frame(14), Frame(1));
                    break;
                case 3:
                    SetSpriteCoords(Frame(15), Frame(1));
                    break;
                case 4:
                    SetSpriteCoords(Frame(16), Frame(1));
                    break;
            }
        }
        public void Update()
        {
            // destroy after 5 ticks/frames
            lifeTime += 1;
            if (lifeTime >= 5)
            {
                destroy = true;
            }
        }
    }
}
