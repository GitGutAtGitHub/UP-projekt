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

        private Stack<Node> path;
        Vector2 targetPosition;
        protected float deltaTime;


        public Enemy(Vector2 position)
        {
            this.position = position;
            targetPosition = new Vector2(864, 864);
            this.Path = Path;
            speed = 100f;
        }

        public Stack<Node> Path { get => path; set => path = value; }

        public override void LoadContent(ContentManager content)
        {
            sprite = Asset.enemy;
        }

        public override void Update(GameTime gameTime)
        {
            if (path.Count > 0)
            {
                targetPosition = path.Peek().Position;
            }

            if (position.X > targetPosition.X)
            {
                position.X--;
            }
            if (position.X < targetPosition.X)
            {
                position.X++;     
            }
            if (position.Y > targetPosition.Y)
            {
                position.Y--;
            }
            if (position.Y < targetPosition.Y)
            {
                position.Y++;
            }

            //if (velocity != Vector2.Zero)
            //{
            //    /// Ensures that the player sprite doesn't move faster if they hold down two move keys at the same time.
            //    velocity.Normalize();
            //}

            if (position == targetPosition)
            {
                //velocity = new Vector2(0,0);
                if (path.Count > 0)
                {
                    path.Pop();
                }

            }

            /*
            Console.WriteLine(velocity);
            Move(gameTime);
            */
        }

        private void Move(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += ((velocity * speed) * deltaTime);
        }
    }
}
