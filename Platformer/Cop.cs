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
    class Cop : MovingSprite
    {
        protected int x_direction = 0;
        protected int y_direction = 0;
        protected int directionOnPath;
        protected int position;
        List<Tuple<int, int>> path;

        public Cop(ContentManager content, int x, int y, int width, int height)
        {
            speed = 1;
            directionOnPath = 1;
            position = 0;
            path = new List<Tuple<int, int>>();
            animationframes = new List<List<Texture2D>>();
            this.HitBox = new Rectangle(x, y, spriteWidth, spriteHeight);
            setX(x);
            setY(y);
            this.spriteHeight = height;
            this.spriteWidth = width;
            updateHitBox(x, y, height, width);

            movedX = 1;
            movedY = 1;
            turningDirection = 0;
            turningCounter = 0;
            turningCounterTime = 30;
            currentHold = 0;
            currentDirectionFrame = 2;
            currentwalkingframe = 0;
            pauseIntervalInFrames = 0;
            frameIntervalDuration = 5;
            moving = false;
            reaching = false;
            setX(x);
            setY(y);
            this.spriteHeight = height;
            this.spriteWidth = width;
            //CHANGE INDEX
            updateHitBox(x, y, height, width);
            LoadContent(content);

            // Needed before Move()
            calculateBothDirections();
        }

        public new void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }

        public override void LoadContent(ContentManager content)
        {
            for (int i = 0; i < 5; i++)
            {
                animationframes.Add(new List<Texture2D>());
            }

            //image = content.Load<Texture2D>("ET Sprites/ET_down.png");
            animationframes[0].Add(content.Load<Texture2D>("Cops/cop_up.png"));
            animationframes[0].Add(content.Load<Texture2D>("Cops/cop_up1.png"));
            animationframes[0].Add(content.Load<Texture2D>("Cops/cop_up2.png"));
            animationframes[1].Add(content.Load<Texture2D>("Cops/cop_right.png"));
            animationframes[1].Add(content.Load<Texture2D>("Cops/cop_right1.png"));
            animationframes[1].Add(content.Load<Texture2D>("Cops/cop_right2.png"));
            animationframes[2].Add(content.Load<Texture2D>("Cops/cop_down.png"));
            animationframes[2].Add(content.Load<Texture2D>("Cops/cop_down1.png"));
            animationframes[2].Add(content.Load<Texture2D>("Cops/cop_down2.png"));
            animationframes[3].Add(content.Load<Texture2D>("Cops/cop_left.png"));
            animationframes[3].Add(content.Load<Texture2D>("Cops/cop_left1.png"));
            animationframes[3].Add(content.Load<Texture2D>("Cops/cop_left2.png"));
            //-----temp thing for offset in animatedsprite
            animationframes[4].Add(content.Load<Texture2D>("Cops/cop_left2.png"));

            Tuple<int, int> t1 = new Tuple<int, int>(560, 420);
            Tuple<int, int> t2 = new Tuple<int, int>(560, 320);
            Tuple<int, int> t3 = new Tuple<int, int>(405, 320);
            Tuple<int, int> t4 = new Tuple<int, int>(405, 20);
            Tuple<int, int> t5 = new Tuple<int, int>(240, 20);

            path.Add(t1);
            path.Add(t2);
            path.Add(t3);
            path.Add(t4);
            path.Add(t5);

            base.LoadContent(content);
        }

        public void Update(GameTime gameTime, ref List<Sprite> spriteList)
        {
            Move();
            base.Update(ref spriteList);
            moving = true;

        }












        #region Movement Region

        public new void Move()
        {
            updateListPosition();
            calculateBothDirections();
            movedX = x_direction * speed;
            movedY = y_direction * speed;
            spriteX += movedX;
            spriteY += movedY;
            base.Move();
        }

        public void updateListPosition()
        {
            if (movedX == 0 && movedY == 0)
            {
                position += directionOnPath;
                if (position >= path.Count - 1)
                {
                    currentHold = holdLength;
                    setDirection(path.Count - 1, -1);
                }
                else if (position <= 0)
                {
                    currentHold = holdLength;
                    setDirection(0, 1);
                }
            }
        }

        public void reverseDirection()
        {
            setDirection(position, directionOnPath * -1);
        }

        public void setDirection(int pos, int direction)
        {
            position = pos;
            directionOnPath = direction;
        }

        public void calculateBothDirections()
        {
            x_direction = calculateDirection(spriteX, path[position + directionOnPath].Item1);
            y_direction = calculateDirection(spriteY, path[position + directionOnPath].Item2);
        }

        public int calculateDirection(int a, int b)
        {
            if (a < b)
                return 1;
            if (a > b)
                return -1;
            else
                return 0;
        }

        #endregion



















        protected new void updateAnimation()
        {
            base.updateAnimation();
        }

        // b == true if the character is walking, false if not walking
        private void updateCurrentWalkingFrame(bool b)
        {
            if (b)
            {
                currentwalkingframe++;
                // FIX HERE, 3 should not be hardcoded
                if (currentwalkingframe >= 3 || currentwalkingframe < 0)
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

        protected override int updateDirectionFrame(int movedX, int movedY)
        {
            if (movedX > 0)
            {
                return 1;
            }
            else if (movedX < 0)
            {
                return 3;
            }
            else if (movedY > 0)
            {
                return 2;
            }
            else if (movedY < 0)
            {
                return 0;
            }
            return 0;
        }

        protected override bool handleCollision(Sprite sprite)
        {
            return true;
        }
    }
}