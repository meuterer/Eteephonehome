using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace Eteephonehome
{
    abstract class MovingSprite : AnimatedSprite
    {

        protected abstract bool handleCollision(Sprite sprite);

        #region Variables
        protected int speed;
        protected int turningDirection;
        protected int turningCounter;
        protected int turningCounterTime;
        protected int currentHold;

        protected int holdLength;
        protected int defaultSpeed;
        protected int mudSpeed;
        protected int diagonalSpeed;
        #endregion

        public MovingSprite()
        {

        }

        public MovingSprite(ContentManager content, int x, int y, int height, int width)
            : base(content, x, y, height, width)
        {
            movedX = 0;
            movedY = 0;
            turningDirection = 0;
            turningCounter = 0;
            turningCounterTime = 30;
            currentHold = 0;
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public new void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }

        public new void Update(ref List<Sprite> spriteList)
        {
            Move();
            base.Update(ref spriteList);
        }

        protected new void updateAnimation()
        {
            base.updateAnimation();
        }

        public void Move()
        {
            if (movedX != 0 || movedY != 0)
            {
                moving = true;
            }
            else
            {
                moving = false;
            }
        }

        protected bool isMovementHoldActive()
        {
            if (currentHold < 1)
            {
                return false;
            }
            else
            {
                currentHold--;
                if (currentHold < 0)
                {
                    return false;
                }
                return true;
            }

        }

        protected void calculateIfDiagonal(int x_value, int y_value)
        {
            if (x_value != 0 && y_value != 0)
            {
                enterDiagonalMode(true);
            }
            else
            {
                enterDiagonalMode(false);
            }
        }

        protected void enterDiagonalMode(bool input)
        {
            if (input)
            {
                speed = diagonalSpeed;
            }
            else
            {
                speed = defaultSpeed;
            }
        }

        protected void detectCollisions(ref List<Sprite> spriteList)
        {
            //spriteX = keepWithinBounds(spriteX + (movedX * speed), 0, x_max - spriteWidth);
            //spriteY = keepWithinBounds(spriteY + (movedY * speed), 0, y_max - spriteHeight);
            int index = 0;
            List<int> deleteList = new List<int>();
            updateHitBox(spriteX + movedX * speed, spriteY + movedY * speed, spriteHeight, spriteWidth);
            foreach (Sprite sprite in spriteList)
            {
                if (sprite != null && this.HitBox.Intersects(sprite.HitBox))
                {
                    // if it returns true, delete the given sprite but do not change the array length
                    if (handleCollision(sprite))
                    {
                        deleteList.Add(index);
                    }
                }
                index++;
            }
            foreach (int i in deleteList)
            {
                spriteList[i] = null;
            }
        }

        public void repel()
        {
            movedX = 0;
            movedY = 0;
        }



    }
}
