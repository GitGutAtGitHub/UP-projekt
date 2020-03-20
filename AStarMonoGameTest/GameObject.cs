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
    public abstract class GameObject
    {

        #region Fields
      
        protected Color tint = Color.White;

        protected Texture2D sprite;

        protected Vector2 position;

        protected Vector2 velocity;
        protected float speed;

        public Vector2 Position { get => position; set => position = value; }
        public  Color Tint { get => tint; set => tint = value; }
        public Texture2D Sprite { get => sprite; set => sprite = value; }

        public virtual Rectangle CollisionBox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, sprite.Width * (int)GameWorld.scale, sprite.Height * (int)GameWorld.scale); }
        }

        #endregion

        public virtual void LoadContent(ContentManager content)
        {

        }

        public abstract void Update(GameTime gameTime);

        public Vector2 GetCoordinate()
        {
            return position / GameWorld.cellSize;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Sprite, Position, null, Color.White, 0, new Vector2(0, 0), GameWorld.scale, SpriteEffects.None, 1);
        }
    }
}
