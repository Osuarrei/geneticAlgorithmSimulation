﻿using Game1.Interfaces.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Entities
{
    public class Tile : Interfaces.Interfaces.IDrawable
    {
        public Tile(int x, int y)
        {
            xloc = x;
            yloc = y;
            nutrientR = (float)Tile.tileRandom.NextDouble() * maxNutrient;
            nutrientG = (float)Tile.tileRandom.NextDouble() * maxNutrient;
            nutrientB = (float)Tile.tileRandom.NextDouble() * maxNutrient;
        }

        public void Draw(GraphicsDevice graphicsDevice)
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.FillRectangle(new Vector2(xloc * Tile.pixelwidth, yloc * Tile.pixelwidth), new Size2(Tile.pixelwidth, Tile.pixelwidth), new Color(
                    nutrientR / maxNutrient,
                    nutrientG / maxNutrient,
                    nutrientB / maxNutrient
                    ));

            spriteBatch.End();
        }

        public int xloc { get; set; }
        public int yloc { get; set; }
        public static int pixelwidth = 10;
        public float nutrientR { get; set; }
        public float nutrientG { get; set; }
        public float nutrientB { get; set; }
        private float maxNutrient = 10.0f;
        public static Random tileRandom = new Random();
    }
}