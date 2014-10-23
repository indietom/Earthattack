using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace ShmupSlut
{
    class player:objects
    {
        // I want the abilty make the player stop
        public bool inputActive;
        public bool respawnCutScene;

        // Keeps track of what gun the player is using
        public int gunType;
        // Removes the power up the player has obtained after a while
        public int powerDownCount;
        // regulates how often the player shoots, I don't want a beam of bullets
        public int fireRate;
        public int score;
        public int lives;
        // I wanted a cool cutscene when the player respawns so these two variables keeps track on what happends 
        public int respawnDelay;
        public int addHealthDelay;
        // when the players get below 3 hp left the player "bleeds" and to not make it a beam of smoke clouds I added a delay
        public int bleedDelay;
        public int kills;
        public int totalKills;
        // Keeps track of what special power up you have, how often you can shoot and how much of you have left
        public int specialType;
        public int specialRate;
        public int specialAmmo;

        public bool vulnerable;

        public Rectangle playerC;

        KeyboardState keyboard;
        GamePadState gamepad;

        public player()
        {
            vulnerable = true;
            SetSize(24, 24);
            pos = new Vector2(320, 240);
            gunType = 1;
            inputActive = true;
            hp = 10;
            lives = 10;
            score = 0;
            powerDownCount = 128 * 10;
        }

        public void CheckHealth(healthbar healthbar, List<particle> particles)
        {
            Random random = new Random();
            // spawn smoke clouds to singal that the player is on the brink of death 
            if (hp <= 3)
            {
                bleedDelay += 1;
                if (bleedDelay >= 5)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        particles.Add(new particle(new Vector2(pos.X + 12, pos.Y + 12 - 5), 1, "smoke", random.Next(3, 5), 0.01f, random.Next(-300, -250)));
                        particles.Add(new particle(new Vector2(pos.X + 12, pos.Y + 12 - 5), 1, "fire smoke", random.Next(3, 5), 0.01f, random.Next(-300, -250)));
                    }
                    bleedDelay = 0;
                }
            }

            if (hp < 0)
            {
                hp = 0;
            }
            if (hp > 10)
            {
                hp = 10;
            }
            if (hp <= 0)
            {
                gunType = 1;
                powerDownCount = 128 * 10;
                imx = 1;
                // the respawn cutscene starts whn the player gets outside of the screen
                if (pos.X > 640 || pos.Y > 480)
                {
                    vulnerable = false;
                    if (respawnDelay <= 0)
                    {
                        lives -= 1;
                    }
                    respawnDelay = 1;
                    pos.Y = 480;
                }
                else
                {
                    // push out the player if it dead
                    imx = Frame(2);
                    pos.X += 3;
                    pos.Y += 3;
                }
                inputActive = false;
            }
            if (respawnCutScene)
            {
                bleedDelay = 0;
                imx = 1;
                pos.Y -= 1;
                // add life slowly
                if (hp <= 9)
                {
                    addHealthDelay += 1;
                    if (addHealthDelay >= 16)
                    {
                        hp += 1;
                        addHealthDelay = 0;
                    }
                }
                if (pos.Y <= 240)
                {
                    // restart the player
                    vulnerable = true;
                    inputActive = true;
                    respawnDelay = 0;
                    powerDownCount = 128 * 10;
                    respawnCutScene = false;
                }
            }
            if (respawnDelay >= 1)
            {
                pos.X = 320;
                respawnDelay += 1;
                respawnCutScene = true;
            }
        }

        public void Update()
        {
            playerC = new Rectangle((int)pos.X, (int)pos.Y, 24, 24);
            // Stops the player from shooting for a while after every shot
            if (fireRate >= 1)
            {
                fireRate += 1;
                if (fireRate >= 64+32 && gunType == 4)
                {
                    fireRate = 0;
                }
                if (fireRate >= 32 && gunType != 4)
                {
                    fireRate = 0;
                }
            }
            // if the player loses all special ammo the special power is removed
            if (specialAmmo <= 0)
            {
                specialType = 0;
            }
            // Stops the player from shooting for a while
            if (specialRate >= 1)
            {
                specialRate += 1;
                if (specialRate >= 64)
                {
                    specialRate = 0;
                }
            }
            // if the player has a power up the power down variable counts down to zero and if it hits zero the power up is removed
            if (gunType != 1)
            {
                powerDownCount -= 1;
                if (powerDownCount <= 0)
                {
                    gunType = 1;
                    powerDownCount = 128 * 10;
                }
            }
        }

        public void Input(List<projectile> projectiles, List<particle> particles, SoundEffect simpleShotSfx, SoundEffect laserShotSfx)
        {
            Random random = new Random();
            KeyboardState prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            GamePadState prevGamepad = gamepad;
            gamepad = GamePad.GetState(PlayerIndex.One);

            if (inputActive)
            {
                if (keyboard.IsKeyDown(Keys.LeftShift) && prevKeyboard.IsKeyUp(Keys.LeftShift) || gamepad.Buttons.B == ButtonState.Pressed && prevGamepad.Buttons.B == ButtonState.Released)
                {
                    // spawns a wall of flames
                    if (specialType == 1 && specialRate <= 0 && specialAmmo >= 1)
                    {
                        for (int i = 0; i < 640 / 16; i++)
                        {
                            projectiles.Add(new projectile(4, new Vector2(16 * i, 480), -90));
                        }
                        specialAmmo -= 1;
                        specialRate = 1;
                    }
                    // spawns a circle of fire, I wrote "i+=29" to make less fireballs in the circle.
                    if (specialType == 2 && specialRate <= 0 && specialAmmo >= 1)
                    {
                        for (int i = 0; i < 360; i+=29)
                        {
                            projectiles.Add(new projectile(4, new Vector2(pos.X + (float)Math.Cos(i) * 24, pos.Y + (float)Math.Sin(i) * 24), -90));
                        }
                        specialAmmo -= 1;
                        specialRate = 1;
                    }
                }
                // I made two shooting if statments because with some type of guns you have to tap the key and some are automatic
                if (keyboard.IsKeyDown(Keys.Space) || gamepad.Buttons.A == ButtonState.Pressed)
                {
                    if (gunType == 5 && fireRate <= 0)
                    {
                        projectiles.Add(new projectile(4, new Vector2(pos.X + 5, pos.Y + 12), random.Next(-125,-45)));
                        fireRate = 25;
                    }
                }
                if (keyboard.IsKeyDown(Keys.Space) && prevKeyboard.IsKeyUp(Keys.Space) || gamepad.Buttons.A == ButtonState.Pressed && prevGamepad.Buttons.A == ButtonState.Released)
                {
                    if (gunType == 1 && fireRate <= 0)
                    {
                        projectiles.Add(new projectile(1, new Vector2(pos.X + 8, pos.Y + 12), -90));
                        simpleShotSfx.Play();
                        fireRate = 1;
                    }
                    if (gunType == 2 && fireRate <= 0)
                    {
                        projectiles.Add(new projectile(1, new Vector2(pos.X + 8, pos.Y + 12), -80));
                        projectiles.Add(new projectile(1, new Vector2(pos.X + 8, pos.Y + 12), -90));
                        projectiles.Add(new projectile(1, new Vector2(pos.X + 8, pos.Y + 12), -100));
                        simpleShotSfx.Play();
                        fireRate = 1;
                    }
                    if (gunType == 3 && fireRate <= 0)
                    {
                        projectiles.Add(new projectile(1, new Vector2(pos.X + 8, pos.Y + 12), -90));
                        simpleShotSfx.Play();
                        fireRate = 1;
                    }
                    if (gunType == 4 && fireRate <= 0)
                    {
                        projectiles.Add(new projectile(2, new Vector2(pos.X + 8, pos.Y + 6), -90));
                        fireRate = 1;
                    }
                    if (gunType == 6 && fireRate <= 0)
                    {
                        projectiles.Add(new projectile(3, new Vector2(pos.X + 8, pos.Y + 6), -90));
                        fireRate = 1;
                        laserShotSfx.Play();
                    }
                    if (gunType == 7 && fireRate <= 0)
                    {
                        projectiles.Add(new projectile(5, new Vector2(pos.X + 8, pos.Y + 6), -90));
                        fireRate = 1;
                    }
                }
                // The machine gun shoots in bursts
                if (gunType == 3 && fireRate == 7 || gunType == 3 && fireRate == 14)
                {
                    projectiles.Add(new projectile(1, new Vector2(pos.X + 8, pos.Y + 12), -90));
                    simpleShotSfx.Play();
                }
                if (keyboard.IsKeyDown(Keys.Left) || keyboard.IsKeyDown(Keys.A) || gamepad.ThumbSticks.Left.X <= -0.5f)
                {
                    if(pos.X >= 0)
                    {
                        pos.X -= 3;
                    }
                    // Change the sprite to a left turning sprite
                    imx = Frame(1);
                }
                if (keyboard.IsKeyDown(Keys.Right) || keyboard.IsKeyDown(Keys.D) || gamepad.ThumbSticks.Left.X >= 0.5f)
                {
                    if (pos.X <= 640 - 26)
                    {
                        pos.X += 3;
                    }
                    // Change the sprite to a right turning sprite
                    imx = Frame(2);
                }
                // if the player is not moving left or right the sprite is returnd to an idle ship 
                if (keyboard.IsKeyUp(Keys.Left) && keyboard.IsKeyUp(Keys.Right) && keyboard.IsKeyUp(Keys.A) && keyboard.IsKeyUp(Keys.D) && gamepad.ThumbSticks.Left.X == 0)
                {
                    imx = 1;
                }
                if (keyboard.IsKeyDown(Keys.Up) || keyboard.IsKeyDown(Keys.W) || gamepad.ThumbSticks.Left.Y >= 0.5f)
                {
                    if (pos.Y >= 0)
                    {
                        pos.Y -= 3;
                    }
                }
                if (keyboard.IsKeyDown(Keys.Down) || keyboard.IsKeyDown(Keys.S) || gamepad.ThumbSticks.Left.Y <= -0.5f)
                {
                    if (pos.Y <= 480 - 26)
                    {
                        pos.Y += 3;
                    }
                }
                if (keyboard.IsKeyDown(Keys.Left) && keyboard.IsKeyDown(Keys.Right) || keyboard.IsKeyDown(Keys.A) && keyboard.IsKeyDown(Keys.D))
                {
                    imx = 1;
                }
            }
        }
    }
}
