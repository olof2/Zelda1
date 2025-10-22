using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Zelda1
{
    public class TextureManager
    {
        public static Texture2D playerTexture;
        public static Texture2D enemyTexture;
        public static Texture2D bridgeTexture;
        public static Texture2D bushTexture;
        public static Texture2D bushSTexture;
        public static Texture2D doorTexture;
        public static Texture2D grassTexture;
        public static Texture2D keyTexture;
        public static Texture2D openDoorTexture;
        public static Texture2D soilTexture;
        public static Texture2D stoneFloorTexture;
        public static Texture2D wallTexture;
        public static Texture2D zeldaTexture;
        public static Texture2D waterTexture;

        public static int textureSize = 40;

        public static void LoadTextures(ContentManager content)
        {
            playerTexture = content.Load<Texture2D>("Link_all2");
            enemyTexture = content.Load<Texture2D>("skelett_anim");
            bridgeTexture = content.Load<Texture2D>("bridge");
            bushTexture = content.Load<Texture2D>("bush");
            bushSTexture = content.Load<Texture2D>("bushS");
            doorTexture = content.Load<Texture2D>("door");
            grassTexture = content.Load<Texture2D>("grass");
            keyTexture = content.Load<Texture2D>("key");
            openDoorTexture = content.Load<Texture2D>("opendoor");
            soilTexture = content.Load<Texture2D>("soil");
            stoneFloorTexture = content.Load<Texture2D>("stonefloor");
            wallTexture = content.Load<Texture2D>("wall");
            zeldaTexture = content.Load<Texture2D>("Zelda");
            waterTexture = content.Load<Texture2D>("water");
        }


    }
}
