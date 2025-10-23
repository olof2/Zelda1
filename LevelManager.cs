using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zelda1
{
    public class LevelManager
    {
        public Tile[,] tileArray;
        public Player player;
        int tileSize = Game1.tileSize;
        Enemy enemy;
        public static List<Enemy> enemies = new List<Enemy>();
        public Item item;
        public static List<Item> items = new List<Item>();
        public LevelManager(string fileName) 
        { 
            ReadFromFile(fileName);
            CreateLevel(fileName);
        }
        List<string> ReadFromFile(string fileName)
        {
            //skapar en lista of strings som motsvarar filename (kartan)
            string line;
            List<string> strings = new List<string>();

            StreamReader sr = new StreamReader(fileName);
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                strings.Add(line);
            }
            sr.Close();
            return strings;

        }

        //public static void Update(GameTime gameTime)
        //{
        //    if (items[0].isCollected)
        //    {
        //        Debug.WriteLine("Key collected!");
        //        foreach (Tile tile in tileArray)
        //        {

        //        }
        //    }
        //}
        void CreateLevel(string fileName)
        {
            //tar listan av strings som är level och stoppar in den i en array
            //skapar tiles efter arrayn
            List<string> level = ReadFromFile(@"Level1.txt");
            foreach (string line in level) { Debug.WriteLine(line); }

            tileArray = new Tile[level[0].Length, level.Count];

            for (int row = 0; row < level.Count; row++)
            {
                for (int col = 0; col < level[row].Length; col++)
                {
                    if (level[row][col] == 'L')
                    {
                        tileArray[col, row] = new Tile(TextureManager.grassTexture, new Vector2(col * tileSize, row * tileSize), true, false);
                        player = new Player(new Vector2(col * tileSize, row * tileSize));
                    }
                    else if (level[row][col] == '1')
                    {
                        tileArray[col, row] = new Tile(TextureManager.stoneFloorTexture, new Vector2(col * tileSize, row * tileSize), true, false);
                        enemy = new Enemy(TextureManager.enemyTexture, new Vector2(col* tileSize, row * tileSize), 1);
                        enemies.Add(enemy);
                        //spawna enemy1
                    }
                    else if (level[row][col] == '2')
                    {
                        tileArray[col, row] = new Tile(TextureManager.stoneFloorTexture, new Vector2(col * tileSize, row * tileSize), true, false);
                        enemy = new Enemy(TextureManager.enemyTexture, new Vector2(col* tileSize, row * tileSize), 2);
                        enemies.Add(enemy);
                        //spawna enemy2
                    }
                    else if (level[row][col] == '3')
                    {
                        tileArray[col, row] = new Tile(TextureManager.stoneFloorTexture, new Vector2(col * tileSize, row * tileSize), true, false);
                        enemy = new Enemy(TextureManager.enemyTexture, new Vector2(col* tileSize, row * tileSize), 3);
                        enemies.Add(enemy);
                        //spawna enemy3
                    }
                    else if (level[row][col] == '4')
                    {
                        tileArray[col, row] = new Tile(TextureManager.stoneFloorTexture, new Vector2(col * tileSize, row * tileSize), true, false);
                        enemy = new Enemy(TextureManager.enemyTexture, new Vector2(col* tileSize, row * tileSize), 4);
                        enemies.Add(enemy);
                        //spawna enemy4
                    }
                    
                    
                    else if (level[row][col] == 'b')
                    {
                        tileArray[col, row] = new Tile(TextureManager.bridgeTexture, new Vector2(col * tileSize, row * tileSize), true, true);
                    }
                    else if (level[row][col] == 'n')
                    {
                        tileArray[col, row] = new Tile(TextureManager.bushTexture, new Vector2(col * tileSize, row * tileSize), true, true);
                    }
                    else if (level[row][col] == 'm')
                    {
                        tileArray[col, row] = new Tile(TextureManager.bushSTexture, new Vector2(col * tileSize, row * tileSize), true, true);
                    }
                    else if (level[row][col] == 'D')
                    {
                        tileArray[col, row] = new Tile(TextureManager.doorTexture, new Vector2(col * tileSize, row * tileSize), false, true);
                    }
                    else if (level[row][col] == '-')
                    {
                        tileArray[col, row] = new Tile(TextureManager.grassTexture, new Vector2(col * tileSize, row * tileSize), true, false);
                    }
                    else if (level[row][col] == 'k')
                    {
                        tileArray[col, row] = new Tile(TextureManager.grassTexture, new Vector2(col * tileSize, row * tileSize), true, false);
                        item = new Item(TextureManager.keyTexture, new Vector2(col * tileSize, row * tileSize));
                        items.Add(item);
                        ////spawna KEY
                    }
                    else if (level[row][col] == 'd')
                    {
                        tileArray[col, row] = new Tile(TextureManager.openDoorTexture, new Vector2(col * tileSize, row * tileSize), true, false);
                    }
                    else if (level[row][col] == '_')
                    {
                        tileArray[col, row] = new Tile(TextureManager.soilTexture, new Vector2(col * tileSize, row * tileSize), true, false);
                    }
                    else if (level[row][col] == 's')
                    {
                        tileArray[col, row] = new Tile(TextureManager.stoneFloorTexture, new Vector2(col * tileSize, row * tileSize), true, false);
                    }
                    else if (level[row][col] == 'w')
                    {
                        tileArray[col, row] = new Tile(TextureManager.wallTexture, new Vector2(col * tileSize, row * tileSize), false, true);
                    }
                    else if (level[row][col] == 'z')
                    {
                        tileArray[col, row] = new Tile(TextureManager.stoneFloorTexture, new Vector2(col * tileSize, row * tileSize), true, false);
                        item = new Item(TextureManager.zeldaTexture, new Vector2(col * tileSize, row * tileSize));
                        items.Add(item);
                        //spawna ZELDA
                    }
                    else if (level[row][col] == '~')
                    {
                        tileArray[col, row] = new Tile(TextureManager.waterTexture, new Vector2(col * tileSize, row * tileSize), false, true);
                    }

                    //L = player
                    //1 = enemy1 - random movement
                    //2 = enemy2 - patrull höger-vänster
                    //3 = enemy3 - patrull upp-ner
                    //4 = enemy4 - patrull höger-vänster random speed
                    //b = bridge
                    //n = bush
                    //lägg till enemyblock på bush och testa


                    //m = bush1
                    //D = door
                    //- = grass
                    //k = item
                    //d = opendoor
                    //_ = soil
                    //s = stonefloor
                    //w = wall
                    //z = zelda
                    //~ = water

                }
            }
        }

    }
}
