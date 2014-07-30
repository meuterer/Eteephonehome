using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Audio;
using Tao.Sdl;
using TiledSharp;

namespace Eteephonehome
{
    
    public class LevLoader
    {

        protected int x_tile_size;
        protected int y_tile_size;
        protected int x_resolution;
        protected int y_resolution;

        TmxMap map;
        Player player1;
        Cop cop1;
        bool generated = false;
        ContentManager content;
        Texture2D Background, Pause;
        GraphicsDeviceManager gd;
        List<Sprite> SpriteList = new List<Sprite>();
        SpriteFont font;
        SoundEffectInstance mainmusic, pausemusic;
        bool paused = false;

        StatusBar status;

        public LevLoader(ContentManager content, GraphicsDeviceManager display, int x_tile_size, int y_tile_size, int x_resolution, int y_resolution)
        {
            gd = display;
            this.x_tile_size = x_tile_size;
            this.y_tile_size = y_tile_size;
            this.x_resolution = x_resolution;
            this.y_resolution = y_resolution-75;
            this.player1 = new Player(gd, content, 50, 550, 50, 50);
            this.cop1 = new Cop(content,560,420,50,50);
            this.font = content.Load<SpriteFont>("Fonts/emulogic");
            status = new StatusBar(0, y_resolution - 75, 960, 75, 5, "Level 1");
        }

        public void loadLevel(ContentManager content, int levelNumber)
        {
            String level = "Level1Map/Level" + levelNumber + ".tmx"; // + levelNumber + "
            this.map = new TmxMap(@"Content/" + level);
            if (map == null)
            {
                Console.WriteLine("IT IS NULL");
            }
            //Pause = content.Load<Texture2D>("Menus/PauseMenu.png");
            status.LoadContent(content);
            Background = content.Load<Texture2D>("Level1Map/Level1.png"); //"background" + levelNumber + ".png"
  
            // this only needs to happen once per execution of the program (but now it happens once per level)
            importAllResources(content);
        }
        public void LoadSounds(ContentManager content)
        {
            SoundEffect effect, pausedmusic;
            effect = content.Load<SoundEffect>("Sound/E-Tee Level 1 Music.wav");
            pausedmusic = content.Load<SoundEffect>("Sound/E-Tee Title Screen.wav");
            mainmusic = effect.CreateInstance();
            pausemusic = pausedmusic.CreateInstance();
            pausemusic.IsLooped = true;
            pausemusic.Play();
            //Console.WriteLine(pausemusic.State);

        }
        public void Update(Controls controls, GameTime gameTime)
        {
            if (controls.onPress(Keys.Enter, Buttons.Start))
            {
                paused = !paused;
            }
            if (!paused)
            {
                foreach (Sprite sprite in SpriteList)
                {
                    if (sprite != null)
                    {
                        if ((sprite as Hedge) != null && ((Hedge)sprite).getHedgeMode() == 1)
                            ((Hedge)sprite).Update(0);
                        else
                            sprite.Update(ref SpriteList);
                    }
                }
                player1.Update(controls, gameTime, ref SpriteList);
                cop1.Update(gameTime, ref SpriteList);
                status.Update(gameTime, player1);
            }
        }

        public void Draw(SpriteBatch sb)
        {
            Color color = Color.White;
            if (paused)
            {
                color = Color.Gray;
            }  
            sb.Draw(this.Background, new Rectangle(0, 0, x_resolution, y_resolution), color);
            foreach (Sprite sprite in SpriteList)
            {
                if (sprite != null)
                {
                    sprite.Draw(sb,color);
                }
            }
            status.Draw(sb);
            player1.Draw(sb);
            cop1.Draw(sb);
            if (paused)
            {
                Vector2 size1 = font.MeasureString("Paused");
                sb.DrawString(font, "Paused", new Vector2((int)(x_resolution - size1.X) / 2, (int)(y_resolution - size1.Y) / 2), Color.Black);
            }
        }

        // loads all of the sprite items into SpriteList
        // when they are deleted they leave the space null and all the other sprites remain at the same index
        public void importAllResources(ContentManager content)
        {
            //SpriteList.Add(new Mud(content, 176, 240, 96, 144));
            importResources(content, "Candy");
            importResources(content, "Hedges");
            importResources(content, "Spaceship");
            importResources(content, "FirePower");
            importResources(content, "Mud");
            importResources(content, "Bushes");
            importResources(content, "Water");
            importResources(content, "Trees");
            importResources(content, "Burnable_hedge");
        }

        public void importResources(ContentManager content, string resourceType) 
        {
            int a;

            TmxLayer resources = this.map.Layers[resourceType];

            List<int> checkListX = new List<int>();
            List<int> checkListY = new List<int>();

            foreach (TmxLayerTile tile in resources.Tiles)
            {
                // THIS CODE IS VERY INEFFICIENT - ITERATES THROUGH LIST ONCE FOR EACH POSSIBLE TYPE
                if (tile.Gid != 0)
                {
                    a = 0;
                    if (resourceType == "Candy")
                    {
                        SpriteList.Add(new Candy(content, findXGridPoint(tile.X), findYGridPoint(tile.Y), x_tile_size, y_tile_size));
                    }
                    if (resourceType == "Hedges")
                    {
                       SpriteList.Add(new Hedge(content, findXGridPoint(tile.X), findYGridPoint(tile.Y), x_tile_size, y_tile_size, 0));
                    }                        
                    if (resourceType == "Spaceship")
                    {
                        if (generated == false)
                            SpriteList.Add(new SpaceShipPart(content, findXGridPoint(tile.X), findYGridPoint(tile.Y), x_tile_size, y_tile_size));

                       generated = true;
                    }
                    if (resourceType == "FirePower")
                    {
                        SpriteList.Add(new PowerUp(content, findXGridPoint(tile.X), findYGridPoint(tile.Y), x_tile_size, y_tile_size, 2));
                    }
                    if (resourceType == "Bushes")
                    {
                       SpriteList.Add(new Hedge(content, findXGridPoint(tile.X), findYGridPoint(tile.Y), x_tile_size, y_tile_size, 4));
                    }
                    if (resourceType == "Burnable_hedge")
                    {
                        SpriteList.Add(new Hedge(content, findXGridPoint(tile.X), findYGridPoint(tile.Y), x_tile_size, y_tile_size, 1));
                    }




                    if (resourceType == "Mud")
                    {
                        //SpriteList.Add(new Mud(content, findXGridPoint(tile.X), findYGridPoint(tile.Y), 16, 16));
                    }
                    if (resourceType == "Water")
                    {
                        //SpriteList.Add(new SpaceShipPart(content, findXGridPoint(tile.X), findYGridPoint(tile.Y), 16, 16));
                    }
                    if (resourceType == "Trees")
                    {
                        //SpriteList.Add(new SpaceShipPart(content, findXGridPoint(tile.X), findYGridPoint(tile.Y), 16, 16));
                    }
                    if (resourceType == "Background")
                    {
                        //Texture2D Background = ;
                    }
                }
            }
        }

        public int findXGridPoint(int input)
        {
            return findGridPoint(input, x_tile_size);
        }

        public int findYGridPoint(int input)
        {
            return findGridPoint(input, y_tile_size);
        }

        public int findGridPoint(int input, int tile_size)
        {
            return input * tile_size;
        }

        

    }
}
