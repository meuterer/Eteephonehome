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
    abstract class Sprite
    {

        public abstract void LoadContent(ContentManager content);

        public Rectangle HitBox { get; set; }

        public Rectangle UpperHitBox;

        protected int x_max = 960, y_max = 640;

        protected int movedX = 0;
        protected int movedY = 0;
        protected int spriteX { get; set; }
        protected int spriteY { get; set; }
        protected int spriteWidth { get; set; }
        protected int spriteHeight { get; set; }
        protected Texture2D image;

        // redundant code?
        public int getX() { return spriteX; }
        public int getY() { return spriteY; }
        public void setX(int x) { spriteX = x; }
        public void setY(int y) { spriteY = y; }

        public int keepWithinBounds(int value, int min, int max)
        {
            if (value < min)
            {
                value = min;
            }
            else if (value > max)
            {
                value = max;
            }
            return value;
        }

        public void Update()
        {
        }

        public void Update(ref List<Sprite> spriteList)
        {
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(image, new Rectangle(spriteX, spriteY, spriteWidth, spriteHeight), Color.White);
        }

        public void Draw(SpriteBatch sb, Color color)
        {
            sb.Draw(image, new Rectangle(spriteX, spriteY, spriteWidth, spriteHeight), color);
        }

        public Sprite()
        {
        }

        public Sprite(ContentManager content, int x, int y, int height, int width)
        {
            setX(x);
            setY(y);
            this.spriteHeight = height;
            this.spriteWidth = width;
            updateHitBox(x, y, height, width);
            LoadContent(content);
        }

        public void updateHitBox(int x, int y, int height, int width)
        {
            this.HitBox = new Rectangle(x + 1, y + 1, width - 2, height - 2);
            this.UpperHitBox = new Rectangle(x, y - height - 60, width, height);
        }



    }

}



