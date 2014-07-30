using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using TiledSharp;

namespace Eteephonehome
{
    class Player : MovingSprite
    {

        Sprite hidingHedge;

        GraphicsDeviceManager graphics;
        ContentManager content;

        Vector2 origin;

        protected bool actionKey = false;
        bool touchingHedge = false;
        bool hiding = false;
        bool invisible = false;
        bool firestarter = false;
        bool greenthumb = false;
        protected int currentPowerUp = 0;

        private int x_accel;
        private int y_accel;
        public double x_vel;
        public double y_vel;
        private double friction;

        private bool right = false; public bool getRight() { return right; }
        private bool left = false; public bool getLeft() { return left; }
        private bool down = false; public bool getDown() { return down; }
        private bool up = false; public bool getUp() { return up; }

        int upNegative, downNegative, leftNegative, rightNegative;

        private bool pushing;
        public int staminaIncrement = 1;
        public int stamina { get; set; }
        public int max_stamina;

        public Player(GraphicsDeviceManager graphics, ContentManager content, int x, int y, int height, int width)
            : base(content, x, y, height, width)
        {
            this.graphics = graphics;
            this.content = content;
            speed = 3;
            diagonalSpeed = 1;
            mudSpeed = 1;
            pushing = false;
            stamina = 0;
            max_stamina = 5;
            speed = 3;
            friction = .15;
            x_accel = 0;
            y_accel = 0;
            x_vel = 0;
            y_vel = 0;
            friction = .15;
            defaultSpeed = speed;

            //rose
            Viewport viewport = graphics.GraphicsDevice.Viewport;

        }

        public override void LoadContent(ContentManager content)
        {
           // image = content.Load<Texture2D>("ET_down.png");

            animationframes = new List<List<Texture2D>>();
            for (int i = 0; i < 4; i++)
            {
                animationframes.Add(new List<Texture2D>());
            }
            animationframes[0].Add(content.Load<Texture2D>("ET Sprites/ET_up.png"));
            animationframes[0].Add(content.Load<Texture2D>("ET Sprites/ET_up1.png"));
            animationframes[0].Add(content.Load<Texture2D>("ET Sprites/ET_up_arm.png"));
            animationframes[1].Add(content.Load<Texture2D>("ET Sprites/ET_right.png"));
            animationframes[1].Add(content.Load<Texture2D>("ET Sprites/ET_right1.png"));
            animationframes[1].Add(content.Load<Texture2D>("ET Sprites/ET_right_arm.png"));
            animationframes[2].Add(content.Load<Texture2D>("ET Sprites/ET_down.png"));
            animationframes[2].Add(content.Load<Texture2D>("ET Sprites/ET_down1.png"));
            animationframes[2].Add(content.Load<Texture2D>("ET Sprites/ET_down_arm.png"));
            animationframes[3].Add(content.Load<Texture2D>("ET Sprites/ET_left.png"));
            animationframes[3].Add(content.Load<Texture2D>("ET Sprites/ET_left1.png"));
            animationframes[3].Add(content.Load<Texture2D>("ET Sprites/ET_left_arm.png"));

            base.LoadContent(content);
        }

        public new void Draw(SpriteBatch sb)
        {
            //Console.WriteLine("x=" + spriteX + ", y=" + spriteY);
            //drawReflection(sb);
            if (invisible)
            {
                base.Draw(sb, Color.Black);
            }
            else if (firestarter)
            {
                base.Draw(sb, Color.Red);
            }
            else
            {
                base.Draw(sb);
            }
        }

        public void drawReflection(SpriteBatch sb)
        {
            SpriteEffect spriteEffect;
            Vector2 origin = new Vector2(0, 0);
            origin.X = 0;
            origin.Y = 0;
            Rectangle rect = new Rectangle(spriteX, spriteY, spriteWidth, spriteHeight);
            Rectangle sourceRect; new Rectangle(spriteX, spriteY, spriteWidth, spriteHeight);
            float rotation = 0f;

            int height = 350 - spriteY;
            if (height < 0)
            {
                height = 0;
            }
            else if (height > 64)
            {
                height = 64;
            }
            height = Math.Abs(height);

            #region Switch
            switch (currentDirectionFrame)
            {
                case 0:
                    rect = new Rectangle(spriteX + 20, spriteY - 18, 64, height);
                    sourceRect = new Rectangle(0, 0, 64, height);
                    sb.Draw(animationframes[2][0], rect, sourceRect, Color.CornflowerBlue);

                    //Texture2D tx = Crop(animationframes[2][0], sourceRect);
                    //Console.WriteLine("height = " + height + ", spriteheight" + spriteHeight);
                    //sb.Draw(tx, sourceRect, null, Color.CornflowerBlue);
                    return;
                case 1:
                    rotation = 1.57f;
                    //spriteEffect = SpriteEffect.FlipVertically;
                    break;
                case 2:
                    rotation = 3.14f;
                    break;
                case 3:
                    rotation = 1.57f;
                    //spriteEffect = SpriteEffect.FlipVertically;
                    break;
                default:
                    break;
            }
            #endregion
            //sb.Draw(animationframes[2][0], rect, null, Color.CornflowerBlue, rotation, origin, SpriteEffects.None, 0f);
        }

        public Texture2D Crop(Texture2D source, Rectangle area)
        {
            if (source == null)
                return null;

            Texture2D cropped = new Texture2D(source.GraphicsDevice, area.Width, area.Height);
            Color[] data = new Color[source.Width * source.Height];
            Color[] cropData = new Color[cropped.Width * cropped.Height + 4];

            source.GetData(data);

            int index = 0;
            for (int y = area.Y; y < area.Y + area.Height; y++)
            {
                for (int x = area.X; x < area.X + area.Width; x++)
                {
                    cropData[index] = data[x + (y * source.Width)];
                    index++;
                }
            }

            cropped.SetData(cropData);

            return cropped;
        }

        public void Update(Controls controls, GameTime gameTime, ref List<Sprite> spriteList)
        {
            if (hiding && !actionKey)
            {
                toggleHide(null, false);
            }
            Move(controls, spriteList);
        }

        public bool incrementStamina(int amount)
        {
            if (stamina >= max_stamina)
            {
                return false;
            }
            stamina += amount;
            return true;
        }

        protected new void updateAnimation()
        {
            base.updateAnimation();
        }

        public new void Move()
        {
            base.Move();
        }

        protected void Move(Controls controls, List<Sprite> spriteList)
        {
            calculateMovement(controls, spriteList);
            if (firestarter && touchingHedge)
            {
                // FIREBALL
            }
            base.Move();
        }

        protected void calculateMovement(Controls controls, List<Sprite> spriteList)
        {
            // BUTTONS.A AND BUTTONS.B MIGHT NEED TO BE SWITCHED
            if (controls.onPress(Keys.X, Buttons.A))
            {
                reaching = true;
                if (currentPowerUp == 1)
                {
                    invisible = true;
                }
                else if (currentPowerUp == 2)
                {
                    firestarter = true;
                }
                else if (currentPowerUp == 3)
                {
                    greenthumb = true;
                }
            }
            else
            {
                if (controls.onRelease(Keys.X, Buttons.A))
                {
                    reaching = false;
                    invisible = false;
                    firestarter = false;
                    greenthumb = false;
                }
            }

            if (controls.onPress(Keys.Z, Buttons.B))
            {
                actionKey = true;
                reaching = true;
            }
            else
            {
                if (controls.onRelease(Keys.Z, Buttons.B))
                {
                    actionKey = false;
                    reaching = false;
                }
            }

            if (controls.onPress(Keys.Right, Buttons.DPadRight))
            {
                right = true;
                rightNegative = speed;
                x_accel += speed;
            }
            else
            {
                if (controls.onRelease(Keys.Right, Buttons.DPadRight))
                {
                    right = false;
                    x_accel -= rightNegative;
                    rightNegative = 0;
                }
            }

            if (controls.onPress(Keys.Left, Buttons.DPadLeft))
            {
                left = true;
                leftNegative = speed;
                x_accel -= speed;
            }
            else
            {
                if (controls.onRelease(Keys.Left, Buttons.DPadLeft))
                {
                    left = false;
                    x_accel += leftNegative;
                    leftNegative = 0;
                }
            }

            if (controls.onPress(Keys.Down, Buttons.DPadDown))
            {
                down = true;
                downNegative = speed;
                y_accel += speed;
            }
            else
            {
                if (controls.onRelease(Keys.Down, Buttons.DPadDown))
                {
                    down = false;
                    y_accel -= downNegative;
                    downNegative = 0;

                }
            }

            if (controls.onPress(Keys.Up, Buttons.DPadUp))
            {
                up = true;
                upNegative = speed;
                y_accel -= speed;
            }
            else
            {
                if (controls.onRelease(Keys.Up, Buttons.DPadUp))
                {
                    up = false;
                    y_accel += upNegative;
                    upNegative = 0;
                }
            }

            double playerFriction = pushing ? (friction * 3) : friction;
            x_vel = x_vel * (1 - playerFriction) + x_accel * .10;
            y_vel = y_vel * (1 - playerFriction) + y_accel * .10;
            movedX = Convert.ToInt32(x_vel);
            movedY = Convert.ToInt32(y_vel);

            calculateIfDiagonal(movedX, movedY);

            //speed = keepWithinBounds(speed, 0, defaultSpeed);
            //movedX = movedX * speed;
            //movedY = movedY * speed;

            updateDirectionFrame(movedX, movedY);

            detectCollisions(ref spriteList);

            spriteX += movedX * speed;
            spriteY += movedY * speed;

        }

        protected override int updateDirectionFrame(int movedX, int movedY)
        {
            if (left && !right)
            {
                return 3;
            }
            if (!left && right)
            {
                return 1;
            }
            if (up && !down)
            {
                return 0;
            }
            if (!up && down)
            {
                return 2;
            }
            return currentDirectionFrame;
        }

        protected override bool handleCollision(Sprite sprite)
        {

            touchingHedge = false;
            if (((sprite as Mud) != null))
            {
                //Console.WriteLine("mudspeed");
                //speed = mudSpeed;
            }

            else
            {
                //Console.WriteLine("smaug");
                //speed = defaultSpeed;
            }

            if (((sprite as Hedge) != null))
            {
                sprite = (Hedge)sprite;
                touchingHedge = true;
                repel();
                if (firestarter && ((Hedge)sprite).getHedgeMode() == 1)
                {
                    ((Hedge)sprite).burnHedge();
                }
                else if (reaching && ((Hedge)sprite).getHedgeMode() == 4)
                {
                    toggleHide(sprite, true);
                }
                return false;
            }

            else if (((sprite as Candy) != null))
            {
                return incrementStamina(staminaIncrement);
            }

            else if (((sprite as Cop) != null))
            {
                //Console.WriteLine("Potato Chip");
                //((Cop)sprite).reverseDirection();
                return false;
            }

            else if (((sprite as SpaceShipPart) != null))
            {
                System.Environment.Exit(0);
            }
            //      this.currentPowerUp = 1;
            //      return true;
            else if (((sprite as PowerUp) != null))
            {
                updateCurrentPowerUp(2);
                return true;
            }
            return false;
        }

        #region Power Up Methods

        protected void updateCurrentPowerUp(int input)
        {
            currentPowerUp = input;
        }

        #endregion


        public bool playerIsVisible()
        {
            return invisible && !hiding;
        }

        public int getPowerUp()
        {
            return currentPowerUp;
        }
        private void toggleHide(Sprite sprite, bool selection)
        {
            if (selection && actionKey == true)
            {
                hiding = true;
                hidingHedge = sprite;
                ((Hedge)sprite).hideCharacter(true);
            }
            else
            {
                hiding = false;
                //((Hedge)hidingHedge).hideCharacter(false);
                hidingHedge = null;
            }

        }



    }

}

