#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Audio;
#endregion

namespace Eteephonehome
{
    class Menu
    {
        Texture2D Background;
        SpriteFont font;
        string[] MenuOptions;
        string Name;
        bool HasBackground;
        string FontName;
        int selected = 0;
        SoundEffect ScrollSound;
        SoundEffect SelectionSound;
        private Eteephonehome game;
        int StaX, StaY;
        int WinHeight;
        int WinWidth;
        public int spacing { get; set; }

        #region Constructor
        //Take in name of menu to load in pertinent things in load content and whether or not it has an image
        public Menu(string MenuName, bool HasImage, string Font, int x_res, int y_res, Eteephonehome game)
        {
            MenuOptions = new string[3] { "Start", "Resume", "Quit" };
            Name = MenuName;
            HasBackground = HasImage;
            FontName = Font;
            WinWidth = x_res;
            WinHeight = y_res;
            StaX = 426;
            StaY = 286;
            spacing = 70;
            this.game = game;

        }
        #endregion

        #region Loads the content we need
        public new void LoadContent(ContentManager content)
        {
            ScrollSound = content.Load<SoundEffect>("Sound/scrollsound");
            SelectionSound = content.Load<SoundEffect>("Sound/selectsound");
            if (HasBackground)
            {
                Console.WriteLine("/Menus/" + Name + "Menu.png");
                Background = content.Load<Texture2D>("Menus/" + Name + "Menu.png");
            }
            //StaX = (int)(StaX / (float)Background.Width * WinWidth);

            //StaY = StaY * WinHeight / Background.Height;

            Console.Write(StaX + " " + StaY);
            font = content.Load<SpriteFont>("Fonts/" + FontName);
        }
        #endregion

        #region Updates the menu in our gameloop
        public void Update(Controls controls, GameTime gameTime)
        {
            if (controls.onRelease(Keys.Down, Buttons.DPadDown))
            {
                ScrollSound.Play();
                selected++;
                if (selected > MenuOptions.Length - 1)
                    selected = 0;
            }

            if (controls.onRelease(Keys.Up, Buttons.DPadUp))
            {
                ScrollSound.Play();
                selected--;
                if (selected < 0)
                    selected = MenuOptions.Length - 1;
            }
            if (controls.onPress(Keys.Enter, Buttons.A))
            {

                SelectionSound.Play();
                switch (selected)
                {
                    case 0:
                        game.curScreen = "game";
                        break;
                    case 1:

                        break;
                    case 2: game.Exit();
                        break;
                    default:
                        break;


                }
            }

        }
        #endregion

        #region Draw the menu
        public new void Draw(SpriteBatch sb)
        {
            if (HasBackground)
                sb.Draw(Background, new Rectangle(0, 0, WinWidth, WinHeight), Color.White);
            Color col;
            int CurX = StaX;
            int CurY = StaY;
            for (int i = 0; i < MenuOptions.Length; i++)
            {
                if (i == selected)
                    col = Color.Pink;
                else
                    col = Color.Black;
                sb.DrawString(font, MenuOptions[i], new Vector2(CurX, CurY), col);
                CurY += spacing;
            }
        }
        #endregion

    }

}
