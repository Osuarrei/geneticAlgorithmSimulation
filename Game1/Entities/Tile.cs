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
    public class Tile : Interfaces.IDrawable
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
            spriteBatch.FillRectangle(new Vector2(xloc * pixelwidth, yloc * pixelwidth), new Size2(pixelwidth, pixelwidth), new Color(
                    nutrientR / maxNutrient,
                    nutrientG / maxNutrient,
                    nutrientB / maxNutrient
                    ));
        }

        public int xloc { get; set; }
        public int yloc { get; set; }
        private int pixelwidth = (int)SimulationStateEnums.MapValues.TileSize;
        public float nutrientR { get; set; }
        public float nutrientG { get; set; }
        public float nutrientB { get; set; }
        private float maxNutrient = 10.0f;
        public static Random tileRandom = new Random();
    }
}
