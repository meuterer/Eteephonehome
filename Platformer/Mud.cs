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
    class Mud : Sprite
    {

        public Mud(ContentManager content, int x, int y, int height, int width) : base(content, x, y, height, width)
        {
        }

        public override void LoadContent(ContentManager content)
        {
            //Console.WriteLine("Baseball");
            image = content.Load<Texture2D>("hitbox.png");
        }

    }
}
