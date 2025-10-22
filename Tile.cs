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
    public class Tile
    {
        public Vector2 position;
        public Texture2D texture;
        public bool isWalkable;
        public bool enemyBlock;

        public Tile(Texture2D texture, Vector2 position, bool isWalkable, bool enemyBlock) 
        {
            this.position = position;
            this.texture = texture;
            this.isWalkable = isWalkable;
            this.enemyBlock = enemyBlock;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }
    }
}
