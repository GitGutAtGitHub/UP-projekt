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
        public static SpriteFont spriteFont;
        public static Texture2D hUI;
        public static Texture2D aUI;
        public static Texture2D gUI;
        public static Texture2D iUI;
        public static Texture2D bulletRed;
        public static Texture2D bulletBlue;
        public static Texture2D bulletYellow;
        public static Texture2D bulletGreen;
        public static Texture2D hTower;
        public static Texture2D aTower;
        public static Texture2D gTower;
        public static Texture2D iTower;


        public static void LoadContent(ContentManager content)
        {
            tile = content.Load<Texture2D>("Tile");
            wall = content.Load<Texture2D>("TileWall");
            start = content.Load<Texture2D>("TileStart");
            enemy = content.Load<Texture2D>("Rick");
            pathSprite = content.Load<Texture2D>("TilePath");
            spriteFont = content.Load<SpriteFont>("info");

            hUI = content.Load<Texture2D>("H");
            aUI = content.Load<Texture2D>("A");
            gUI = content.Load<Texture2D>("G");
            iUI = content.Load<Texture2D>("I");

            bulletRed = content.Load<Texture2D>("BulletRed");
            bulletBlue = content.Load<Texture2D>("BulletBlue");
            bulletYellow = content.Load<Texture2D>("BulletYellow");
            bulletGreen = content.Load<Texture2D>("BulletGreen");

            hTower = content.Load<Texture2D>("H");
            aTower = content.Load<Texture2D>("A");
            gTower = content.Load<Texture2D>("G");
            iTower = content.Load<Texture2D>("I");
        }
    }
}
