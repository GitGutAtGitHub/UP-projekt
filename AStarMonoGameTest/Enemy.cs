using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarMonoGameTest
{
    class Enemy : GameObject
    {
        #region fields
        private Stack<Node> path;
        Vector2 targetPosition;
        protected float deltaTime;
        private float dstX;
        private float dstY;

        private int healthbarHeight = 20;
        private int healthbarWidth = 10;

        private Rectangle currentHealthRectangle;
        private Rectangle maxHealthRectangle;

        private Color healthbarColor;

        //the current health for the current healthbar
        private byte currentHealth;
        private byte endHealth;

        private byte hHealth = 5;
        private byte aHealth = 15;
        private byte gHealth = 8;
        private byte iHealth = 10;

        private byte choosenHealthBar = 0;

        private bool findNewBar = true;




        Random brandom = new Random();


        private List<HealthbarStruct> healthbarList = new List<HealthbarStruct>();

        #endregion

        public Enemy(Vector2 position, Stack<Node> path)
        {
            //enemyThread = new Thread(EnemyUpdate);
            base.position = position;
            this.path = path;
            targetPosition = new Vector2(864, 864);
            speed = 100f;
            sprite = Asset.enemy;

            // Adds all four different healthbars to a list.
            healthbarList.Add(new HealthbarStruct(hHealth, Color.Red));
            healthbarList.Add(new HealthbarStruct(aHealth, Color.Yellow));
            healthbarList.Add(new HealthbarStruct(gHealth, Color.Green));
            healthbarList.Add(new HealthbarStruct(iHealth, Color.Blue));
        }

        public override void Update(GameTime gameTime)
        {
            HealthbarHandler();
            CursedDamagde();
            Move(gameTime);

        }

        private void Move(GameTime gameTime)
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

            if (path.Count <= 0)
            {
                GameWorld.Destroy(this);
                GameWorld.failedProjects += 1;
            }
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += ((velocity * speed) * deltaTime);
        }



        /// <summary>
        /// Handles the healthbars. Only one healthbar out of four appears at a time.
        /// Each healthbar is randomly chosen and can only be chosen once per enemy.
        /// Once an enemys four healthbars have all been filled out, the enemy disappears.
        /// </summary>
        private void HealthbarHandler()
        {
            //if we have to find a new healthbar
            if (findNewBar == true)
            {
                //choose a random healthbar at the healthbar list
                choosenHealthBar = (byte)brandom.Next(0, (healthbarList.Count));
                // The health the player needs to get the enemy to.
                endHealth = healthbarList[choosenHealthBar].health;
                // The current health of the randomly selected healthbar.
                currentHealth = 1;
                // Adds the correct color to the randomly selected healthbar.
                healthbarColor = healthbarList[choosenHealthBar].healthColor;
                //change it to false, so it dosent choose a new one immediatly
                findNewBar = false;
            }
            // Sets the max healhbar so the player can see how much health is still needed.
            maxHealthRectangle = new Rectangle((int)position.X, (int)position.Y, endHealth * 10, healthbarHeight);
            //updates the healthbar rectangle
            currentHealthRectangle = new Rectangle((int)position.X, (int)position.Y, currentHealth * 10, healthbarHeight);

            //if the healthbar is filled up, find a new one
            if (currentHealth >= endHealth)
            {
                healthbarList.RemoveAt(choosenHealthBar);
                findNewBar = true;
            }

            //if there is no new healthbar (the list of healthbars is empty), destory the object.
            if (healthbarList.Count <= 0)
            {
                // All four healthbars have been filled out and the enemy is removed from the game.
                GameWorld.Destroy(this);
                // The players gets a point for each finished project.
                GameWorld.projectsDone += 1;
                // Resources are gained for every successful project.
                GameWorld.resources += 1;
            }
        }

        


        public override void Draw(SpriteBatch spriteBatch)
        {
            // draws the enemy sprite.
            spriteBatch.Draw(Sprite, Position, null, Color.White, 0, new Vector2(0, 0), GameWorld.scale, SpriteEffects.None, 1);

            //draws the healthbar
            spriteBatch.Draw(Asset.healthbarSprite, currentHealthRectangle, healthbarColor);

            //draws the outline of the maximum healthbar tm
            Rectangle topLine = new Rectangle((int)position.X, (int)position.Y, endHealth * healthbarWidth, 1);
            Rectangle bottomLine = new Rectangle((int)position.X, (int)position.Y + healthbarHeight, endHealth * healthbarWidth, 1);
            Rectangle rightLine = new Rectangle((int)position.X + endHealth * healthbarWidth, (int)position.Y, 1, healthbarHeight);
            Rectangle leftLine = new Rectangle((int)position.X, (int)position.Y, 1, healthbarHeight);
            spriteBatch.Draw(Asset.healthbarSprite, topLine, null, healthbarColor, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(Asset.healthbarSprite, bottomLine, null, healthbarColor, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(Asset.healthbarSprite, rightLine, null, healthbarColor, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(Asset.healthbarSprite, leftLine, null, healthbarColor, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        /// <summary>
        /// USED FOR TESTING ONLY. CAN BE DELETED ONCE TOWERS CAN DO "DAMAGE".
        /// </summary>
        public void CursedDamagde()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad5))
            {
                if (currentHealth < endHealth)
                {
                    currentHealth++;
                }
                // Console.WriteLine($"Health: {currentHealth}");
            }
        }
    }

    /// <summary>
    /// Datatype for the healthbar, containing a vale for the amount of health
    /// and a value for the color
    /// </summary>
    public struct HealthbarStruct
    {
        public byte health;
        public Color healthColor;

        public HealthbarStruct(byte health, Color healthColor)
        {
            this.health = health;
            this.healthColor = healthColor;
        }
    }
}
