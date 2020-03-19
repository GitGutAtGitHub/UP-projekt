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
    enum NodeType { EMPTY, ENEMY, START, GOAL, TOWER }

    class Node : GameObject
    {
        #region Fields
        private Rectangle nodeRectangle;

        private bool discovered;/* = false;*/
        public bool walkable;/* = true;*/
        public bool containsTower;

        private int gCost = 0;
        private int hCost = 0;

        private Node parent;

        public NodeType type;
        #endregion


        #region Properties
        public Rectangle NodeRectangle { get => nodeRectangle; set => nodeRectangle = value; }
        public bool Discovered { get => discovered; set => discovered = value; }
        public Node Parent { get => parent; set => parent = value; }
        public int GCost { get => gCost; set => gCost = value; }
        public int HCost { get => hCost; set => hCost = value; }
        public int FCost { get => hCost + gCost; }
        #endregion


        public Node(Vector2 position, NodeType type, bool walkable, bool containsTower)
        {
            base.Position = position;
            this.type = type;
            this.walkable = walkable;
            this.containsTower = containsTower;
            NodeRectangle = new Rectangle((int)position.X * (int)GameWorld.cellSize, (int)position.Y * (int)GameWorld.cellSize, (int)GameWorld.cellSize, (int)GameWorld.cellSize);
        }


        public override void LoadContent(ContentManager content)
        {
            switch (type)
            {
                case (NodeType.EMPTY):
                    Sprite = Asset.tile;
                    break;


                case (NodeType.TOWER):
                    Sprite = Asset.wall;
                    break;

                case (NodeType.ENEMY):
                    Sprite = Asset.enemy;
                    break;

                case (NodeType.GOAL):
                    Sprite = Asset.start;
                    break;

                case (NodeType.START):
                    Sprite = Asset.start;
                    break;

                default:
                    Sprite = Asset.tile;
                    break;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Sprite, Position, null, Color.Green, 0, new Vector2(0, 0), GameWorld.scale, SpriteEffects.None, 0.9f);

            spriteBatch.DrawString(Asset.spriteFont, $"G: {gCost}", new Vector2((Position.X + 5), (Position.Y)), Color.DarkRed, 0, Vector2.Zero, 1, SpriteEffects.None, 0.92f);
            spriteBatch.DrawString(Asset.spriteFont, $"H: {hCost}", new Vector2((Position.X + 5), (Position.Y + (Sprite.Height * GameWorld.scale / 1.3f))), Color.DarkBlue, 0, Vector2.Zero, 1, SpriteEffects.None, 0.92f);
            spriteBatch.DrawString(Asset.spriteFont, $"F: {FCost}", new Vector2((Position.X + Sprite.Width * GameWorld.scale / 1.5f), (Position.Y + (Sprite.Height * GameWorld.scale / 1.3f))), Color.DarkGreen, 0, Vector2.Zero, 1, SpriteEffects.None, 0.92f);


        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
