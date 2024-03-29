﻿using Microsoft.Xna.Framework;
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
        private TowerType towerType;
        private Rectangle uIBounds;


        public override Rectangle UIBounds
        {
            get { return new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height); }
        }


        public UI(TowerType towerType, Texture2D sprite, Vector2 position)
        {
            this.towerType = towerType;
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

                    if (towerType == TowerType.H)
                    {
                        GridManager.ClickType = TowerType.H;
                        GridManager.TowerPicked = true;
                    }

                    else if (towerType == TowerType.A)
                    {
                        GridManager.ClickType = TowerType.A;
                        GridManager.TowerPicked = true;
                    }

                    else if (towerType == TowerType.G)
                    {
                        GridManager.ClickType = TowerType.G;
                        GridManager.TowerPicked = true;
                    }

                    else if (towerType == TowerType.I)
                    {
                        GridManager.ClickType = TowerType.I;
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
