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

    class ScreenGrid
    {
        protected int x_width;
        protected int y_height;
        protected int tile_x_dimension { get; set; }
        protected int tile_y_dimension { get; set; }
        protected int numberOfSprites;

        List<int>[,] grid;
        List<Sprite> spriteList;

        public ScreenGrid(int x_width, int y_height, int tile_x_dimension, int tile_y_dimension)
        {
            this.x_width = x_width;
            this.y_height = y_height;
            this.tile_x_dimension = tile_x_dimension;
            this.tile_y_dimension = tile_y_dimension;
            numberOfSprites = 0;
            grid = new List<int>[x_width / tile_x_dimension, y_height / tile_y_dimension];
        }

        public int addToGrid(Object o, int x_val, int y_val)
        {
            int index = generateIndexNumber();
            int x = calculateXValue(x_val);
            int y = calculateYValue(y_val);
            
            return index;
        }

        public bool removeFromGrid(Object o, int x, int y)
        {
            return true;
        }

        public int calculateXValue(int x_location)
        {
            return calculateGridCoordinate(x_location);
        }

        public int calculateYValue(int y_location)
        {
            return calculateGridCoordinate(y_location);
        }

        public int calculateGridCoordinate(int x)
        {
            return 0;
        }

        public int getX()
        {
            return x_width;
        }

        public int getY()
        {
            return y_height;
        }

        public int generateIndexNumber()
        {
            //Do this in one line
            numberOfSprites++;
            return numberOfSprites - 1;
        }

    }



}
