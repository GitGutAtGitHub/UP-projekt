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
    class UI : GameObject
    {
        public UI(Texture2D sprite, Vector2 position)
        {
           
            base.sprite = sprite;
            base.position = position;
        }

        public override void Update(GameTime gameTime)
        {

            if (CollisionBox.Contains(GameWorld.mousePoint))
            {
                if (GameWorld.curMouseState.LeftButton == ButtonState.Released && GameWorld.prevMouseState.LeftButton == ButtonState.Pressed)
                {
                    Console.WriteLine("UI: Clicked");
                }
               
            }
        }

        public static void DelleTest()
        {
            //Console.WriteLine("UI: Clicked");
        }
    }
}
