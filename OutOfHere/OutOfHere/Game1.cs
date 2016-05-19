#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
#endregion

namespace _300hoursMARK2
{
    enum Scene
    {
        Game,
        GameOver,
        Menu
    }
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    class Game1 : Game
    {

        public const int ScreenWidth = 400;
        public const int ScreenHeight = 240;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        CInput cInput;

        //FPS counting varibles
        bool FPSEnable = false;
        int FPSCount = 0;
        double FPSGameTime = 0;

        //Manager
        public static Scene sceneManager;
        //Game
        GameManager gameManager;
        GameOverManager gameOverManager;


        public Game1()
            : base()
        {
            
            graphics = new GraphicsDeviceManager(this);
            Resolution.Init(ref graphics);
            Content.RootDirectory = "Content";

            // Change Virtual Resolution 
            Resolution.SetVirtualResolution(ScreenWidth, ScreenHeight);

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
            cInput = new CInput(InputType.Keyboard);
            Resolution.SetResolution(ScreenWidth * 2,ScreenHeight * 2, false);
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

            // TODO: use this.Content to load your game content here
            sceneManager = Scene.Game;
            gameManager = new GameManager(Content);
            gameOverManager = new GameOverManager(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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

        protected override void Update(GameTime gt)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            // TODO: Add your update logic here



            cInput.Update();
            switch (sceneManager)
            {
                case (Scene.Game):
                    gameManager.Update(gt, cInput);
                    break;
                case (Scene.GameOver):
                    break;
                default:
                    Exit();
                    break;

            }
            if (cInput.GetStateKeyboard().IsKeyDown(Keys.F1) && CInput.LastCInput.GetStateKeyboard().IsKeyUp(Keys.F1))
            {
                FPSEnable = !FPSEnable;
                Console.WriteLine("FPS: " + FPSEnable.ToString());
            }
            CInput.LastCInput.Update();

            base.Update(gt);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.Gray);
            Resolution.BeginDraw();

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Immediate,
                    BlendState.AlphaBlend,
                    SamplerState.PointClamp,
                    null,
                    null,
                    null, Resolution.getTransformationMatrix());
            switch (sceneManager)
            {
                case (Scene.Game):
                    gameManager.Draw(spriteBatch);
                    break;
                case (Scene.GameOver):
                    gameOverManager.Draw(spriteBatch);
                    break;
                default:
                    Exit();
                    break;

            }
            spriteBatch.End();

            //FPS Counter
            if (FPSEnable)
            {
                if (FPSGameTime >= 1000)
                {
                    Console.WriteLine(FPSCount);
                    FPSGameTime = 0;
                    FPSCount = 0;
                }
                FPSGameTime += gameTime.ElapsedGameTime.TotalMilliseconds;
                FPSCount++;
            }

            base.Draw(gameTime);
        }
    }
}
