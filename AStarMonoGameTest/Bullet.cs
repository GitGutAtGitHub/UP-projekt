using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarMonoGameTest
{
    enum BulletType { H, A, G, I }

    class Bullet : GameObject
    {
        private Texture2D bulletSprite;
        private Enemy enemy;
        private float targetDistanceX;
        private float targetDistanceY;
        protected float deltaTime;
        private BulletType bulletType;
        private Rectangle bulletBounds;

        public Rectangle BulletBounds { get => bulletBounds; set => bulletBounds = value; }


        public Bullet(BulletType bulletType, Texture2D bulletSprite)
        {
            this.bulletSprite = bulletSprite;
            this.bulletType = bulletType;
            BulletBounds = new Rectangle((int)position.X * (int)GameWorld.cellSize, (int)position.Y * (int)GameWorld.cellSize, (int)GameWorld.cellSize, (int)GameWorld.cellSize);
        }


        public override void Update(GameTime gameTime)
        {
            SeekAndDestroy();
            UpdateDistance(enemy);
        }


        public void Move(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += ((velocity * speed) * deltaTime);
        }

        private void OnCollision()
        {
            //AddHealth(this);
        }

        public virtual void CheckCollision(GameObject other)
        {
            //if (other is Enemy)
            //{
            //    if (other..Intersects(this.EnemyBounds))
            //    {

            //    }
            //}
        }

        public void UpdateDistance(Enemy target)
        {
            targetDistanceX = target.Position.X - position.X;
            targetDistanceY = target.Position.Y - position.Y;
        }

        public void SeekAndDestroy()
        {
            if ((targetDistanceX >= -sprite.Width * 2 && targetDistanceX <= sprite.Width * 2) &&
                (targetDistanceY >= -sprite.Height * 2 && targetDistanceY <= sprite.Height * 2))
            {

                if (targetDistanceX < -sprite.Width / 2)
                {
                    velocity.X = -1f;
                }
                else if (targetDistanceX > sprite.Width / 2)
                {
                    velocity.X = 1f;
                }


                if (targetDistanceY < -sprite.Height / 2)
                {
                    velocity.Y = -1f;
                }
                else if (targetDistanceY > sprite.Height / 2)
                {
                    velocity.Y = 1f;
                }


                if (velocity != Vector2.Zero)
                {
                    // Ensures that the player sprite doesn't move faster if they hold down two move keys at the same time.
                    velocity.Normalize();
                }
            }
        }
    }
}
