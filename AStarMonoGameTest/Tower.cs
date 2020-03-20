using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarMonoGameTest
{
    enum TowerType { H, A, G, I }

    class Tower : GameObject
    {
        #region Fields
        private int health;
        private int bulletSpeed;

        private Rectangle towerRectangle;
        #endregion


        public Rectangle TowerRectangle { get => towerRectangle; set => towerRectangle = value; }


        public Tower(string name, int health, int bulletSpeed, int damage, Vector2 position, Texture2D sprite)
        {
            base.name = name;
            this.bulletSpeed = bulletSpeed;
            this.health = health;
            Position = position;
            Sprite = sprite;
            TowerRectangle = new Rectangle((int)position.X * (int)GameWorld.cellSize, (int)position.Y * (int)GameWorld.cellSize, (int)GameWorld.cellSize, (int)GameWorld.cellSize);
            GameWorld.resources -= 5;
        }


        public override void LoadContent(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
