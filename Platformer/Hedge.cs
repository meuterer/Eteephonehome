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
    class Hedge : Sprite
    {

        ContentManager content;

        protected int isBurning = 0;
        protected int burningDuration = 90;
        protected bool isGrowable = false;

        public int currentHedgeMode = 0;
        Texture2D[] hedgeModes = new Texture2D[6];

        public Hedge(ContentManager content, int x, int y, int height, int width, int type)
            : base(content, x, y, height, width)
        {
            currentHedgeMode = type;
            this.content = content;
            this.HitBox = new Rectangle(x, y, width, height);
            hedgeModes[0] = content.Load<Texture2D>("Level1Map/empty.png");
            hedgeModes[1] = content.Load<Texture2D>("Level1Map/Burnable_hedge.png");
            hedgeModes[2] = content.Load<Texture2D>("Level1Map/Burning_hedge.png");
            hedgeModes[3] = content.Load<Texture2D>("Level1Map/Burnt_hedge.png");
            hedgeModes[4] = content.Load<Texture2D>("Bushes/bushes.png");
            hedgeModes[5] = content.Load<Texture2D>("ET Sprites/ET_hiding.png");
            image = hedgeModes[currentHedgeMode];
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public void Update(int i)
        {
            
            if (isBurning == 1)
            {
                image = hedgeModes[3];
                HitBox = new Rectangle(0, 0, 0, 0);
            }
            if (isBurning > 0)
            {
                Console.Write("ON FIRE");
                isBurning--;
            }
        }

        public int getHedgeMode()
        {
            return currentHedgeMode;
        }

        public void burnHedge()
        {
            isBurning = burningDuration;
            image = hedgeModes[2];
            Console.WriteLine(burningDuration + " " + isBurning);
        }

        public void hideCharacter(bool input)
        {
            if (input && currentHedgeMode == 4)
            {
                currentHedgeMode = 5;
            }
            if (!input && currentHedgeMode == 5)
            {
                currentHedgeMode = 4;
            }
            image = hedgeModes[currentHedgeMode];
        }

    }


}
