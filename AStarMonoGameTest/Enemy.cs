using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarMonoGameTest
{
    class Enemy : GameObject
    {
        //Thread enemyThread;
        private Stack<Node> path;

        Vector2 targetPosition;

        protected float deltaTime;
        private float dstX;
        private float dstY;


        public Enemy(Vector2 position, Stack<Node> path)
        {
            //enemyThread = new Thread(EnemyUpdate);
            base.position = position;
            this.path = path;
            targetPosition = new Vector2(864, 864);
            speed = 100f;
            sprite = Asset.enemy;
        }


        public override void Update(GameTime gameTime)
        {

            if (path.Count > 0)
            {
                targetPosition = path.Peek().Position;

            }

            dstX = Math.Abs(position.X - targetPosition.X);
            dstY = Math.Abs(position.Y - targetPosition.Y);


            if (position.X >= (targetPosition.X - 0.2f) && position.X >= (targetPosition.X + 0.2f))
            {
                velocity.X = -1f;
            }

            if (position.X <= (targetPosition.X - 0.2f) && position.X <= (targetPosition.X + 0.2f))
            {
                velocity.X = 1f;
            }

            if (position.Y >= (targetPosition.Y - 0.2f) && position.Y >= (targetPosition.Y + 0.2f))
            {
                velocity.Y = -1f;
            }

            if (position.Y <= (targetPosition.Y - 0.2f) && position.Y <= (targetPosition.Y + 0.2f))
            {
                velocity.Y = 1f;
            }

            if (velocity != Vector2.Zero)
            {
                /// Ensures that the player sprite doesn't move faster if they hold down two move keys at the same time.
                velocity.Normalize();
            }


            if (dstX < 1 && dstY < 1)
            {


                velocity = new Vector2(0, 0);
                if (path.Count > 0)
                {
                    path.Pop();
                }

            }

            //Console.WriteLine(velocity);
            Move(gameTime);

            if (path.Count <= 0)
            {
                GameWorld.Destroy(this);
            }

        }

        private void Move(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += ((velocity * speed) * deltaTime);
        }


    }
}
