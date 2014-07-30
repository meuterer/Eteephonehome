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
    abstract class AnimatedSprite : Sprite
    {

        #region Variables
        protected int currentDirectionFrame;
        protected int currentwalkingframe;
        protected int pauseIntervalInFrames;
        protected int frameIntervalDuration;
        protected List<List<Texture2D>> animationframes;
        protected bool moving;
        protected bool reaching;
        #endregion

        public AnimatedSprite()
        {

        }

        public AnimatedSprite(ContentManager content, int x, int y, int height, int width)
            : base(content, x, y, height, width)
        {
            currentDirectionFrame = 2;
            currentwalkingframe = 0;
            pauseIntervalInFrames = 0;
            frameIntervalDuration = 15;
            moving = false;
            reaching = false;
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public new void Update(ref List<Sprite> spriteList)
        {
        }

        public new void Draw(SpriteBatch sb)
        {
            updateAnimation();
            base.Draw(sb);
        }

        public new void Draw(SpriteBatch sb, Color color)
        {
            updateAnimation();
            base.Draw(sb, color);
        }

        protected void updateAnimation()
        {

            if (pauseIntervalInFrames > 0)
            {
                pauseIntervalInFrames--;
                return;
            }
            else if (moving)
            {
                updateCurrentWalkingFrame(true, 1);
            }
            // this needs to come after (moving) . . .
            else if (reaching)
            {
                currentwalkingframe = 2;
            }
            else
            {
                currentwalkingframe--;
                if (currentwalkingframe < 0)
                {
                    currentwalkingframe = 0;
                }
            }
            currentDirectionFrame = updateDirectionFrame(movedX, movedY);
            image = animationframes[currentDirectionFrame][currentwalkingframe];
            pauseIntervalInFrames = frameIntervalDuration;
        }

        protected abstract int updateDirectionFrame(int movedX, int movedY);

        private void updateCurrentWalkingFrame(bool walking, int offset)
        {
            if (walking)
            {
                currentwalkingframe++;
                // TEMPORARY HACK THAT WILL PROBABLY NEVER GET CHANGED:
                if (currentwalkingframe >= (animationframes[0].Count - offset) || currentwalkingframe < 0)
                {
                    currentwalkingframe = 0;
                    return;
                }
            }
            else if (currentwalkingframe > 0)
            {
                currentwalkingframe--;
            }
        }


    }
}