using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShmupSlut
{
    class enemy:objects
    {
        // what behvior it has
        public int aiType;
        // what sprite it has
        public int graphicsType;
        public int fireRate;
        // one of the enemis move to a ranom position which is set by the shootingPos and then it moves left and right while shooting
        public int shootingPos;

        // so that the game knows what gibs to spawn
        public string material;

        public bool vulnerable;
        // so that the game knows what drawSprite version it needs to use and how to place the hitbox
        public bool rotated;

        public Rectangle enemyC;

        public enemy(Vector2 pos2, int aiType2, int graphicsType2, float spe)
        {
            Random random = new Random();
            speed = spe;
            pos = pos2;
            aiType = aiType2;
            graphicsType = graphicsType2;
            switch (graphicsType)
            {
                case 1:
                    currentFrame = 1;
                    SetSpriteCoords(Frame(currentFrame), Frame(10));
                    SetSize(24, 24);
                    material = "organic";
                    break;
                case 2:
                    SetSpriteCoords(1, Frame(10));
                    SetSize(24, 24);
                    material = "organic";
                    break;
                case 3:
                    SetSpriteCoords(currentFrame, Frame(11));
                    SetSize(24, 24);
                    material = "metal";
                    break;
                case 4:
                    SetSpriteCoords(Frame(3), Frame(11));
                    SetSize(24, 24);
                    material = "organic";
                    break;
                case 5:
                    SetSpriteCoords(currentFrame, Frame(11));
                    SetSize(24, 24);
                    currentFrame = 4;
                    material = "metal";
                    break;
                case 6:
                    SetSpriteCoords(currentFrame, Frame(11));
                    SetSize(24, 24);
                    currentFrame = 7;
                    material = "metal";
                    break;
                case 7:
                    SetSpriteCoords(currentFrame, Frame(12));
                    SetSize(24, 24);
                    material = "organic";
                    direction = 3;
                    break;
                case 8:
                    SetSpriteCoords(currentFrame, Frame(12));
                    SetSize(24, 24);
                    material = "organic";
                    currentFrame = 3;
                    direction = 4;
                    break;
                case 9:
                    SetSpriteCoords(Frame(1), Frame(9));
                    SetSize(24, 24);
                    material = "metal";
                    break;
                case 10:
                    SetSpriteCoords(1, Frame(9));
                    SetSize(24, 24);
                    material = "metal";
                    break;
                case 11:
                    SetSpriteCoords(Frame(currentFrame), Frame(7));
                    SetSize(24, 24);
                    currentFrame = 0;
                    material = "metal";
                    break;
                case 12:
                    SetSpriteCoords(Frame(6), Frame(7));
                    SetSize(24, 24);
                    material = "metal";
                    break;
                case 13:
                    SetSpriteCoords(Frame(7), Frame(7));
                    SetSize(24, 24);
                    material = "metal";
                    break;
                case 14:
                    SetSpriteCoords(Frame(6), Frame(6));
                    SetSize(24, 24);
                    material = "metal";
                    break;
            }
            switch (aiType)
            { 
                case 1:
                    vulnerable = true;
                    hp = 1;
                    break;
                case 2:
                    vulnerable = true;
                    hp = 2;
                    fireRate = 60;
                    break;
                case 3:
                    rotated = true;
                    vulnerable = true;
                    hp = 2;
                    break;
                case 4:
                    // set the shooting position
                    shootingPos = random.Next(50, 300);
                    vulnerable = true;
                    hp = 5;
                    break;
                case 5:
                    hp = 2;
                    vulnerable = true;
                    break;
                case 6:
                    hp = 1;
                    vulnerable = true;
                    break;
            }
        }
        public enemy(Vector2 pos2, int aiType2, int graphicsType2, float spe, float ang)
        {
            Random random = new Random();
            speed = spe;
            pos = pos2;
            aiType = aiType2;
            graphicsType = graphicsType2;
            angle = ang;
            switch (graphicsType)
            {
                case 1:
                    currentFrame = 1;
                    SetSpriteCoords(Frame(currentFrame), Frame(10));
                    SetSize(24, 24);
                    material = "organic";
                    break;
                case 2:
                    SetSpriteCoords(1, Frame(10));
                    SetSize(24, 24);
                    material = "organic";
                    break;
                case 3:
                    SetSpriteCoords(currentFrame, Frame(11));
                    SetSize(24, 24);
                    material = "metal";
                    break;
                case 4:
                    SetSpriteCoords(Frame(3), Frame(11));
                    SetSize(24, 24);
                    material = "organic";
                    break;
                case 5:
                    SetSpriteCoords(currentFrame, Frame(11));
                    SetSize(24, 24);
                    currentFrame = 4;
                    material = "metal";
                    break;
                case 6:
                    SetSpriteCoords(currentFrame, Frame(11));
                    SetSize(24, 24);
                    currentFrame = 7;
                    material = "metal";
                    break;
            }
            switch (aiType)
            {
                case 1:
                    vulnerable = true;
                    hp = 1;
                    break;
                case 2:
                    vulnerable = true;
                    hp = 2;
                    break;
                case 3:
                    rotated = true;
                    break;
                case 4:
                    shootingPos = random.Next(50, 300);
                    vulnerable = true;
                    hp = 5;
                    break;
            }
        }
        public void Movment(player player)
        {
            switch (aiType)
            { 
                case 1:
                    pos.Y += speed;
                    break;
                case 2:
                    pos.Y += speed;
                    break;
                case 3:
                    MathAim(player.pos.X, player.pos.Y);
                    pos.Y += speed;
                    break;
                case 4:
                    // move to the shooting postion
                    if (pos.Y <= shootingPos)
                    {
                        pos.Y += 2;
                    }
                    // start moving left and right
                    if (direction == 3)
                    {
                        pos.X -= 1;
                        if (pos.X <= 48)
                        {
                            pos.X += 5;
                            direction = 4;
                        }
                    }
                    else
                    {
                        pos.X += 1;
                        if (pos.X >= 640-24-48)
                        {
                            pos.X -= 5;
                            direction = 3;
                        }
                    }
                    break;
                case 5:
                    pos.Y += speed;
                    break;
                case 6:
                    pos.Y += speed;
                    break;
            }
        }
        public void CheckHealth(List<projectile> projectiles, List<explosion> explosions, List<hitEffect> hitEffects, List<gib> gibs, ref player player)
        {
            Random random = new Random();
            if (!rotated)
            {
                enemyC = new Rectangle((int)pos.X + 6, (int)pos.Y + 6, width - 6, 24 - 6);
            }
            else
            {
                enemyC = new Rectangle((int)pos.X + 6 - width/2-6, (int)pos.Y + 6 - 9, width - 6, 24 - 6);
            }
            if (player.playerC.Intersects(enemyC))
            {
                if (player.vulnerable)
                {
                    player.hp -= 2;
                    hp = 0;
                }
                else
                {
                    hp = 0;
                }
            }
            if (pos.Y > 600)
            {
                destroy = true;
            }
            foreach (projectile p in projectiles)
            {
                if (p.projectileC.Intersects(enemyC) && vulnerable)
                {
                    hp -= p.dm;
                    if (hp >= 1)
                    {
                        if (!rotated)
                        {
                            hitEffects.Add(new hitEffect(pos));
                        }
                        else
                        {
                            hitEffects.Add(new hitEffect(new Vector2(pos.X-width/2, pos.Y-height/2)));
                        }
                    }
                    p.destroy = true;
                }
            }
            if (hp <= 0 && vulnerable)
            {
                explosions.Add(new explosion(pos));
                player.totalKills += 1;
                player.kills += 1;
                for (int i = 0; i < 20; i++)
                {
                    if (material == "organic")
                    {
                        gibs.Add(new gib(new Vector2(pos.X + 12, pos.Y + 12), random.Next(1, 7), random.Next(360), material, random.Next(10, 21)));
                    }
                    if (material == "metal")
                    {
                        gibs.Add(new gib(new Vector2(pos.X + 12, pos.Y + 12), random.Next(1, 7), random.Next(360), material, random.Next(1, 10)));
                    }
                }
                switch (aiType)
                { 
                    case 1:
                        player.score += 1000;
                        break;
                    case 2:
                        player.score += 3000;
                        break;
                    case 3:
                        player.score += 4000;
                        break;
                    case 4:
                        player.score += 6000;
                        break;
                    case 5:
                        player.score += 7000;
                        break;
                    case 6:
                        player.score += 10000;
                        break;
                }
                destroy = true;
            }
        }
        public void Attack(List<enemyProjectile> enemyProjectiles, player player)
        {
            Random random = new Random();
            // if the player is dead the enemis stop shooting so that the player has a chance to survive once the player has respawnd
            if (player.respawnCutScene)
            {
                fireRate = 0;
            }
            switch (aiType)
            { 
                case 2:
                    fireRate += 1;
                    if (fireRate == 64 + 16 || fireRate == 64 + 32 || fireRate == 64 + 48)
                    {
                        enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X+width/2-2, pos.Y+height/2-2), 1, 8, -270));
                    }
                    if (fireRate >= 128 + 64)
                    {
                        fireRate = random.Next(-64, 64+32);
                    }
                    break;
                case 3:
                    fireRate += 1;
                    if (fireRate == 64 + 16 || fireRate == 64 + 32)
                    {
                        enemyProjectiles.Add(new enemyProjectile(pos, 4, 8, player));
                    }
                    if (fireRate >= 128)
                    {
                        fireRate = random.Next(-64, 63);
                    }
                    break;
                case 4:
                    fireRate += 1;
                    if (fireRate >= 64)
                    {
                        enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + width / 2 + 3, pos.Y + height / 2 - 2), 2, 5, -270));
                        enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + width / 2 - 8, pos.Y + height / 2 - 2), 2, 5, -270));
                        fireRate = 0;
                    }
                    break;
                case 5:
                    fireRate += 1;
                    if (fireRate >= 32)
                    {
                        if (direction == 3)
                        {
                            enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + width / 2 - 8, pos.Y + height / 2 - 2), 1, 7, -180));
                        }
                        else
                        {
                            enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + width / 2 - 8, pos.Y + height / 2 - 2), 1, 7, 0));
                        }
                        fireRate = 0;
                    }
                    break;
                case 6:
                    fireRate += 1;
                    if (fireRate == 32 || fireRate == 32 + 8 || fireRate == 32 + 16)
                    {
                        enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + width / 2, pos.Y + height / 2), 5, 10, random.Next(-280, -260)));
                    }
                    if (fireRate >= 64)
                    {
                        fireRate = 0;
                    }
                    break;
            }
        }
        public void Animation()
        {
            // most of the cases are the same, move frame after a few ticks and when the frame hits a certin frame restart the animation 
            switch (graphicsType)
            {
                case 1:
                    imx = Frame(currentFrame);
                    animationCount += 1;
                    if(animationCount >= 10)
                    {
                        currentFrame += 1;
                        animationCount = 0;
                    }
                    if (currentFrame == 5)
                    {
                        currentFrame = 1;
                    }
                    break;
                case 3:
                    imx = Frame(currentFrame);
                    animationCount += 1;
                    if(animationCount >= 5)
                    {
                        currentFrame += 1;
                        animationCount = 0;
                    }
                    if (currentFrame == 3)
                    {
                        currentFrame = 0;
                    }
                    break;
                case 5:
                    imx = Frame(currentFrame);
                    animationCount += 1;
                    if (animationCount >= 5)
                    {
                        currentFrame += 1;
                        animationCount = 0;
                    }
                    if (currentFrame == 7)
                    {
                        currentFrame = 4;
                    }
                    break;
                case 6:
                    imx = Frame(currentFrame);
                    animationCount += 1;
                    if (animationCount >= 3)
                    {
                        currentFrame += 1;
                        animationCount = 0;
                    }
                    if (currentFrame == 10)
                    {
                        currentFrame = 7;
                    }
                    break;
                case 7:
                    imx = Frame(currentFrame);
                    animationCount += 1;
                    if (animationCount >= 5)
                    {
                        currentFrame += 1;
                        animationCount = 0;
                    }
                    if (currentFrame == 3)
                    {
                        currentFrame = 0;
                    }
                    break;
                case 8:
                    imx = Frame(currentFrame);
                    animationCount += 1;
                    if (animationCount >= 5)
                    {
                        currentFrame += 1;
                        animationCount = 0;
                    }
                    if (currentFrame == 6)
                    {
                        currentFrame = 3;
                    }
                    break;
                case 11:
                    imx = Frame(currentFrame);
                    animationCount += 1;
                    if (animationCount >= 3)
                    {
                        currentFrame += 1;
                        animationCount = 0;
                    }
                    if (currentFrame == 3)
                    {
                        currentFrame = 0;
                    }
                    break;
                case 14:
                    imx = Frame(currentFrame);
                    animationCount += 1;
                    if (animationCount >= 3)
                    {
                        currentFrame += 1;
                        animationCount = 0;
                    }
                    if (currentFrame == 7)
                    {
                        currentFrame = 0;
                    }
                    break;
            }
        }
    }
}
