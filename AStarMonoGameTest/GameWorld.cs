using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace AStarMonoGameTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameWorld : Game
    {
        private static List<GameObject> gameObjects = new List<GameObject>();
        public static float scale = 3f;
        public static float cellSize = 32 * scale;
        public static int cellRowCount = 10;
        private GridManager grid;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 1000;
     
            graphics.ApplyChanges();

            IsMouseVisible = true;

            grid = new GridManager(cellRowCount, cellSize);

            gameObjects.AddRange(grid.CreateGrid());
           

            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Enemy e = new Enemy(new Vector2(0, 0));

            gameObjects.Add(e);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Asset.LoadContent(Content);

            foreach (GameObject gO in gameObjects)
            {
                gO.LoadContent(Content);
            }
            Node tmpStart = new Node(new Vector2(5,5),NodeType.Empty,true);
            Node tmpGoal = new Node(new Vector2(9, 9), NodeType.Empty, true);

            /*
            foreach (GameObject nO in gameObjects)
            {
                if (nO.type == NodeType.Start)
                {
                    tmpStart = nO as Node;
                }
                else if (nO.type == NodeType.Goal)
                {
                    tmpGoal = nO as Node;
                }
            }
            */
            
            

             e.Path = grid.FindPath(grid.Nodes[0, 0], grid.Nodes[9, 9]);
            
    
           // grid.FindPath(grid.Nodes[0, 0], grid.Nodes[9, 9]);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (GameObject gO in gameObjects)
            {
                gO.Update(gameTime);
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            foreach (GameObject gO in gameObjects)
            {
                
                gO.Draw(spriteBatch);
            }
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        
    }
}
