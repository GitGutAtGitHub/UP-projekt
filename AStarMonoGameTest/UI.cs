using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarMonoGameTest
{
    class UI : GameObject
    {
        public override Rectangle UIBounds
        {
            get { return new Rectangle((int)position.X, (int)position.Y, sprite.Width * (int)GameWorld.scale, sprite.Height * (int)GameWorld.scale); }
        }


        public UI(Texture2D sprite, Vector2 position)
        {
            base.sprite = sprite;
            base.position = position;
            GameWorld.mouseHandler.leftClickEvent += OnLeftClickEvent;
            GameWorld.mouseHandler.rightClickEvent += OnRightClickEvent;
        }


        public void OnRightClickEvent()
        {
            GridManager.TowerPicked = false;
        }

        public void OnLeftClickEvent()
        {
            if (GameWorld.mouseHandler.Point.X > 9 || GameWorld.mouseHandler.Point.Y > 9)
            {
                if (UIBounds.Contains(GameWorld.mouseHandler.PointUI))
                {
                    if (sprite == Asset.wall)
                    {
                        GridManager.ClickType = TowerType.H;
                        GridManager.TowerPicked = true;
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
