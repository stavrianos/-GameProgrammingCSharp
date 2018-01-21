using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProgrammingAssignment3
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        const int WindowWidth = 800;
        const int WindowHeight = 600;

        Random rand = new Random();
        Vector2 centerLocation = new Vector2(
            WindowWidth / 2, WindowHeight / 2);

        // STUDENTS: declare variables for 3 rock sprites
        Texture2D greenRockSprite;
        Texture2D magentaRockSprite;
        Texture2D whiteRockSprite;

        // STUDENTS: declare variables for 3 rocks
        Rock rock0;
        Rock rock1;
        Rock rock2;

        // delay support
        const int TotalDelayMilliseconds = 1000;
        int elapsedDelayMilliseconds = 0;

        // random velocity support
        const float BaseSpeed = 0.15f;
        Vector2 upLeft = new Vector2(-BaseSpeed, -BaseSpeed);
        Vector2 upRight = new Vector2(BaseSpeed, -BaseSpeed);
        Vector2 downRight = new Vector2(BaseSpeed, BaseSpeed);
        Vector2 downLeft = new Vector2(-BaseSpeed, BaseSpeed);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // change resolution
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
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

            // STUDENTS: Load content for 3 sprites
            greenRockSprite = Content.Load<Texture2D>("greenrock");
            magentaRockSprite = Content.Load<Texture2D>("magentarock");
            whiteRockSprite = Content.Load<Texture2D>("whiterock");
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

            // STUDENTS: update rocks
            //update each rock if it exists by calling the Update method
            if (rock0 != null)
            {
                rock0.Update(gameTime);
            }
            if (rock1 != null)
            {
                rock1.Update(gameTime);
            }
            if (rock2 != null)
            {
                rock2.Update(gameTime);
            }

            // update timer
            elapsedDelayMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsedDelayMilliseconds >= TotalDelayMilliseconds)
            {
                // STUDENTS: timer expired, so spawn new rock if fewer than 3 rocks in window
                // Call the GetRandomRock method to do this
                if (rock0 == null)
                {
                    rock0 = GetRandomRock();
                }
                else if (rock1 == null)
                {
                    rock1 = GetRandomRock();
                }
                else if (rock2 == null)
                {
                    rock2 = GetRandomRock();
                }
                // restart timer
                elapsedDelayMilliseconds = 0;
            }

            // STUDENTS: Check each rock to see if it's outside the window. If so
            // spawn a new random rock for it by calling the GetRandomRock method
            // Caution: Only check the property if the variable isn't null
            if (rock0 != null && rock0.OutsideWindow)
            {
                rock0 = GetRandomRock();
            }
            if (rock1 != null && rock1.OutsideWindow)
            {
                rock1 = GetRandomRock();
            }
            if (rock2 != null && rock2.OutsideWindow)
            {
                rock2 = GetRandomRock();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // STUDENTS: draw rocks
            spriteBatch.Begin();
           
            //draw existing rocks
            if (rock0 != null)
            {
                rock0.Draw(spriteBatch);
            }
            if (rock1 != null)
            {
                rock1.Draw(spriteBatch);
            }
            if (rock2 != null)
            {
                rock2.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Gets a rock with a random sprite and velocity
        /// </summary>
        /// <returns>the rock</returns>
        private Rock GetRandomRock()
        {
            // STUDENTS: Uncomment and complete the code below to randomly pick a rock sprite by calling the GetRandomSprite method
            //Texture2D sprite = ;
            Texture2D sprite = GetRandomSprite();

            // STUDENTS: Uncomment and complete the code below to randomly pick a velocity by calling the GetRandomVelocity method
            //Vector2 velocity = ;
            Vector2 velocity = GetRandomVelocity();

            // STUDENTS: After completing the two lines of code above, delete the following two lines of code
            // They're only included so the code I provided to you compiles


            // return a new rock, centered in the window, with the random sprite and velocity
            return new Rock(sprite, centerLocation, velocity, WindowWidth, WindowHeight);
        }

        /// <summary>
        /// Gets a random sprite
        /// </summary>
        /// <returns>the sprite</returns>
        private Texture2D GetRandomSprite()
        {
            // STUDENTS: Uncommment and modify the code below as appropriate to return 
            // a random sprite
            int spriteNumber = rand.Next(0, 3);
            switch (spriteNumber)
            {
                case 0:
                    return greenRockSprite;
                case 1:
                    return magentaRockSprite;
                default:
                    return whiteRockSprite;
            }

            // STUDENTS: After completing the code above, delete the following line of code
            // It's only included so the code I provided to you compiles
        }

        /// <summary>
        /// Gets a random velocity
        /// </summary>
        /// <returns>the velocity</returns>
        private Vector2 GetRandomVelocity()
        {
            // STUDENTS: Uncommment and modify the code below as appropriate to return 
            // a random velocity
            int velocityNumber = rand.Next(0, 4);
            switch (velocityNumber)
            {
                case 0:
                    return upLeft;
                case 1:
                    return upRight;
                case 2:
                    return downRight;
                default:
                    return downLeft;
            }

            // STUDENTS: After completing the code above, delete the following line of code
            // It's only included so the code I provided to you compiles
        }
    }
}
