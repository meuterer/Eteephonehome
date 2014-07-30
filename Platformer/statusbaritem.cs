using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Tao.Sdl;

namespace Eteephonehome
{
    class statusbaritem
    {
        private StatusBar parent;
        private bool appearing, vanishing;
        private bool visible;
        private int x, y,scale,curScale;
        private Texture2D image;

        public statusbaritem(int x,int y,StatusBar parent,int scale)
        {
            Console.WriteLine(x + " " + y);
            this.parent = parent;
            this.x = x;
            this.y = y;
            this.visible = false;
            this.curScale = 0;
            this.scale = scale;
        }
        
        public  void LoadContent(ContentManager content)
        {
            this.image = content.Load<Texture2D>("Candy/Candy.png");
        }
        public void Update(GameTime time)
        {
            if(vanishing)
            {
                visible = false;
                vanishing = false;
            }
            if(appearing)
            {
                visible = true;
                appearing = false;
                // animate in
            }

        }
        public void Draw(SpriteBatch sb)
        {
            if (visible)
            {
                //Console.WriteLine("wat");
                sb.Draw(image, new Rectangle(x, y, scale, scale), Color.White);
            }
        }
        public void setStatus(int status)
        {
            //new icon
            if (status == 0)
                appearing = true;
            if (status == 1)
                vanishing = true;
        }
    }
}
