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
        public static MouseHandler mouseHandler;

        #region Lists
        public static List<GameObject> GameObjects { get => gameObjects; set => gameObjects = value; }
        private static List<GameObject> newObjects = new List<GameObject>();
        private static List<GameObject> deleteObjects = new List<GameObject>();
        private static List<GameObject> gameObjects = new List<GameObject>();
        #endregion

        #region Fields
        public static float scale = 3f;
        public static float cellSize = 32 * scale;

        public static int cellRowCount = 10;
        private static int wave = 1;
        private static int waveCounter;

        private static Stack<Node> path;

        private static GridManager gridManager;

        private static TimeSpan timer;

        private static bool startWave;
        public static int projectsDone;
        public static int failedProjects;
        #endregion


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
            graphics.PreferredBackBufferWidth = 1500;
            graphics.PreferredBackBufferHeight = 1000;
            timer = new TimeSpan(0, 0, 0, 0, 0);
            graphics.ApplyChanges();

            IsMouseVisible = true;
            mouseHandler = new MouseHandler();


            gridManager = new GridManager(cellRowCount, cellSize);

            GameObjects.AddRange(gridManager.CreateGrid());

            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Asset.LoadContent(Content);

            GameObjects.Add(new UI(Asset.wall, new Vector2(11 * 96, 1 * 96)));


            foreach (GameObject gO in GameObjects)
            {
                gO.LoadContent(Content);
            }

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

            mouseHandler.Update();

            GameObjects.AddRange(newObjects);
            newObjects.Clear();
            deleteObjects.Clear();

            if (Keyboard.HasBeenPressed(Keys.S))
            {
                startWave = true;
            }

            if (startWave == true)
            {
                //the timer starts "running"
                timer -= gameTime.ElapsedGameTime;
                if (timer <= TimeSpan.Zero)
                {
                    //once the timer hits zero, its runs the startsWave method
                    //and resets the timer
                    timer = new TimeSpan(0, 0, 0, 0, 500);
                    StartWave(gameTime);

                }
            }

            foreach (GameObject gO in GameObjects)
            {
                gO.Update(gameTime);
            }

            foreach (GameObject gO in deleteObjects)
            {
                GameObjects.Remove(gO);

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
            foreach (GameObject gO in GameObjects)
            {
                spriteBatch.DrawString(Asset.spriteFont, $"Wave: {wave}", new Vector2(11 * 96, 0 * 96), Color.DarkRed, 0, Vector2.Zero, 5, SpriteEffects.None, 0.92f);
                spriteBatch.DrawString(Asset.spriteFont, $"Failed projects: {failedProjects}", new Vector2(11 * 96, 2 * 96), Color.DarkRed, 0, Vector2.Zero, 3, SpriteEffects.None, 0.92f);
                spriteBatch.DrawString(Asset.spriteFont, $"Projects done: {projectsDone}", new Vector2(11 * 96, 3 * 96), Color.DarkRed, 0, Vector2.Zero, 3, SpriteEffects.None, 0.92f);
                gO.Draw(spriteBatch);
            }
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public static void StartWave(GameTime gameTime)
        {
            //hvor mange enemies der er allerede tilføjet i den nuværende wave
            waveCounter++;

            //laver en sti til enemies
            path = gridManager.FindPath(gridManager.Nodes[0, 5], gridManager.Nodes[9, 5]);

            //så længe antallet af enemies ikke overstiger wave counter
            if (waveCounter <= wave)
            {
                newObjects.Add(new Enemy(new Vector2(0 * 96, 5 * 96), path));

            }
            else
            {
                //if the number of enemies equals the wavenumber
                wave++;

                //resets the counter
                waveCounter = 0;

                //stops the Startwave function from running
                startWave = false;

                //resets the timer
                timer = new TimeSpan(0, 0, 0, 0, 0);
            }


        }

        public static void Instantiate(GameObject gO)
        {
            newObjects.Add(gO);
        }

        public static void Destroy(GameObject gO)
        {
            deleteObjects.Add(gO);
        }
    }
}
