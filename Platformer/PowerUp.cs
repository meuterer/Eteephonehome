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
    class PowerUp : Sprite
    {

        public int powerUpType { get; set; }

        public PowerUp(ContentManager content, int x, int y, int height, int width, int type)
        {
            setX(x);
            setY(y);
            this.spriteHeight = height;
            this.spriteWidth = width;
            powerUpType = type;
            updateHitBox(x, y, height, width);
            LoadContent(content);
        }

        public override void LoadContent(ContentManager content)
        {
            switch (powerUpType)
            {
                case 1:
                    //invisible
                    //image = content.Load<Texture2D>("Level1Map/FirePower.png");
                    break;
                case 2:
                    image = content.Load<Texture2D>("Level1Map/FirePower.png");
                    break;
                case 3: 
                    //greenthumb
                    //image = content.Load<Texture2D>("Level1Map/FirePower.png");
                    break;
                default:
                    break;
            }
        }

    }
}
