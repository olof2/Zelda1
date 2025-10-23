using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Zelda1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Tile tile;
        public static Tile[,] tileArray;
        public static int tileSize = 40;
        LevelManager levelManager;
        SpriteFont font;

        public static Player player;
        Timer deathTimer = new Timer();


        enum GameState { Start, Play, Pause, Dying, GameOver }
        GameState currentGameState = GameState.Start;
        bool gameVictory = false;

        List<Enemy> enemies = new List<Enemy>();
        //List<Item> items= new List<Item>();


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        protected override void Initialize()
        {
            base.Initialize();
            _graphics.PreferredBackBufferHeight = tileSize * 16;
            _graphics.PreferredBackBufferWidth = tileSize * 32;
            _graphics.ApplyChanges();
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureManager.LoadTextures(Content);
            font = Content.Load<SpriteFont>("spritefont1");

            //hämta enemies, items, etc från levelmanager 
            levelManager = new LevelManager(@"Level1.txt");
            tileArray = levelManager.tileArray;
            player = levelManager.player;
            enemies = LevelManager.enemies;
            //items = LevelManager.items;

            deathTimer.resetAndStart(1.0);

        }


        public static bool isTileWalkable(Vector2 pos)
        {
            return tileArray[(int)pos.X / tileSize, (int)pos.Y / tileSize].isWalkable;
        }
        public static bool isTileBlock(Vector2 pos)
        {
            return tileArray[(int)pos.X / tileSize, (int)pos.Y / tileSize].enemyBlock;
        }


        protected override void Update(GameTime gameTime)
        {

            //lägg till keymouse reader för pause-knapper?
            KeyMouseReader.Update();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (currentGameState == GameState.Start)
            {
                Debug.WriteLine("Gamestate start");

                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    currentGameState = GameState.Play;
                }
            }


            else if (currentGameState == GameState.Play)
            {
                Debug.WriteLine("Gamestate play");
                //LevelManager.Update(gameTime);

                player.Update(gameTime);

                foreach (Item item in LevelManager.items)
                {
                    if (player.rect.Intersects(item.rect) && !item.isCollected)
                    {
                        //Debug.WriteLine("item picked up!");
                        item.isCollected = true;
                        break;
                    }

                    if (LevelManager.items[1].isCollected)
                    {
                        Debug.WriteLine("Key collected!");
                        foreach (Tile tile in tileArray)
                        {
                            //kolla om tile är dörr
                            if (tile.texture == TextureManager.doorTexture)
                            {
                                tile.texture = TextureManager.openDoorTexture;
                                tile.isWalkable = true;
                                tile.enemyBlock = false;
                            }
                        }
                    }

                    if (LevelManager.items[0].isCollected)
                    {
                        Debug.WriteLine("you found zelda!");
                        gameVictory = true;
                        currentGameState = GameState.GameOver;
                    }

                }

                foreach (Enemy enemy in enemies)
                {
                    enemy.Update(gameTime);
                    if (player.rect.Intersects(enemy.rect))
                    {
                        player.TakeDamage(1);
                        Debug.WriteLine("hit by enemy!, health is " + player.health);
                    }
                    else if (player.isAttacking && player.attackRect.Intersects(enemy.rect))
                    {
                        Debug.WriteLine("enemy hit!");
                        enemy.health -= 1;
                        player.isAttacking = false;

                        //se över om det går att göra så svärd/attack fortsätter synas


                        if (enemy.health <= 0)
                        {
                            LevelManager.enemies.Remove(enemy);
                            break;
                        }
                    }
                }

                if (KeyMouseReader.KeyPressed(Keys.P))
                {
                    currentGameState = GameState.Pause;
                }

                if (player.health <= 0)
                {
                    currentGameState = GameState.Dying;
                }
            }


            else if (currentGameState == GameState.Pause)
            {
                // P för unpause, R för restart

                Debug.WriteLine("Gamestate Paused");
                if (KeyMouseReader.KeyPressed(Keys.P))
                {
                    currentGameState = GameState.Play;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    Restart();
                }

            }

            else if (currentGameState == GameState.Dying)
            {
                Debug.WriteLine("Gamestate dying");
                deathTimer.Update(gameTime.ElapsedGameTime.TotalSeconds);

                if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    Restart();
                }
                
                player.isDead = true;
                player.Update(gameTime);

                if (deathTimer.isDone())
                {
                    currentGameState = GameState.GameOver;
                }

            }

            else if (currentGameState == GameState.GameOver)
            {
                Debug.WriteLine("Gamestate gameover");
                if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    Restart();
                }
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            if (currentGameState == GameState.Start)
            {
                _spriteBatch.DrawString(font, "Press Enter to Start", new Vector2(300, 200), Color.White);
            }

            else if (currentGameState == GameState.Play)
            {
                foreach (Tile tile in tileArray)
                {
                    tile.Draw(_spriteBatch);
                }
                player.Draw(_spriteBatch);
                foreach (Item item in LevelManager.items) { item.Draw(_spriteBatch); }
                foreach (Enemy _e in enemies) { _e.Draw(_spriteBatch); }
            }

            else if (currentGameState == GameState.Pause)
            {
                _spriteBatch.DrawString(font, "Game Paused\nPress P to Resume\nPress R to Restart", new Vector2(250, 200), Color.White);
            }

            else if (currentGameState == GameState.Dying)
            {
                player.Draw(_spriteBatch);
                _spriteBatch.DrawString(font, "Game Over\nPress R to Restart", new Vector2(300, 200), Color.White);
            }

            else if (currentGameState == GameState.GameOver)
            {
                //player.Draw(_spriteBatch);
                if (gameVictory)
                {
                    _spriteBatch.DrawString(font, "You found Zelda! congratulations!\nPress R to Restart", new Vector2(300, 200), Color.White);

                }
                else
                {
                    _spriteBatch.DrawString(font, "Game Over\nPress R to Restart", new Vector2(300, 200), Color.White);
                }
            }
            
            
            _spriteBatch.End();
            base.Draw(gameTime);

        }

        private void Restart()
        {
            //restart logic
            enemies.Clear();
            LevelManager.items.Clear();
            levelManager = new LevelManager(@"Level1.txt");

            player.isDead = false;
            deathTimer.resetAndStart(1.0);
            player.health = player.maxHealth;
            player = levelManager.player;

            tileArray = levelManager.tileArray;
            enemies = LevelManager.enemies;
            foreach (Enemy enemy in enemies)
            {
                enemy.health = enemy.maxHealth;
            }
            //items = LevelManager.items;
            foreach (Item item in LevelManager.items)
            {
                item.isCollected = false;
            }
            gameVictory = false;
            currentGameState = GameState.Play;
        }


        //---TO-DO---
        //fixa gamestates           x
        //fixa death och respawn    x
        //nån typ av HUD?
        //patrullerande fiender, random hastighet - utöka fiender hélt enkel    x
        //dörr + nyckel             x
        //zelda winstate            x
        //Zelda alternativt Link skall vara inlåst i ett slott och spelaren måste plocka upp en nyckel för att komma in i slottet och rädda kungariket.
        //Det ska finnas åtminstone 3 floder.För att kors floderna måste spelaren gå på de broar som förbinder land.

        //score
        //animation för hit         x
        //testa animation för död   x
        //andra item för score
        //triforce för annan attack?
    }
}
