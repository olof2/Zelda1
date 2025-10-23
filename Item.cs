using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Zelda1
{
    public class Item
    {
        Texture2D texture;
        public Vector2 position;
        public Rectangle rect;
        public bool isCollected;
        Player player;

        public Item(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            isCollected = false;
            rect = new Rectangle((int)position.X+5, (int)position.Y+5, 30, 30);
        }

        

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!isCollected)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
        }
    }
}
