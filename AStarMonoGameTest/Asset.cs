using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarMonoGameTest
{
	public static class Asset
	{
        public static Texture2D tile;
        public static Texture2D start;
        public static Texture2D wall;
        public static Texture2D enemy;
        public static Texture2D pathSprite;
        public static Texture2D healthbarSprite;

        public static SpriteFont spriteFont;

        public static void LoadContent(ContentManager content)
        {
            tile = content.Load<Texture2D>("Tile");
            wall = content.Load<Texture2D>("TileWall");
            start = content.Load<Texture2D>("TileStart");
            enemy = content.Load<Texture2D>("TileEnemy");
            pathSprite = content.Load<Texture2D>("TilePath");
            healthbarSprite = content.Load<Texture2D>("healthbarSprite");

            spriteFont = content.Load<SpriteFont>("info");
        }
    }
}
