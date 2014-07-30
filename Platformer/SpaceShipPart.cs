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
    class SpaceShipPart : Sprite
    {

        public SpaceShipPart(ContentManager content, int x, int y, int height, int width) : base(content, x, y, 48, 48)
        {
            this.HitBox = new Rectangle(x, y, width, height);
        }

        public override void LoadContent(ContentManager content)
        {
            this.image = content.Load<Texture2D>("Spaceship/Spaceship.png");
        }
        
    }
}
