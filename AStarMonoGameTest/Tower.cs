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
        private TowerType towerType;

        private Rectangle towerBounds;
        #endregion


        public Rectangle TowerBounds { get => towerBounds; set => towerBounds = value; }


        public Tower(int health, TowerType towerType, Vector2 position, Texture2D sprite)
        {
            this.towerType = towerType;
            base.health = health;
            Position = position;
            Sprite = sprite;
            TowerBounds = new Rectangle((int)position.X * (int)GameWorld.cellSize, (int)position.Y * (int)GameWorld.cellSize, (int)GameWorld.cellSize, (int)GameWorld.cellSize);
            GameWorld.resources -= 5;
        }


        public override void LoadContent(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {
            Shoot();
        }

        public void Shoot()
        {
            if (towerType == TowerType.H)
            {
                Bullet bullet = new Bullet(BulletType.H, Asset.bulletRed);
            }

            else if (towerType == TowerType.A)
            {
                Bullet bullet = new Bullet(BulletType.A, Asset.bulletYellow);
            }

            else if (towerType == TowerType.G)
            {
                Bullet bullet = new Bullet(BulletType.G, Asset.bulletGreen);
            }

            else if (towerType == TowerType.I)
            {
                Bullet bullet = new Bullet(BulletType.I, Asset.bulletBlue);
            }
        }
    }
}
