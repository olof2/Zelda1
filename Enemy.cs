using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Zelda1
{
    public class Enemy
    {
        public Texture2D texture;
        public Rectangle rect;
        public Vector2 position;
        public Vector2 direction;
        public Vector2 destination;
        public float speed;
        public Color color;

        Random rnd = new Random();
        int type = 1;

        public int health;
        public int maxHealth;

        public bool isMoving;
        public bool isAttacking;

        private AnimationManager walkAnim;
        private AnimationManager currentAnim;

        Rectangle[] walkRects = AnimationManager.enemyWalkRects;
        

        //lägga till en int/enum som "movestate" = patroll eller random. lägga till attacks?

        Timer _timer = new Timer();

        public Enemy(Texture2D texture, Vector2 positon, int type)
        {
            this.type = type;
            this.texture = texture;
            this.position = positon;
            direction = Vector2.Zero;
            isMoving = false;
            isAttacking = false;
            rect = new Rectangle((int)position.X + 5, (int)position.Y + 5, 30, 30);
            _timer.resetAndStart(1.0);
            
            speed = 150f;
            health = 3;
            maxHealth = 3;
            color = Color.White;

            walkAnim = new AnimationManager(walkRects, 3.5f);
            //switch case för types
            //default är 1. ställ in fär beroende på andra types. ställ in hastighet, health
            //type 1. vit. 2hp. random direction. 150f speed
            //type 2. orange. 2hp. patrull direction höger-vänster. 150f speed
            //type 3. orange. 2hp. patrull direction upp-ner. 150f speed
            //type 4. röd. 3hp. patrull direction höger-vänster. 100 till 175 speed

            switch (type)
            {
                case 1:
                default:
                    color = Color.White;
                    health = 2;
                    maxHealth = 2;
                    speed = 150f;
                    break;
                case 2:
                    color = Color.Orange;
                    health = 2;
                    maxHealth = 2;
                    speed = 150f;
                    break;
                case 3:
                    color = Color.Orange;
                    health = 2;
                    maxHealth = 2;
                    speed = 150f;
                    break;
                case 4:
                    color = Color.Red;
                    health = 3;
                    maxHealth = 3;
                    //speed = rnd.Next(100, 176);
                    speed = 200;
                    break;
            }

        }

        public void Update(GameTime gameTime)
        {
            _timer.Update(gameTime.ElapsedGameTime.TotalSeconds);

            currentAnim = walkAnim;
            currentAnim.setSpeed(7.5f);
            currentAnim.setPlay();
            currentAnim.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            //&& attackTimer.isDone()
            if (!isMoving && !isAttacking && _timer.isDone())
            {
                int _case4 = rnd.Next(0, 4);
                int _case2 = rnd.Next(0, 2);

                switch (type)
                {
                    case 1:
                    default:
                        //random movement enemy här
                        switch (_case4)
                        {
                            case 0:
                                ChangeDirection(new Vector2(0, -1), gameTime);
                                _timer.resetAndStart(0.8);
                                break;

                            case 1:
                                ChangeDirection(new Vector2(0, 1), gameTime);
                                _timer.resetAndStart(0.8);
                                break;

                            case 2:
                                ChangeDirection(new Vector2(1, 0), gameTime);
                                _timer.resetAndStart(0.8);
                                break;

                            case 3:
                                ChangeDirection(new Vector2(-1, 0), gameTime);
                                _timer.resetAndStart(0.8);
                                break;

                            default:
                                //Attack();
                                _timer.resetAndStart(0.8);
                                break;

                        }
                        break;

                    case 2:                    
                        //patroll enemy här höger-vänster
                        switch (_case2)
                        {
                            case 0:
                                ChangeDirection(new Vector2(1, 0), gameTime);
                                _timer.resetAndStart(0.8);
                                break;

                            case 1:
                                ChangeDirection(new Vector2(-1, 0), gameTime);
                                _timer.resetAndStart(0.8);
                                break;

                            default:
                                //Attack();
                                _timer.resetAndStart(0.8);
                                break;

                        }
                        break;

                    case 3:
                        //patroll enemy här upp-ner
                        switch (_case2)
                        {
                            case 0:
                                ChangeDirection(new Vector2(0, 1), gameTime);
                                _timer.resetAndStart(0.8);
                                break;

                            case 1:
                                ChangeDirection(new Vector2(0, -1), gameTime);
                                _timer.resetAndStart(0.8);
                                break;

                            default:
                                //Attack();
                                _timer.resetAndStart(0.8);
                                break;

                        }
                        break;

                    case 4:
                        //patroll enemy här höger-vänster
                        double _dbouble = rnd.Next(4, 8);
                        _dbouble /= 10;
                        switch (_case2)
                        {
                            case 0:
                                ChangeDirection(new Vector2(1, 0), gameTime);
                                _timer.resetAndStart(_dbouble);
                                break;

                            case 1:
                                ChangeDirection(new Vector2(-1, 0), gameTime);
                                _timer.resetAndStart(_dbouble);
                                break;

                            default:
                                //Attack();
                                _timer.resetAndStart(_dbouble);
                                break;

                        }
                        break;
                }
                
                    
                
                
            }
            else if (!isMoving && isAttacking) { }
            else
            {
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
                }
            }
        }

        public void ChangeDirection(Vector2 dir, GameTime gameTime)
        {
            direction = dir;
            Vector2 newDestination = position + direction * Game1.tileSize;
            if (Game1.isTileWalkable(newDestination) && !Game1.isTileBlock(newDestination))
            {
                destination = newDestination;
                isMoving = true;
            }
        }

        public void Draw(SpriteBatch sb)
        {

            sb.Draw(texture, rect, walkAnim.GetCurrentRect(), color);
            //gör color till en variabel för att kunna skapa fiender i olika färg
        }
    }
}
