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
    public enum NodeType { Empty, Enemy, Start, Goal, Tower}

    class Node : GameObject
    {

        private bool discovered = false;
        private Node parent;
        private int gCost = 0;
        private int hCost = 0;
        public bool walkable = true;
        public NodeType type;
      
        public bool Discovered { get => discovered; set => discovered = value; }
        public Node Parent { get => parent; set => parent = value; }
        public int GCost { get => gCost; set => gCost = value; }
        public int HCost { get => hCost; set => hCost = value; }
        public int FCost { get => hCost+gCost;}

        public override void LoadContent(ContentManager content)
        {
            switch (type)
            {
                case (NodeType.Empty):
                    Sprite = Asset.tile;
                    break;


                case (NodeType.Tower):
                    Sprite = Asset.wall;
                    break;

                case (NodeType.Enemy):
                    Sprite = Asset.enemy;
                    break;

                case (NodeType.Goal):
                    Sprite = Asset.start;
                    break;

                case (NodeType.Start):
                    Sprite = Asset.start;
                    break;

                default:
                    Sprite = Asset.tile;
                    break;
            } 
            
        }

    

        public Node(Vector2 position, NodeType type, bool walkable)
        {
            base.Position = position;
            this.type = type;
            this.walkable = walkable;
            GameWorld.clickEvent += ClickDisShit;
        }

        
        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Sprite, Position, null, Color.Green, 0, new Vector2(0, 0), GameWorld.scale, SpriteEffects.None, 0.9f);

            spriteBatch.DrawString(Asset.spriteFont, $"G: {gCost}", new Vector2((Position.X+5), (Position.Y)), Color.DarkRed, 0, Vector2.Zero, 1, SpriteEffects.None, 0.92f);
            spriteBatch.DrawString(Asset.spriteFont, $"H: {hCost}", new Vector2((Position.X+5), (Position.Y+(Sprite.Height*GameWorld.scale/1.3f))), Color.DarkBlue, 0, Vector2.Zero, 1, SpriteEffects.None, 0.92f);
            spriteBatch.DrawString(Asset.spriteFont, $"F: {FCost}", new Vector2((Position.X +Sprite.Width*GameWorld.scale/1.5f), (Position.Y + (Sprite.Height * GameWorld.scale / 1.3f))), Color.DarkGreen, 0, Vector2.Zero, 1, SpriteEffects.None, 0.92f);


        }

        public void ClickDisShit()
        {

            Console.WriteLine("test");
            if (CollisionBox.Contains(GameWorld.mousePoint))
            {
                if (GameWorld.curMouseState.LeftButton == ButtonState.Released && GameWorld.prevMouseState.LeftButton == ButtonState.Pressed)
                {
                    type = NodeType.Tower;
                    sprite = Asset.wall;
                    walkable = false;
                }
                if (GameWorld.curMouseState.RightButton == ButtonState.Released && GameWorld.prevMouseState.RightButton == ButtonState.Pressed)
                {
                    type = NodeType.Empty;
                    sprite = Asset.tile;
                    walkable = true;
                }
            }
            
      
        }
        public override void Update(GameTime gameTime)
        {
            /*
            if (CollisionBox.Contains(GameWorld.mousePoint))
            {
                if (GameWorld.curMouseState.LeftButton == ButtonState.Released && GameWorld.prevMouseState.LeftButton == ButtonState.Pressed)
                {
                    type = NodeType.Tower;
                    sprite = Asset.wall;
                    walkable = false;
                }
                if (GameWorld.curMouseState.RightButton == ButtonState.Released && GameWorld.prevMouseState.RightButton == ButtonState.Pressed)
                {
                    type = NodeType.Empty;
                    sprite = Asset.tile;
                    walkable = true;
                }
            }
            */
            
        }
    }
}
