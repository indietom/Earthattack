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
    class menu:objects
    {
        KeyboardState keyboard;
        KeyboardState prevKeyboard;
        GamePadState gamepad;

        string[] options;

        public int selected;

        public menu()
        {
            options = new string[3]{"Start", "How To", "Quit"};
            selected = 1;
        }

        public void Update()
        {
            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            GamePadState prevGamepad = gamepad;
            gamepad = GamePad.GetState(PlayerIndex.One);
            // Move up and down in the menu's available options
            if (keyboard.IsKeyDown(Keys.Down) && prevKeyboard.IsKeyUp(Keys.Down) || keyboard.IsKeyDown(Keys.S) && prevKeyboard.IsKeyUp(Keys.S) || gamepad.ThumbSticks.Left.Y == -1 && prevGamepad.ThumbSticks.Left.Y != -1)
            {
                selected += 1;
            }
            if (keyboard.IsKeyDown(Keys.Up) && prevKeyboard.IsKeyUp(Keys.Up) || keyboard.IsKeyDown(Keys.W) && prevKeyboard.IsKeyUp(Keys.W) || gamepad.ThumbSticks.Left.Y == 1 && prevGamepad.ThumbSticks.Left.Y != 1)
            {
                selected -= 1;
            }
            // Keeps selected inside of the menu's available options
            if (selected >= 4)
            {
                selected = 1;
            }
            if (selected <= 0)
            {
                selected = 3;
            }
        }
        public void Draw(SpriteBatch spriteBatch, Texture2D spritesheet, SpriteFont font, bool startedGame, Texture2D menuScreen)
        {
            spriteBatch.Draw(menuScreen, new Vector2(0, 0), Color.White);
            for (int i = 0; i < 3; i++)
            {
                spriteBatch.DrawString(font, options[i], new Vector2(320 - 14 * 4, 240 + 15 * i), Color.White);
            }
            // change the way the selected option looks so that the player knowns it is selected
            if (selected == 1)
            {
                if (!startedGame)
                {
                    options[0] = "   > Start&";
                }
                else
                {
                    options[0] = "   > Resume&";
                }
            }
            else
            {
                if (!startedGame)
                {
                    options[0] = "Start";
                }
                else
                {
                    options[0] = "Resume";
                }
            }
            if (selected == 2)
            {
                options[1] = "   > How To&";
            }
            else
            {
                options[1] = "How To";
            }
            if (selected == 3)
            {
                options[2] = "   > Quit&";
            }
            else
            {
                options[2] = "Quit";
            }
        }
    }
}
