using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Audio;

namespace Eteephonehome
{
    
    class StatusBar
    {

        protected string level_name;
        protected Texture2D background,firestarter,powerup;
        protected SpriteFont font,smallfont;
        bool powered_up,waiting = false;
        private MouseState oldState;
        //what each powerup takes
        int valueStamina = 1;
        int stamina = 0;
        protected int start_x, start_y, dimension, max_stamina,width,menu_x,menu_y_small,menu_y_big,oldStamina,curIcon;
        protected List<statusbaritem> staminaicons = new List<statusbaritem>();
        public Player player { get; set; }
        public StatusBar(int start_x,int start_y,int width, int dimension, int max_stamina, string level_name)
        {
            this.oldStamina = 0;
            this.dimension = dimension;
            this.width = width;
            this.max_stamina = max_stamina;
            this.level_name = level_name;
            this.start_x = start_x;
            this.start_y = start_y;
            this.powered_up = false;
            curIcon = 0;

        }
        public void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>("Status Bar/Status Bar.png");
            int numPixels = 75;
            int x = 294, y = start_y + 10;
            int size = 40;
            for (int i = 0; i < max_stamina; i++)
            {
                staminaicons.Add(new statusbaritem(convert_x(x), start_y + 10, this, size));
                x += numPixels;
            }

            this.font = content.Load<SpriteFont>("Fonts/emulogic");
            this.smallfont = content.Load<SpriteFont>("Fonts/emusmall");
            this.firestarter = content.Load<Texture2D>("Pickups/FirePower.png");
            //calculate y for centered menu item for both fonts
            menu_y_big = (font.LineSpacing - dimension) / -2;
            menu_y_small = (smallfont.LineSpacing - dimension) / -2;
            foreach (statusbaritem sbi in staminaicons)
            {
                sbi.LoadContent(content);
            }
        }

        public void Update(GameTime gameTime,Player player)
        {
            //Console.WriteLine(player.stamina);
            switch(player.getPowerUp())
            {
                    //invisibility power up
                case 1:
                    //powerup = invisible;
                    //powered_up = true;
                    break;
                case 2:
                    powerup = firestarter;
                    powered_up = true;
                    break;
                case 3:
                    //powerup = greenthumb
                    //powered_up = true;
                    break;
                default:
                    powered_up = false;
                    break;
            }
            int stamina = player.stamina;
            if (stamina > oldStamina)
            {
                //.WriteLine("stamina up!");
                int diff = (stamina - oldStamina) / valueStamina;
                for(int i = curIcon;i < curIcon + diff;i++)
                {
                    staminaicons[i].setStatus(0);
                    //Console.WriteLine("setting status");
                }
                curIcon = curIcon + diff;
                if (curIcon > max_stamina-1)
                    curIcon = max_stamina-1;
            }
            if(stamina < oldStamina)
            {
                //Console.WriteLine("Stamina down");

                int diff = (oldStamina - stamina) / valueStamina;
                for(int i = curIcon;i>curIcon-diff;i--)
                {
                    staminaicons[i].setStatus(1);
                }
                curIcon = curIcon - diff;
                if (curIcon < 0)
                    curIcon = 0;
            }
            oldStamina = stamina;
            foreach(statusbaritem x in staminaicons)
            {
                x.Update(gameTime);
            }
            

        }
        public void Draw(SpriteBatch sb)
        {
            Vector2 size1 = smallfont.MeasureString("Power");
            int power_width = (int)size1.X;
            sb.Draw(background, new Rectangle(start_x, start_y,width , dimension), Color.White);
            
            sb.DrawString(font, level_name, convert_coords(30,menu_y_big), Color.Black);
            sb.DrawString(font, "Power:", convert_coords(663, menu_y_big), Color.Black);
            
            if (powered_up)
            {
                int y_pos = (powerup.Height - dimension) / -2;
                float scale = dimension / powerup.Height;
                sb.Draw(powerup, new Rectangle(850,start_y+y_pos,(int)(48),(int)(48)), Color.White);
            }
            foreach(statusbaritem x in staminaicons)
            {
                x.Draw(sb);
            }
        }
        protected Vector2 convert_coords(int x, int y)
        {
            //convert to be relative to scaled height and dimension
            int new_x = (int)(x / (float)background.Width * width);
            int new_y = (int)(y / (float)background.Height * dimension);
            //add x and y to offset down to stamina bar
            new_x += start_x;
            new_y += start_y;
            return new Vector2(new_x, new_y);

        }
        protected int convert_x(int x)
        {
            return start_x + (int)(x / (float)background.Width * width);
        }
        protected int convert_y(int y)
        {
            return start_y + (int)(y / (float)background.Height * dimension);
        }

    }



}
