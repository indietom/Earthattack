using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShmupSlut
{
    class boss:objects
    {
        // When the boss has died I use this variable to count until the level changes 
        public int nextLevelCount;
        // These variables are used to make the boss move in another direction
        public int switchDirCount;
        public int maxSwitchDirCount;
        public int fireRate;
        public int fireRate2;
        public int type;
        public int bleedCount;

        // Decides if the boss goes up or down
        public bool goUp;

        public Rectangle bossC;

        public boss(Vector2 pos2, int type2)
        {
            pos = pos2;
            type = type2;
            switch (type2)
            { 
                case 1:
                    hp = 16;
                    SetSize(124, 124);
                    SetSpriteCoords(Frame(18), Frame(8));
                    maxSwitchDirCount = 400;
                    direction = 3;
                    speed = 1;
                    break;
                case 2:
                    hp = 20;
                    SetSize(60, 50);
                    SetSpriteCoords(Frame(18), Frame(13));
                    maxSwitchDirCount = 400;
                    direction = 3;
                    speed = 4;
                    goUp = false;
                    break;
                case 3:
                    hp = 30;
                    currentFrame = 18;
                    SetSpriteCoords(Frame(14), Frame(currentFrame));
                    SetSize(99, 74);
                    speed = 2;
                    break;
            }
        }
        public void CheckHealth(List<projectile> projectiles, List<hitEffect> hitEffects, List<particle> particles, ref player player, ref int level)
        {
            Random random = new Random();
            if (player.playerC.Intersects(bossC) && player.vulnerable)
            {
                player.hp = 0;
            }
            switch (type)
            { 
                // Just so that the player knows that the boss is almost dead
                case 2:
                    if (hp <= 10)
                    {
                        bleedCount += 1;
                        if (bleedCount >= 16)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                particles.Add(new particle(new Vector2(pos.X + random.Next(width), pos.Y + random.Next(height)), 1, "smoke", random.Next(1, 5), 0.01f, random.Next(-180, -45)));
                                particles.Add(new particle(new Vector2(pos.X + random.Next(width), pos.Y + random.Next(height)), 1, "fire smoke", random.Next(1, 5), 0.01f, random.Next(-180, -45)));
                            }
                            bleedCount = 0;
                        }
                    }
                    break;
                case 3:
                    if (hp <= 15)
                    {
                        bleedCount += 1;
                        if (bleedCount >= 16)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                particles.Add(new particle(new Vector2(pos.X + 34, pos.Y + 53), 1, "smoke", random.Next(1, 5), 0.01f, random.Next(-100, -80)));
                                particles.Add(new particle(new Vector2(pos.X + 68, pos.Y + 53), 1, "fire smoke", random.Next(1, 5), 0.01f, random.Next(-100, -80)));
                            }
                            bleedCount = 0;
                        }
                    }
                    break;
            }
            foreach (projectile p in projectiles)
            {
                // So that the player knows that the boss has been hit
                if (p.projectileC.Intersects(bossC))
                {
                    for (int x = 0; x < width / 24; x++)
                    {
                        for (int y = 0; y < height / 24; y++)
                        {
                            hitEffects.Add(new hitEffect(new Vector2(pos.X+x*24, pos.Y+y*24)));
                        }
                    }
                    hp -= p.dm;
                    p.destroy = true;
                }
            }
            if (hp <= 0)
            {
                switch (type)
                { 
                    case 2:
                        imy = 377;
                        break;
                }
                // move it outside of the screen
                pos.X += 3 + speed;
                pos.Y += 3 + speed;
                // count until the next level starts
                nextLevelCount += 1;
            }
            // progress unto the next level
            if (nextLevelCount >= 128 * 2)
            {
                level += 1;
                destroy = true;
            }
        }
        public void Update()
        {
            switch(type)
            {
                case 1:
                    bossC = new Rectangle((int)pos.X + 30, (int)pos.Y + 41, 52, 53);
                    break;
                case 2:
                    bossC = new Rectangle((int)pos.X + 13, (int)pos.Y + 17, 20, 27);
                    break;
                case 3:
                    bossC = new Rectangle((int)pos.X + 7, (int)pos.Y + 1, 87, 54);
                    break;
            }
        }
        public void Attack(List<enemyProjectile> enemyProjectiles, player player)
        {
            // stop shooting if the player is dead
            if (player.hp <= 0 && player.respawnCutScene)
            {
                fireRate = 0;
            }
            Random random = new Random();
            switch (type)
            {
                case 1:
                    fireRate += 1;
                    if (fireRate >= 128 * 2 && hp >= 9 && hp >= 1)
                    {
                        enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + 18, pos.Y + 22), 3, 4, 0));
                        enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + 88, pos.Y + 22), 3, 4, 0));
                        fireRate = 0;
                    }
                    // when the boss has lost half of its life it will start shooting more often and a wall of bullets
                    if (fireRate >= 128 && hp <= 8 && hp >= 1)
                    {
                        enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + 18, pos.Y + 22), 3, 4, 0));
                        enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + 88, pos.Y + 22), 3, 4, 0));
                        for (int i = 0; i < 5; i++)
                        {
                            enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + 24 + i * 24, pos.Y + 22), 1, 7, -270));
                        }
                        fireRate = 0;
                    }
                    break;
                case 2:
                    if (hp >= 1)
                    {
                        fireRate += 1;
                        // burst fire
                        if (fireRate == 32 || fireRate == 40 || fireRate == 48)
                        {
                            if (direction == 3)
                            {
                                enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + 4, pos.Y + 47), 1, 8, -220));
                            }
                            if (direction == 4)
                            {
                                enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + 50, pos.Y + 47), 1, 8, -320));
                            }
                        }
                        // restart the firerate variable once the burst is over
                        if (fireRate >= 48 + 16)
                        {
                            fireRate = 0;
                        }
                        // shoot missiles
                        fireRate2 += 1;
                        if (fireRate2 >= 64)
                        {
                            if (direction == 3)
                            {
                                enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + 44, pos.Y + 33), 6, 8, -180));
                            }
                            if (direction == 4)
                            {
                                enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + 16, pos.Y + 33), 6, 8, 0));
                            }
                            fireRate2 = 0;
                        }
                    }
                    break;
                case 3:
                    if (hp >= 1)
                    {
                        fireRate += 1;
                        if (fireRate >= 64)
                        {
                            enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + 34, pos.Y + 57), 5, 7, random.Next(-290, -250)));
                            enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + 67, pos.Y + 57), 5, 7, random.Next(-290, -250)));
                            fireRate = 0;
                        }
                    }
                    break;
            }
        }
        public void Animation()
        {
            switch (type)
            {
                case 3:
                    animationCount += 1;
                    imy = Frame(currentFrame);
                    if (animationCount >= 5)
                    {
                        // Everyframe contains three normal sized frames so I move it 3 frames
                        currentFrame -= 3;
                        animationCount = 0;
                    }
                    // restart animation 
                    if (currentFrame <= 9)
                    {
                        currentFrame = 18;
                    }
                    break;
            }
        }
        public void Movment(player player)
        { 
            Random random = new Random();
            switch (type)
            { 
                case 1:
                    // raise the speed once the boss is almost dead
                    if (hp <= 8)
                    {
                        speed = 3;
                    }
                    if (pos.Y <= 50)
                    {
                        pos.Y += 1;
                    }
                    else
                    {
                        switch (direction)
                        {
                            case 1:
                                pos.Y += speed;
                                break;
                            case 2:
                                pos.Y -= speed;
                                break;
                            case 3:
                                pos.X -= speed;
                                break;
                            case 4:
                                pos.X += speed;
                                break;
                        }

                        // compnsate for the raised speed
                        if (hp >= 9)
                            switchDirCount += 1;
                        else
                            switchDirCount += 3;

                        if (direction == 3 && switchDirCount >= maxSwitchDirCount)
                        {
                            direction = 4;
                            switchDirCount = 0;
                        }
                        if (direction == 4 && switchDirCount >= maxSwitchDirCount)
                        {
                            direction = 3;
                            switchDirCount = 0;
                        }
                    }
                    break;
                case 2:
                    if (hp >= 1)
                    {
                        // if the player is close the boss will get more aggresive making it hard to hit and get passed 
                        if (DistanceTo(player.pos.X, player.pos.Y) <= 128)
                        {
                            speed = 10;
                        }
                        else
                        {
                            speed = 4;
                        }
                        // go up and down
                        if (pos.Y >= 480 - width)
                        {
                            goUp = true;
                        }
                        if (pos.Y <= 0)
                        {
                            goUp = false;
                        }
                        switchDirCount += (int)speed;
                        switch (direction)
                        {
                            case 3:
                                pos.X -= speed;
                                imx = Frame(18);
                                break;
                            case 4:
                                pos.X += speed;
                                imx = 512;
                                break;
                        }
                        if (switchDirCount >= maxSwitchDirCount)
                        {
                            if (goUp)
                            {
                                pos.Y -= height / 2;
                            }
                            else
                            {
                                pos.Y += height / 2;
                            }
                        }
                        if (direction == 3 && switchDirCount >= maxSwitchDirCount)
                        {
                            direction = 4;
                            switchDirCount = 0;
                        }
                        if (direction == 4 && switchDirCount >= maxSwitchDirCount)
                        {
                            direction = 3;
                            switchDirCount = 0;
                        }
                    }
                    break;
                case 3:
                    pos.Y += speed;
                    // reset the position so that it can attack again but only if it's alive
                    if (pos.Y >= 480 + height && hp >= 1)
                    {
                        pos = new Vector2(random.Next(640 - width), random.Next(-300, -200));
                    }
                    break;
            }
        }
    }
}
