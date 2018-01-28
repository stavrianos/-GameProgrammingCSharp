using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject
{
    /// <summary>
    /// A burger
    /// </summary>
    public class Burger
    {
        #region Fields

        // graphic and drawing info
        Texture2D sprite;
        Rectangle drawRectangle;

        // burger stats
        int health = 100;

        // shooting support
        bool canShoot = true;
        int elapsedCooldownTime = 0;

        // sound effect
        SoundEffect shootSound;

        #endregion

        #region Constructors

        /// <summary>
        ///  Constructs a burger
        /// </summary>
        /// <param name="contentManager">the content manager for loading content</param>
        /// <param name="spriteName">the sprite name</param>
        /// <param name="x">the x location of the center of the burger</param>
        /// <param name="y">the y location of the center of the burger</param>
        /// <param name="shootSound">the sound the burger plays when shooting</param>
        public Burger(ContentManager contentManager, string spriteName, int x, int y,
            SoundEffect shootSound)
        {
            LoadContent(contentManager, spriteName, x, y);
            this.shootSound = shootSound;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the collision rectangle for the burger
        /// </summary>
        public Rectangle CollisionRectangle
        {
            get { return drawRectangle; }
        }

        public int Health
        {
            get { return health; }
            set
            {
                if (health >= 0 && health <= 100)
                {
                    health = value;
                }
            }
        }

        #endregion

        #region Private properties

        /// <summary>
        /// Gets and sets the x location of the center of the burger
        /// </summary>
        private int X
        {
            get { return drawRectangle.Center.X; }
            set
            {
                drawRectangle.X = value - drawRectangle.Width / 2;

                // clamp to keep in range
                if (drawRectangle.Left < 0)
                {
                    drawRectangle.X = 0;
                }
                else if (drawRectangle.Right > GameConstants.WindowWidth)
                {
                    drawRectangle.X = GameConstants.WindowWidth - drawRectangle.Width;
                }
            }
        }

        /// <summary>
        /// Gets and sets the y location of the center of the burger
        /// </summary>
        private int Y
        {
            get { return drawRectangle.Center.Y; }
            set
            {
                drawRectangle.Y = value - drawRectangle.Height / 2;

                // clamp to keep in range
                if (drawRectangle.Top < 0)
                {
                    drawRectangle.Y = 0;
                }
                else if (drawRectangle.Bottom > GameConstants.WindowHeight)
                {
                    drawRectangle.Y = GameConstants.WindowHeight - drawRectangle.Height;
                }
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Updates the burger's location based on mouse. Also fires 
        /// french fries as appropriate
        /// </summary>
        /// <param name="gameTime">game time</param>
        /// <param name="mouse">the current state of the mouse</param>
        public void Update(GameTime gameTime, KeyboardState keyboard)
        {
            // burger should only respond to input if it still has health
            if (this.health > 0)
            {
                // move burger using mouse
                //X = mouse.X;
                //Y = mouse.Y;

                if (keyboard.IsKeyDown(Keys.W))
                {
                    Y -= GameConstants.BurgerMovementAmount;
                }
                if (keyboard.IsKeyDown(Keys.S))
                {
                    Y += GameConstants.BurgerMovementAmount;
                }
                if (keyboard.IsKeyDown(Keys.A))
                {
                    X -= GameConstants.BurgerMovementAmount;
                }
                if (keyboard.IsKeyDown(Keys.D))
                {
                    X += GameConstants.BurgerMovementAmount;
                }

                // clamp burger in window
                // this is done in the X and Y setter methods

                // update shooting allowed
                if (!canShoot)
                {
                    this.elapsedCooldownTime += gameTime.ElapsedGameTime.Milliseconds;
                }

                //shooting allowed when time is elapsed or mouse button released
                //if (this.elapsedCooldownTime >= GameConstants.BURGER_COOLDOWN_MILLISECONDS || mouse.LeftButton == ButtonState.Released)
                //{
                //    canShoot = true;
                //    this.elapsedCooldownTime = 0;
                //}
                if (this.elapsedCooldownTime >= GameConstants.BurgerTotalCooldownMilliseconds || keyboard.IsKeyUp(Keys.Space))
                {
                    canShoot = true;
                    this.elapsedCooldownTime = 0;
                }

                // timer concept (for animations) introduced in Chapter 7

                // shoot if appropriate

                if (keyboard.IsKeyDown(Keys.Space) && canShoot)
                {
                    canShoot = false;
                    Projectile projectile = new Projectile(ProjectileType.FrenchFries,
                        Game1.GetProjectileSprite(ProjectileType.FrenchFries),
                        X,
                        Y - GameConstants.FrenchFriesProjectileOffset,
                        GameConstants.FrenchFriesProjectileSpeed);
                    Game1.AddProjectile(projectile);

                    //play shoot sound
                    this.shootSound.Play();
                }
            }

        }

        /// <summary>
        /// Draws the burger
        /// </summary>
        /// <param name="spriteBatch">the sprite batch to use</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.sprite, this.drawRectangle, Color.White);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads the content for the burger
        /// </summary>
        /// <param name="contentManager">the content manager to use</param>
        /// <param name="spriteName">the name of the sprite for the burger</param>
        /// <param name="x">the x location of the center of the burger</param>
        /// <param name="y">the y location of the center of the burger</param>
        private void LoadContent(ContentManager contentManager, string spriteName,
            int x, int y)
        {
            // load content and set remainder of draw rectangle
            sprite = contentManager.Load<Texture2D>(spriteName);
            drawRectangle = new Rectangle(x - sprite.Width / 2,
                y - sprite.Height / 2, sprite.Width,
                sprite.Height);
        }

        #endregion
    }
}
