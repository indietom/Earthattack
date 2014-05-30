using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace ShmupSlut
{
    class explosion:objects
    {
        public explosion(Vector2 pos2)
        {
            pos = pos2;
            SetSpriteCoords(Frame(currentFrame), Frame(13));
            SetSize(24, 24);
        }
        public void Animation(SoundEffect explosionSfx)
        {
            imx = Frame(currentFrame);
            animationCount += 1;
            // every 2:nd tick the animation frame moves one step
            if (animationCount >= 2)
            {
                currentFrame += 1;
                animationCount = 0;
            }
            // play the sound effect in the middle of the animation because then
            if (currentFrame == 4)
            {
                explosionSfx.Play();
            }
            // destroy the explosion once the animation is done
            if (currentFrame == 8)
            {
                destroy = true;
            }
        }
    }
}
