using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShmupSlut
{
    class backgroundObject:objects
    {
        public string type;
        // the foreground objects are often large and to save space on the spritesheet I zoom in instead
        public float zoom;

        public backgroundObject(Vector2 pos2, int imx2, int imy2, int wi, int hi, float spe, string type2)
        {
            Random random = new Random();
            type = type2;
            if (type == "fore")
            {
                zoom = random.Next(2, 6);
                angle = random.Next(360);
            }
            pos = pos2;
            SetSpriteCoords(imx2, imy2);
            SetSize(wi, hi);
            speed = spe;
        }
        public void Movment()
        {
            pos.Y += speed;
            // the foreground objects are more often large so they need more space before being destroied
            if (pos.Y >= 800 && type == "fore")
            {
                destroy = true;
            }
            if (pos.Y >= 480 + 24 && type != "fore")
            {
                destroy = true;
            }
        }
    }
}
