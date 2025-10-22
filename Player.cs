using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Zelda1
{
    public class Player
    {
        public Texture2D texture;
        public Rectangle rect;
        public Vector2 position;
        public Vector2 direction;
        Vector2 facing;
        public Vector2 destination;
        public float speed;
        public int health;
        public int maxHealth = 3;
        public bool isMoving;
        public bool isAttacking;

        public Texture2D swordTexture;
        public Rectangle attackRect;

        Timer attackTimer = new Timer();
        Timer moveTimer = new Timer();
        Timer damageTimer = new Timer();
        public bool isDead = false;

        private AnimationManager currentAnim;
        private AnimationManager walkLeftAnim;
        private AnimationManager walkRightAnim;
        private AnimationManager walkDownAnim;
        private AnimationManager walkUpAnim;
        private AnimationManager attackLeftAnim;
        private AnimationManager attackRightAnim;
        private AnimationManager attackDownAnim;
        private AnimationManager attackUpAnim;
        private AnimationManager deathAnim;


        public Player(Vector2 positon)
        {
            texture = TextureManager.playerTexture;
            swordTexture = TextureManager.keyTexture;
            this.position = positon;
            direction = Vector2.Zero;
            facing = Vector2.Zero;
            speed = 200f;
            health = 3;
            maxHealth = 3;
            isMoving = false;
            isAttacking = false;
            rect = new Rectangle((int)position.X+5, (int)position.Y+5, 30, 30);
            attackTimer.resetAndStart(1.0);
            moveTimer.resetAndStart(0.1);
            damageTimer.resetAndStart(1.0);

            walkLeftAnim = new AnimationManager(AnimationManager.playerWalkLeft, 5.0f);
            walkRightAnim = new AnimationManager(AnimationManager.playerWalkRight, 5.0f);
            walkDownAnim = new AnimationManager(AnimationManager.playerWalkDown, 5.0f);
            walkUpAnim = new AnimationManager(AnimationManager.playerWalkUp, 5.0f);

            attackLeftAnim = new AnimationManager(AnimationManager.attackLeftAnim, 5.0f);
            attackRightAnim = new AnimationManager(AnimationManager.attackRightAnim, 5.0f);
            attackDownAnim = new AnimationManager(AnimationManager.attackDownAnim, 5.0f);
            attackUpAnim = new AnimationManager(AnimationManager.attackUpAnim, 5.0f);

            deathAnim = new AnimationManager(AnimationManager.deathAnim, 3.0f);

            currentAnim = walkRightAnim;
        }

        public void Update(GameTime gameTime)
        {
            attackTimer.Update(gameTime.ElapsedGameTime.TotalSeconds);
            moveTimer.Update(gameTime.ElapsedGameTime.TotalSeconds);
            damageTimer.Update(gameTime.ElapsedGameTime.TotalSeconds);

            if (attackTimer.isDone()) { isAttacking = false; }
            //if (isDead)
            //{
            //    currentAnim.setPlay();
            //    currentAnim.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            //    return;
            //}

            //KeyMouseReader.Update();
            // KeyMouseReader.KeyPressed(Keys.Escape); använd för att tvinga släppa+trycka

            //currentAnim = walkLeftAnim;
            currentAnim.setSpeed(6f);
            (isMoving || isAttacking || isDead ? (Action)currentAnim.setPlay : currentAnim.setPause)();
            currentAnim.Update((float)gameTime.ElapsedGameTime.TotalSeconds);


            if (!isMoving && !isAttacking && !isDead)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && moveTimer.isDone() && !isDead)
                {
                    currentAnim = walkUpAnim;
                    if (FacingDirection(new Vector2(0, -1)) == true)
                    {
                        MoveDirection(new Vector2(0, -1));
                        isMoving = true;
                    }
                    else { moveTimer.resetAndStart(0.1); }
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down) && moveTimer.isDone() && !isDead)
                {
                    currentAnim = walkDownAnim;
                    if (FacingDirection(new Vector2(0, 1)) == true)
                    {
                        MoveDirection(new Vector2(0, 1));
                        isMoving = true;
                    }
                    else { moveTimer.resetAndStart(0.1); }
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Left) && moveTimer.isDone() && !isDead)
                {
                    currentAnim = walkLeftAnim;
                    if (FacingDirection(new Vector2(-1, 0)) == true)
                    {
                        MoveDirection(new Vector2(-1, 0));
                        isMoving = true;
                    }
                    else { moveTimer.resetAndStart(0.1); }
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right) && moveTimer.isDone() && !isDead)
                {
                    currentAnim = walkRightAnim;
                    if (FacingDirection(new Vector2(1, 0)) == true)
                    {
                        MoveDirection(new Vector2(1, 0));
                        isMoving = true;
                    }
                    else { moveTimer.resetAndStart(0.1); }
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Space) && !isDead)
                {
                    if (attackTimer.isDone())
                    {
                        if (currentAnim == walkUpAnim) { currentAnim = attackUpAnim; }
                        else if (currentAnim == walkDownAnim) { currentAnim = attackDownAnim; }
                        else if (currentAnim == walkLeftAnim) { currentAnim = attackLeftAnim; }
                        else if (currentAnim == walkRightAnim) { currentAnim = attackRightAnim; }
                        Attack();
                        Debug.WriteLine("check 1 after attack(), isattacking är " + isAttacking);
                        attackTimer.resetAndStart(0.6);
                        Debug.WriteLine("attacktimer startat, isattacking är " + isAttacking);

                    }
                }

            }
            else
            {
                currentAnim.setPlay();
                position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                rect.X = (int)position.X;
                rect.Y = (int)position.Y;

                if (Vector2.Distance(position, destination) < 4f)
                {
                    position = destination;
                    rect.X = (int)position.X;
                    rect.Y = (int)position.Y;
                    isMoving = false;
                    direction = Vector2.Zero;
                    Debug.WriteLine("facing är " + facing);
                }
            }

            
        }

        public bool FacingDirection(Vector2 dir)
        {
            bool test;
            direction = dir;
            test = (facing == dir) ? test = true : test = false;
            facing = direction;

            return test;
        }
        public void MoveDirection(Vector2 dir)
        {
            direction = dir;
            Vector2 newDestination = position + direction * Game1.tileSize;
            if (Game1.isTileWalkable(newDestination))
            {
                destination = newDestination;
                isMoving = true;
            }
        }

        public void Attack()
        {
            isAttacking = true;
            attackRect = new Rectangle((int)position.X + (int)facing.X * Game1.tileSize, (int)position.Y + (int)facing.Y * Game1.tileSize, Game1.tileSize, Game1.tileSize);
            
            //Debug.WriteLine("position is " + position + " | direction is " + direction + " | attempting to attack");
            //Debug.WriteLine("attackrect set");

        }

        public void TakeDamage(int dmg)
        {
            if (damageTimer.isDone() && health > 0)
            {
                health -= dmg;
                damageTimer.resetAndStart(1.0);
            }

            if (health <= 0)
            {
                isDead = true;
                currentAnim = deathAnim;
            }
            //fortsätt här med dödsanimation och hantering
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, rect, currentAnim.GetCurrentRect(), Color.White);
            if (isAttacking)
            {
                sb.Draw(swordTexture, attackRect, Color.White);

            }

        }
    }
}
