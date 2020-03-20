using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarMonoGameTest
{
    public class MouseHandler
    {
        public delegate void ClickDelegate();
        public event ClickDelegate leftClickEvent;
        public event ClickDelegate rightClickEvent;

        #region Fields

        private Point point;
        private Point pointUI;

        private MouseState previousMouseState;
        private MouseState currentMouseState;
        #endregion


        #region Properties
        public Point Point { get => point; set => point = value; }
        public Point PointUI { get => pointUI; set => pointUI = value; }
        public MouseState PreviousMouseState { get => previousMouseState; set => previousMouseState = value; }
        public MouseState CurrentMouseState { get => currentMouseState; set => currentMouseState = value; }
        #endregion


        public MouseHandler()
        {

        }


        public void Update()
        {
            MousePosition();
            MouseClickLeft();
            MouseClickRight();
        }

        public void MousePosition()
        {
            PreviousMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
            Point = new Point(CurrentMouseState.X / (int)GameWorld.cellSize, CurrentMouseState.Y / (int)GameWorld.cellSize);
            PointUI = new Point(CurrentMouseState.X, CurrentMouseState.Y);
        }

        public void MouseClickLeft()
        {
            if (CurrentMouseState.LeftButton == ButtonState.Released && PreviousMouseState.LeftButton == ButtonState.Pressed)
            {
                leftClickEvent?.Invoke();
            }
        }

        public void MouseClickRight()
        {
            if (CurrentMouseState.RightButton == ButtonState.Released && PreviousMouseState.RightButton == ButtonState.Pressed)
            {
                rightClickEvent?.Invoke();
            }
        }
    }
}
