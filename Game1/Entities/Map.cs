﻿using Game1.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Entities
{
    public class Map : Interfaces.IDrawable
    {
        public void Draw(GraphicsDevice graphicsDevice)
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < heightY; y++)
            {
                for (int x = 0; x < widthX; x++)
                {
                    tileCollection[x, y].Draw(spriteBatch);
                }
            }
        }

        public Map(int widthx, int heighty)
        {
            tileCollection = new Tile[widthx,heighty];
            widthX = widthx;
            heightY = heighty;

            for (int y = 0; y < heighty; y++)
            {
                for (int x = 0; x < widthx; x++)
                {
                    tileCollection[x, y] = new Tile(x, y);
                }
            }
        }

        public Tile GetTile(int xloc, int yloc)
        {
            return tileCollection[xloc, yloc];
        }

        public Tile GetTile(Vector2 locationVector)
        {
            return tileCollection[(int)locationVector.X, (int)locationVector.Y];
        }

        private Tile[,] tileCollection;
        public int widthX { get; set; }
        public int heightY { get; set; }
    }
}
