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
    class Candy : Sprite
    {

        public bool pickedUp = false;

        public Candy(ContentManager content, int x, int y, int height, int width) : base(content, x, y, height, width)
        {
            //Console.WriteLine("Candy");
            this.HitBox = new Rectangle(x, y, spriteWidth, spriteHeight);
        }

        public override void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("Candy/Candy.png");
        }

    }


}
