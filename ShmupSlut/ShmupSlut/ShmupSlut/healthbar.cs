using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShmupSlut
{
    class healthbar:objects
    {
        public healthbar()
        {
            SetSize(10, 100);
            SetSpriteCoords(1, 376);
            pos = new Vector2(0, 0);
        }

        public void Update(player player)
        {
            // calcutlets how long the healthbar should be based on the player's hp
           height = player.hp * 10;
        }
    }
}
