using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarMonoGameTest
{
    public static class Keyboard
    {
        private static KeyboardState currentKeyState;
        private static KeyboardState previousKeyState;

        public static KeyboardState GetState()
        {
            previousKeyState = currentKeyState;
            currentKeyState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
            return currentKeyState;
        }

        /// <summary>
        /// Checks if key is held down
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsPressed(Keys key)
        {
            return currentKeyState.IsKeyDown(key);
        }


        ///// <summary>
        ///// Checks if key is being pressed (It triggers once)
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        public static bool HasBeenPressed(Keys key)
        {
            return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
        }
    }
}
