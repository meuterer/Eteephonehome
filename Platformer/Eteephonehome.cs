#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Tao.Sdl;
using TiledSharp;
#endregion

namespace Eteephonehome
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Eteephonehome : Game
    {

        protected int x_resolution;
        protected int y_resolution;
        protected int tile_dimension;
        protected int status_bar_dimension = 75;
        LevLoader map;
        Menu main_menu;
        public string curScreen;
        
        Controls controls;
        SpriteBatch spriteBatch;
        GraphicsDeviceManager graphics;

        public Eteephonehome(int x_resolution, int y_resolution, int tile_dimension)
        {
            this.x_resolution = x_resolution;
            this.y_resolution = y_resolution;
            this.tile_dimension = tile_dimension;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            main_menu = new Menu("Main", true, "emulogic", 960, 715, this);

        }

        protected override void Initialize()
        {
            map = new LevLoader(this.Content, graphics, tile_dimension, tile_dimension, x_resolution, y_resolution);
            graphics.PreferredBackBufferWidth = x_resolution;
            graphics.PreferredBackBufferHeight = y_resolution;
            graphics.ApplyChanges();
            base.Initialize();
            Joystick.Init();
            curScreen = "menu";
            controls = new Controls();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // the 1 needs to be from input, choosing the current level
            // probably use a method othan than LoadContent to load the levels
            map.loadLevel(this.Content, 1);

            main_menu.LoadContent(this.Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            controls.Update();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            if (curScreen == "game")
                map.Update(controls, gameTime);
            if (curScreen == "menu")
                main_menu.Update(controls, gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();

            if (curScreen == "game")
                map.Draw(spriteBatch);
            if (curScreen == "menu")
                main_menu.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

}

